
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgEquipmentViewComponentAwakeSystem : AwakeSystem<DlgEquipmentViewComponent> 
	{
		public override void Awake(DlgEquipmentViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgEquipmentViewComponentDestroySystem : DestroySystem<DlgEquipmentViewComponent> 
	{
		public override void Destroy(DlgEquipmentViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
