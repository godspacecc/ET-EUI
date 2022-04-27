
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgSelectCharacterViewComponent : Entity,IAwake,IDestroy 
	{
		public void DestroyWidget()
		{
			this.uiTransform = null;
		}

		public Transform uiTransform = null;
	}
}
