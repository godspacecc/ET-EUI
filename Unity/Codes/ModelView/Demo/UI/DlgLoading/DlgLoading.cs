namespace ET
{
	public  class DlgLoading :Entity,IAwake,IUILogic
	{

		public DlgLoadingViewComponent View { get => this.Parent.GetComponent<DlgLoadingViewComponent>();} 

		 

	}
}
