namespace ET
{
	public  class DlgSelectCharacter :Entity,IAwake,IUILogic
	{

		public DlgSelectCharacterViewComponent View { get => this.Parent.GetComponent<DlgSelectCharacterViewComponent>();} 

		 

	}
}
