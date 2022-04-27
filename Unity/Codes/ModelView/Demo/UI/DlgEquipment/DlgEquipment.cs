namespace ET
{
	public  class DlgEquipment :Entity,IAwake,IUILogic
	{

		public DlgEquipmentViewComponent View { get => this.Parent.GetComponent<DlgEquipmentViewComponent>();} 

		 

	}
}
