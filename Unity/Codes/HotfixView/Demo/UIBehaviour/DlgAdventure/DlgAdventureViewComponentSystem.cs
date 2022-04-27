
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgAdventureViewComponentAwakeSystem : AwakeSystem<DlgAdventureViewComponent> 
	{
		public override void Awake(DlgAdventureViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgAdventureViewComponentDestroySystem : DestroySystem<DlgAdventureViewComponent> 
	{
		public override void Destroy(DlgAdventureViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
