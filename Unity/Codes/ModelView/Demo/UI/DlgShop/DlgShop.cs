namespace ET
{
	public  class DlgShop :Entity,IAwake,IUILogic
	{

		public DlgShopViewComponent View { get => this.Parent.GetComponent<DlgShopViewComponent>();} 

		 

	}
}
