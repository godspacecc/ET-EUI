
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgSelectCharacterViewComponentAwakeSystem : AwakeSystem<DlgSelectCharacterViewComponent> 
	{
		public override void Awake(DlgSelectCharacterViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgSelectCharacterViewComponentDestroySystem : DestroySystem<DlgSelectCharacterViewComponent> 
	{
		public override void Destroy(DlgSelectCharacterViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
