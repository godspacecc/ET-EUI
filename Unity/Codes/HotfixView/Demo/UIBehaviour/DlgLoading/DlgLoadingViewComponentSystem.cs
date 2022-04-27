
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgLoadingViewComponentAwakeSystem : AwakeSystem<DlgLoadingViewComponent> 
	{
		public override void Awake(DlgLoadingViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgLoadingViewComponentDestroySystem : DestroySystem<DlgLoadingViewComponent> 
	{
		public override void Destroy(DlgLoadingViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
