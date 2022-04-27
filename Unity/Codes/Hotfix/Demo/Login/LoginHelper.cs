using System;
using System.Diagnostics.Eventing;
using UnityEngine;

namespace ET
{
    [FriendClass(typeof(ServerInfosComponent))]
    [FriendClass(typeof(RoleInfosComponent))]
    [FriendClass(typeof(AccountInfoComponent))]
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            A2C_LoginAccount a2CLogin = null;
            Session session = null;
            try
            {
                session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                password = MD5Helper.StringMD5(password);
                a2CLogin = (A2C_LoginAccount) await session.Call(new C2A_LoginAccount() { Account = account, Password = password });
            }
            catch (Exception e)
            {
                session?.Dispose();
                Log.Error(e.ToString());
            }

            if (a2CLogin.Error != ErrorCode.ERR_Success)
            {
                session?.Dispose();
                return a2CLogin.Error;
            }

            zoneScene.AddComponent<SessionComponent>().Session = session;
            zoneScene.GetComponent<SessionComponent>().Session.AddComponent<PingComponent>();
            
            zoneScene.GetComponent<AccountInfoComponent>().Token = a2CLogin.Token;
            zoneScene.GetComponent<AccountInfoComponent>().AccountId = a2CLogin.AccountId;

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetServerInfos(Scene zonescene)
        {
            A2C_GetServerInfos a2CGetServerInfos = null;

            try
            {
                a2CGetServerInfos =(A2C_GetServerInfos) await zonescene.GetComponent<SessionComponent>().Session.Call(new C2A_GetServerInfos()
                {
                    AccountId = zonescene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zonescene.GetComponent<AccountInfoComponent>().Token,
                });
                if (a2CGetServerInfos.Error != ErrorCode.ERR_Success)
                {
                    return a2CGetServerInfos.Error;
                }

                foreach (var serverInfo in a2CGetServerInfos.ServerInfoList)
                {
                    ServerInfo info = zonescene.GetComponent<ServerInfosComponent>().AddChild<ServerInfo>();
                    info.FromMessage(serverInfo);
                    zonescene.GetComponent<ServerInfosComponent>().Add(info);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> CreateRole(Scene zonescene,string name)
        {
            A2C_CreateRole a2CCreateRole = null;

            try
            {
                a2CCreateRole =(A2C_CreateRole) await zonescene.GetComponent<SessionComponent>().Session.Call(new C2A_CreateRole()
                {
                    AccountId = zonescene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zonescene.GetComponent<AccountInfoComponent>().Token,
                    Name = name,
                    ServerId = zonescene.GetComponent<ServerInfosComponent>().CurrentServerdId,
                });
                if (a2CCreateRole.Error != ErrorCode.ERR_Success)
                {
                    return a2CCreateRole.Error;
                }

                RoleInfo roleInfo = zonescene.GetComponent<RoleInfosComponent>().AddChild<RoleInfo>();
                roleInfo.FromMessage(a2CCreateRole.RoleInfo);
                
                zonescene.GetComponent<RoleInfosComponent>().Add(roleInfo);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
        
        public static async ETTask<int> GetRoles(Scene zonescene)
        {
            A2C_GetRoles a2CGetRoles = null;
            
            try
            {
                a2CGetRoles = (A2C_GetRoles) await zonescene.GetComponent<SessionComponent>().Session.Call(new C2A_GetRoles()
                {
                    AccountId = zonescene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zonescene.GetComponent<AccountInfoComponent>().Token,
                    ServerId = zonescene.GetComponent<ServerInfosComponent>().CurrentServerdId,
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2CGetRoles.Error != ErrorCode.ERR_Success)
            {
                return a2CGetRoles.Error;
            }

            zonescene.GetComponent<RoleInfosComponent>().RoleInfos.Clear();
            foreach (var roleinfoproto in a2CGetRoles.RoleInfo)
            {
                RoleInfo roleInfo = zonescene.GetComponent<RoleInfosComponent>().AddChild<RoleInfo>();
                roleInfo.FromMessage(roleinfoproto);
                zonescene.GetComponent<RoleInfosComponent>().RoleInfos.Add(roleInfo);
            }
            
            return ErrorCode.ERR_Success;
        }
        
        public static async ETTask<int> DeleteRole(Scene zonescene)
        {
            A2C_DeleteRole a2CDeleteRole = null;

            try
            {
                a2CDeleteRole =(A2C_DeleteRole) await zonescene.GetComponent<SessionComponent>().Session.Call(new C2A_DeleteRole()
                {
                    AccountId = zonescene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zonescene.GetComponent<AccountInfoComponent>().Token,
                    RoleInfoId = zonescene.GetComponent<RoleInfosComponent>().CurrentRoleId,
                    ServerId = zonescene.GetComponent<ServerInfosComponent>().CurrentServerdId
                });
                Game.EventSystem.PublishAsync(new EventType.LoginFinish() {ZoneScene = zonescene}).Coroutine();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            
            if (a2CDeleteRole.Error != ErrorCode.ERR_Success)
            {
                return a2CDeleteRole.Error;
            }

            int index = zonescene.GetComponent<RoleInfosComponent>().RoleInfos.FindIndex(a => a.Id == a2CDeleteRole.DeleteRoleInfoId);
            zonescene.GetComponent<RoleInfosComponent>().RoleInfos.RemoveAt(index);
            zonescene.GetComponent<RoleInfosComponent>().CurrentRoleId = 0;
            
            return ErrorCode.ERR_Success;
        }
        
        public static async ETTask<int> GetRelamKey(Scene zonescene)
        {
            A2C_GetRelamKey a2CGetRelamKey = null;

            try
            {
                a2CGetRelamKey = (A2C_GetRelamKey) await zonescene.GetComponent<SessionComponent>().Session.Call(new C2A_GetRelamKey()
                {
                    AccountId = zonescene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zonescene.GetComponent<AccountInfoComponent>().Token,
                    ServerId = zonescene.GetComponent<ServerInfosComponent>().CurrentServerdId,
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2CGetRelamKey.Error != ErrorCode.ERR_Success)
            {
                return a2CGetRelamKey.Error;
            }

            zonescene.GetComponent<AccountInfoComponent>().RelamAddress = a2CGetRelamKey.RelamAddress;
            zonescene.GetComponent<AccountInfoComponent>().RelamKey = a2CGetRelamKey.RelamKey;
            zonescene.GetComponent<SessionComponent>().Session.Dispose();

            return ErrorCode.ERR_Success;
        }
        
        public static async ETTask<int> EnterGame(Scene zonescene)
        {
            string relamAddress = zonescene.GetComponent<AccountInfoComponent>().RelamAddress;
            
            Log.Warning($"relamaddress: {relamAddress}");
            
            R2C_LoginRelam r2CLoginRelam;
            
            Session session = zonescene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(relamAddress));
            try
            {
                r2CLoginRelam = (R2C_LoginRelam) await session.Call(new C2R_LoginRelam()
                {
                    AccountId = zonescene.GetComponent<AccountInfoComponent>().AccountId,
                    RelamTokenKey = zonescene.GetComponent<AccountInfoComponent>().RelamKey,
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                session?.Dispose();
                return ErrorCode.ERR_NetWorkError;
            }
            session?.Dispose();

            if (r2CLoginRelam.Error != ErrorCode.ERR_Success)
            {
                return r2CLoginRelam.Error;
            }

            Log.Debug("从Relam获取GateAddress成功");

            G2C_LoginGameGate g2CLoginGate;
            Session gatesession = zonescene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(r2CLoginRelam.GateAddress));
            gatesession.AddComponent<PingComponent>();
            zonescene.GetComponent<SessionComponent>().Session = gatesession;
            try
            {
                g2CLoginGate = (G2C_LoginGameGate) await gatesession.Call(new C2G_LoginGameGate()
                {
                    Account = zonescene.GetComponent<AccountInfoComponent>().AccountId,
                    RoleId = zonescene.GetComponent<RoleInfosComponent>().CurrentRoleId,
                    Key = r2CLoginRelam.GateSessionKey,
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                zonescene.GetComponent<SessionComponent>().Session?.Dispose();
                return ErrorCode.ERR_NetWorkError;
            }

            if (g2CLoginGate.Error != ErrorCode.ERR_Success)
            {
                zonescene.GetComponent<SessionComponent>().Session?.Dispose();
                return g2CLoginGate.Error;
            }
            
            Log.Debug("登录Gate服成功");

            G2C_EnterGame g2CEnterGame = null;
            try
            {
                g2CEnterGame = (G2C_EnterGame)await gatesession.Call(new C2G_EnterGame());
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                zonescene.GetComponent<SessionComponent>().Session?.Dispose();
                return ErrorCode.ERR_NetWorkError;
            }
            
            if (g2CEnterGame.Error != ErrorCode.ERR_Success)
            {
                Log.Error(g2CEnterGame.Error.ToString());
                return g2CEnterGame.Error;
            }

            Log.Debug("角色进入游戏成功！");
            zonescene.GetComponent<PlayerComponent>().MyId = g2CEnterGame.MyId;
            await zonescene.GetComponent<ObjectWait>().Wait<WaitType.Wait_SceneChangeFinish>();
            
            return ErrorCode.ERR_Success;
        }
    }
}