namespace ET
{
	public  class DlgAdventure :Entity,IAwake,IUILogic
	{

		public DlgAdventureViewComponent View { get => this.Parent.GetComponent<DlgAdventureViewComponent>();} 

		 

	}
}
