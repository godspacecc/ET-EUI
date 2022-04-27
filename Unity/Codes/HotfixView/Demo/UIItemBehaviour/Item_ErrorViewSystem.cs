
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_ErrorDestroySystem : DestroySystem<Scroll_Item_Error> 
	{
		public override void Destroy( Scroll_Item_Error self )
		{
			self.DestroyWidget();
		}
	}
}
