
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class ES_ErrorDefault : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy 
	{
		public TMPro.TextMeshProUGUI E_MessageTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MessageTextMeshProUGUI == null )
     			{
		    		this.m_E_MessageTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_Message");
     			}
     			return this.m_E_MessageTextMeshProUGUI;
     		}
     	}

		public UnityEngine.RectTransform E_MessageRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MessageRectTransform == null )
     			{
		    		this.m_E_MessageRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"E_Message");
     			}
     			return this.m_E_MessageRectTransform;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_MessageTextMeshProUGUI = null;
			this.m_E_MessageRectTransform = null;
			this.uiTransform = null;
		}

		private TMPro.TextMeshProUGUI m_E_MessageTextMeshProUGUI = null;
		private UnityEngine.RectTransform m_E_MessageRectTransform = null;
		public Transform uiTransform = null;
	}
}
