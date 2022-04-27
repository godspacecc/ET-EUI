﻿using System;
using System.Collections.Generic;

namespace ET
{
    public enum TimerClass
    {
        None,
        OnceTimer,
        OnceWaitTimer,
        RepeatedTimer,
    }
    
    [ObjectSystem]
    public class TimerActionAwakeSystem: AwakeSystem<TimerAction, TimerClass, long, int, object>
    {
        public override void Awake(TimerAction self, TimerClass timerClass, long time, int type, object obj)
        {
            self.TimerClass = timerClass;
            self.Object = obj;
            self.Time = time;
            self.Type = type;
        }
    }

    [ObjectSystem]
    public class TimerActionDestroySystem: DestroySystem<TimerAction>
    {
        public override void Destroy(TimerAction self)
        {
            self.Object = null;
            self.Time = 0;
            self.TimerClass = TimerClass.None;
            self.Type = 0;
        }
    }
    
    public class TimerAction: Entity, IAwake, IAwake<TimerClass, long, int, object>, IDestroy
    {
        public TimerClass TimerClass;
        /// <summary>
        /// 执行参数
        /// </summary>
        public object Object;
        /// <summary>
        /// 间隔时间
        /// </summary>
        public long Time;
        /// <summary>
        /// 任务id类型
        /// </summary>
        public int Type;
    }

    [ObjectSystem]
    public class TimerComponentAwakeSystem: AwakeSystem<TimerComponent>
    {
        public override void Awake(TimerComponent self)
        {
            TimerComponent.Instance = self;
            self.Awake();
        }
    }

    [ObjectSystem]
    public class TimerComponentUpdateSystem: UpdateSystem<TimerComponent>
    {
        public override void Update(TimerComponent self)
        {
            self.Update();
        }
    }
    
