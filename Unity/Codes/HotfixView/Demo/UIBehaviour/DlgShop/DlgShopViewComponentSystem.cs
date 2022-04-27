
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgShopViewComponentAwakeSystem : AwakeSystem<DlgShopViewComponent> 
	{
		public override void Awake(DlgShopViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgShopViewComponentDestroySystem : DestroySystem<DlgShopViewComponent> 
	{
		public override void Destroy(DlgShopViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
