
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ES_ErrorDefaultAwakeSystem : AwakeSystem<ES_ErrorDefault,Transform> 
	{
		public override void Awake(ES_ErrorDefault self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ES_ErrorDefaultDestroySystem : DestroySystem<ES_ErrorDefault> 
	{
		public override void Destroy(ES_ErrorDefault self)
		{
			self.DestroyWidget();
		}
	}
}
