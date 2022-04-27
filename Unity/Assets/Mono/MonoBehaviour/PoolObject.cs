using UnityEngine;

namespace ET
{
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    public class PoolObject : MonoBehaviour
    {
        public string poolName;
        //defines whether the object is waiting in pool or is in use 定义对象是在池中等待还是正在使用  
        public bool isPooled;
    }
}