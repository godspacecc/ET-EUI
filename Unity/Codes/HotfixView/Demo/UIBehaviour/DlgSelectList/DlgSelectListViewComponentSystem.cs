
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgSelectListViewComponentAwakeSystem : AwakeSystem<DlgSelectListViewComponent> 
	{
		public override void Awake(DlgSelectListViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgSelectListViewComponentDestroySystem : DestroySystem<DlgSelectListViewComponent> 
	{
		public override void Destroy(DlgSelectListViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
