
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgLoadingViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_ConfimButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ConfimButton == null )
     			{
		    		this.m_E_ConfimButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/E_Confim");
     			}
     			return this.m_E_ConfimButton;
     		}
     	}

		public UnityEngine.UI.Image E_ConfimImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ConfimImage == null )
     			{
		    		this.m_E_ConfimImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/E_Confim");
     			}
     			return this.m_E_ConfimImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ConfimButton = null;
			this.m_E_ConfimImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_ConfimButton = null;
		private UnityEngine.UI.Image m_E_ConfimImage = null;
		public Transform uiTransform = null;
	}
}