    [ObjectSystem]
    public class TimerComponentLoadSystem: LoadSystem<TimerComponent>
    {
        public override void Load(TimerComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class TimerComponentDestroySystem: DestroySystem<TimerComponent>
    {
        public override void Destroy(TimerComponent self)
        {
            TimerComponent.Instance = null;
        }
    }

    public class TimerComponent: Entity, IAwake, IUpdate, ILoad, IDestroy
    {
        public static TimerComponent Instance
        {
            get;
            set;
        }

        /// <summary>
        /// key: time, value: timer id
        /// </summary>
        private readonly MultiMap<long, long> TimeId = new MultiMap<long, long>();

        private readonly Queue<long> timeOutTime = new Queue<long>();

        private readonly Queue<long> timeOutTimerIds = new Queue<long>();
        
        private readonly Queue<long> everyFrameTimer = new Queue<long>();

        // 记录最小时间，不用每次都去MultiMap取第一个值
        private long minTime;

        private const int TimeTypeMax = 10000;

        private ITimer[] timerActions;

        public void Awake()
        {
            this.foreachFunc = (k, v) =>
            {
                if (k > this.timeNow)
                {
                    minTime = k;
                    return false;
                }

                this.timeOutTime.Enqueue(k);
                return true;
            };
            
            this.timerActions = new ITimer[TimeTypeMax];

            List<Type> types = Game.EventSystem.GetTypes(typeof (TimerAttribute));

            foreach (Type type in types)
            {
                ITimer iTimer = Activator.CreateInstance(type) as ITimer;
                if (iTimer == null)
                {
                    Log.Error($"Timer Action {type.Name} 需要继承 ITimer");
                    continue;
                }
                
                object[] attrs = type.GetCustomAttributes(typeof(TimerAttribute), false);
                if (attrs.Length == 0)
                {
                    continue;
                }

                foreach (object attr in attrs)
                {
                    TimerAttribute timerAttribute = attr as TimerAttribute;
                    this.timerActions[timerAttribute.Type] = iTimer;
                }
            }
        }

        private long timeNow;
        private Func<long, List<long>, bool> foreachFunc;

        public void Update()
        {
            #region 每帧执行的timer，不用foreach TimeId，减少GC

            int count = this.everyFrameTimer.Count;
            for (int i = 0; i < count; ++i)
            {
                long timerId = this.everyFrameTimer.Dequeue();
                TimerAction timerAction = this.GetChild<TimerAction>(timerId);
                if (timerAction == null)
                {
                    continue;
                }
                Run(timerAction);
            }

            #endregion
            
            if (this.TimeId.Count == 0)
            {
                return;
            }

            timeNow = TimeHelper.ServerNow();

            if (timeNow < this.minTime)
            {
                return;
            }

            this.TimeId.ForEachFunc(this.foreachFunc);

            while (this.timeOutTime.Count > 0)
            {
                long time = this.timeOutTime.Dequeue();
                var list = this.TimeId[time];
                for (int i = 0; i < list.Count; ++i)
                {
                    long timerId = list[i];
                    this.timeOutTimerIds.Enqueue(timerId);
                }

                this.TimeId.Remove(time);
            }

            while (this.timeOutTimerIds.Count > 0)
            {
                long timerId = this.timeOutTimerIds.Dequeue();

                TimerAction timerAction = this.GetChild<TimerAction>(timerId);
                if (timerAction == null)
                {
                    continue;
                }
                Run(timerAction);
            }
        }

        private void Run(TimerAction timerAction)
        {
            switch (timerAction.TimerClass)
            {
                case TimerClass.OnceTimer:
                {
                    int type = timerAction.Type;
                    ITimer timer = this.timerActions[type];
                    if (timer == null)
                    {
                        Log.Error($"not found timer action: {type}");
                        return;
                    }
                    timer.Handle(timerAction.Object);
                    break;
                }
                case TimerClass.OnceWaitTimer:
                {
                    ETTask<bool> tcs = timerAction.Object as ETTask<bool>;
                    this.Remove(timerAction.Id);
                    tcs.SetResult(true);
                    break;
                }
                case TimerClass.RepeatedTimer:
                {
                    int type = timerAction.Type;
                    long tillTime = TimeHelper.ServerNow() + timerAction.Time;
                    this.AddTimer(tillTime, timerAction);

                    ITimer timer = this.timerActions[type];
                    if (timer == null)
                    {
                        Log.Error($"not found timer action: {type}");
                        return;
                    }
                    timer.Handle(timerAction.Object);
                    break;
                }
            }
        }
        
        private void AddTimer(long tillTime, TimerAction timer)
        {
            if (timer.TimerClass == TimerClass.RepeatedTimer && timer.Time == 0)
            {
                this.everyFrameTimer.Enqueue(timer.Id);
                return;
            }
            this.TimeId.Add(tillTime, timer.Id);
            if (tillTime < this.minTime)
            {
                this.minTime = tillTime;
            }
        }

        public bool Remove(ref long id)
        {
            long i = id;
            id = 0;
            return this.Remove(i);
        }
        
        private bool Remove(long id)
        {
            if (id == 0)
            {
                return false;
            }

            TimerAction timerAction = this.GetChild<TimerAction>(id);
            if (timerAction == null)
            {
                return false;
            }
            timerAction.Dispose();
            return true;
        }

        public async ETTask<bool> WaitTillAsync(long tillTime, ETCancellationToken cancellationToken = null)
        {
            if (timeNow >= tillTime)
            {
                return true;
            }

            ETTask<bool> tcs = ETTask<bool>.Create(true);
            TimerAction timer = this.AddChild<TimerAction, TimerClass, long, int, object>(TimerClass.OnceWaitTimer, tillTime - timeNow, 0, tcs, true);
            this.AddTimer(tillTime, timer);
            long timerId = timer.Id;

            void CancelAction()
            {
                if (this.Remove(timerId))
                {
                    tcs.SetResult(false);
                }
            }
            
            bool ret;
            try
            {
                cancellationToken?.Add(CancelAction);
                ret = await tcs;
            }
            finally
            {
                cancellationToken?.Remove(CancelAction);    
            }
            return ret;
        }

        public async ETTask<bool> WaitFrameAsync(ETCancellationToken cancellationToken = null)
        {
            bool ret = await WaitAsync(1, cancellationToken);
            return ret;
        }

        public async ETTask<bool> WaitAsync(long time, ETCancellationToken cancellationToken = null)
        {
            if (time == 0)
            {
                return true;
            }
            long tillTime = TimeHelper.ServerNow() + time;

            ETTask<bool> tcs = ETTask<bool>.Create(true);
            
            TimerAction timer = this.AddChild<TimerAction, TimerClass, long, int, object>(TimerClass.OnceWaitTimer, time, 0, tcs, true);
            this.AddTimer(tillTime, timer);
            long timerId = timer.Id;

            void CancelAction()
            {
                if (this.Remove(timerId))
                {
                    tcs.SetResult(false);
                }
            }

            bool ret;
            try
            {
                cancellationToken?.Add(CancelAction);
                ret = await tcs;
            }
            finally
            {
                cancellationToken?.Remove(CancelAction); 
            }
            return ret;
        }
        
        // 用这个优点是可以热更，缺点是回调式的写法，逻辑不连贯。WaitTillAsync不能热更，优点是逻辑连贯。
        // wait时间短并且逻辑需要连贯的建议WaitTillAsync
        // wait时间长不需要逻辑连贯的建议用NewOnceTimer
        /// <summary>
        /// 在tillTime执行类型为type,参数为args的任务
        /// </summary>
        /// <param name="tillTime"></param>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public long NewOnceTimer(long tillTime, int type, object args)
        {
            if (tillTime < TimeHelper.ServerNow())
            {
                Log.Warning($"new once time too small: {tillTime}");
            }
            TimerAction timer = this.AddChild<TimerAction, TimerClass, long, int, object>(TimerClass.OnceTimer, tillTime, type, args, true);
            this.AddTimer(tillTime, timer);
            return timer.Id;
        }

        public long NewFrameTimer(int type, object args)
        {
#if NOT_UNITY
			return NewRepeatedTimerInner(100, type, args);
#else
            return NewRepeatedTimerInner(0, type, args);
#endif
        }

        /// <summary>
        /// 创建一个RepeatedTimer
        /// </summary>
        private long NewRepeatedTimerInner(long time, int type, object args)
        {
#if NOT_UNITY
			if (time < 100)
			{ 
				throw new Exception($"repeated timer < 100, timerType: time: {time}");
			}
#endif
            long tillTime = TimeHelper.ServerNow() + time;
            TimerAction timer = this.AddChild<TimerAction, TimerClass, long, int, object>(TimerClass.RepeatedTimer, time, type, args, true);

            // 每帧执行的不用加到timerId中，防止遍历
            this.AddTimer(tillTime, timer);
            return timer.Id;
        }
        /// <summary>
        /// 每隔time 执行类型为type,参数为args的任务
        /// </summary>
        /// <param name="time"></param>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public long NewRepeatedTimer(long time, int type, object args)
        {
            if (time < 100)
            {
                Log.Error($"time too small: {time}");
                return 0;
            }
            return NewRepeatedTimerInner(time, type, args);
        }
    }
}