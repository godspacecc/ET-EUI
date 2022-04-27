
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_Item_Error : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Error BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public TMPro.TextMeshProUGUI E_MessageTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_MessageTextMeshProUGUI == null )
     				{
		    			this.m_E_MessageTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_Message");
     				}
     				return this.m_E_MessageTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_Message");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_MessageTextMeshProUGUI = null;
			this.uiTransform = null;
		}

		private TMPro.TextMeshProUGUI m_E_MessageTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
