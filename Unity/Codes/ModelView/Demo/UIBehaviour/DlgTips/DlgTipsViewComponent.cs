
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgTipsViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.RectTransform E_Popup_MessageRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Popup_MessageRectTransform == null )
     			{
		    		this.m_E_Popup_MessageRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"E_Popup_Message");
     			}
     			return this.m_E_Popup_MessageRectTransform;
     		}
     	}

		public ES_ErrorDefault ES_ErrorDefault
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_errordefault == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"E_Popup_Message/ES_ErrorDefault");
		    	   this.m_es_errordefault = this.AddChild<ES_ErrorDefault,Transform>(subTrans);
     			}
     			return this.m_es_errordefault;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_Popup_MessageRectTransform = null;
			this.m_es_errordefault?.Dispose();
			this.m_es_errordefault = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_E_Popup_MessageRectTransform = null;
		private ES_ErrorDefault m_es_errordefault = null;
		public Transform uiTransform = null;
	}
}
