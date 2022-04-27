﻿using System;
using System.Threading.Tasks;

namespace ET
{
    [FriendClass(typeof(SessionPlayerComponent))]
    [FriendClass(typeof(SessionStateComponent))]
    public class C2G_EnterGameHandler: AMRpcHandler<C2G_EnterGame, G2C_EnterGame> 
    {
        protected override async ETTask Run(Session session, C2G_EnterGame request, G2C_EnterGame response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Gate)
            {
                Log.Debug($"请求的Scene错误,当前的Scene为{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                return;
            }

            SessionPlayerComponent sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
            if (sessionPlayerComponent == null)
            {
                response.Error = ErrorCode.ERR_SessionPlayerError;
                reply();
                return;
            }
            
            Player player = Game.EventSystem.Get(sessionPlayerComponent.PlayerInstanceId) as Player;
            if (player == null || player.IsDisposed)
            {
                response.Error = ErrorCode.ERR_NonePlayerError;
                reply();
                return;
            }

            long instanceId = session.InstanceId;

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, player.Account.GetHashCode()))
                {
                    if (instanceId != session.InstanceId || player.IsDisposed)
                    {
                        response.Error = ErrorCode.ERR_PlayerSessionError;
                        reply();
                        return;
                    }

                    if (session.GetComponent<SessionStateComponent>() != null && session.GetComponent<SessionStateComponent>().State == SessionState.Game)
                    {
                        response.Error = ErrorCode.ERR_SessionStateError;
                        reply();
                        return;
                    }

                    if (player.PlayerState == PlayerState.Game)
                    {
                        try
                        {
                            IActorResponse responseEnter = await MessageHelper.CallLocationActor(player.UnitId, 
                                new G2M_RequsetEnterGameState());
                            if (responseEnter.Error == ErrorCode.ERR_Success)
                            {
                                reply();
                                return;
                            }
                            Log.Error($"二次登录失败{responseEnter.Error}|{responseEnter.Message}");
                            response.Error = ErrorCode.ERR_ReEnterGameError;
                            await DisConnectHelper.KickPlayer(player, true);
                            reply();
                            session?.DisConnect().Coroutine();
                        }
                        catch (Exception e)
                        {
                            Log.Error($"二次登录失败{e.ToString()}");
                            response.Error = ErrorCode.ERR_ReEnterGameError2;
                            await DisConnectHelper.KickPlayer(player, true);
                            reply();
                            session?.DisConnect().Coroutine();
                            throw;
                        }
                        return;
                    }

                    try
                    {
                        //从数据库或缓存种加载Unit实体及其相关组件
                        (bool isNewPlayer, Unit unit) = await UnitHelper.LoadUnit(player);
                        unit.AddComponent<UnitGateComponent, long>(player.InstanceId);

                        await UnitHelper.InitUnit(unit, isNewPlayer);
                        response.MyId = unit.Id;
                        reply();
                        
                        StartSceneConfig sceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "Game");
                        await TransferHelper.Transfer(unit, sceneConfig.InstanceId, sceneConfig.Name);

                        SessionStateComponent sessionStateComponent = session.GetComponent<SessionStateComponent>();
                        if (sessionStateComponent != null)
                        {
                            session.AddComponent<SessionStateComponent>();
                        }
                        sessionStateComponent.State = SessionState.Game;

                        player.PlayerState = PlayerState.Game;
                    }
                    catch (Exception e)
                    {
                        Log.Error($"角色进入游戏逻辑服出现问题 账户:{player.Account} 角色Id:{player.Id} 异常信息:{e.ToString()}");
                        response.Error = ErrorCode.ERR_EnterGameError;
                        reply();
                        await DisConnectHelper.KickPlayer(player, true);
                        session.DisConnect().Coroutine();
                    }
                }
            }
        }
    }
}