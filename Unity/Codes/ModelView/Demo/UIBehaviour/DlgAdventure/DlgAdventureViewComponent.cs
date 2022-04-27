
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgAdventureViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.RectTransform EG_ContentRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_ContentRectTransform == null )
     			{
		    		this.m_EG_ContentRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_Content");
     			}
     			return this.m_EG_ContentRectTransform;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EG_ContentRectTransform = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_ContentRectTransform = null;
		public Transform uiTransform = null;
	}
}
