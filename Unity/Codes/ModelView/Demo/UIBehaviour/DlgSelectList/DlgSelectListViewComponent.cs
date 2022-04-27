
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgSelectListViewComponent : Entity,IAwake,IDestroy 
	{
		public TMPro.TMP_InputField EInputTMP_InputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EInputTMP_InputField == null )
     			{
		    		this.m_EInputTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject,"InputBg/EInput");
     			}
     			return this.m_EInputTMP_InputField;
     		}
     	}

		public UnityEngine.UI.Image EInputImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EInputImage == null )
     			{
		    		this.m_EInputImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"InputBg/EInput");
     			}
     			return this.m_EInputImage;
     		}
     	}

		public UnityEngine.UI.Button EConfirmButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EConfirmButton == null )
     			{
		    		this.m_EConfirmButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"InputBg/EConfirm");
     			}
     			return this.m_EConfirmButton;
     		}
     	}

		public UnityEngine.UI.Image EConfirmImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EConfirmImage == null )
     			{
		    		this.m_EConfirmImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"InputBg/EConfirm");
     			}
     			return this.m_EConfirmImage;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_LoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_LoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_LoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"ELoopScrollList_");
     			}
     			return this.m_ELoopScrollList_LoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EInputTMP_InputField = null;
			this.m_EInputImage = null;
			this.m_EConfirmButton = null;
			this.m_EConfirmImage = null;
			this.m_ELoopScrollList_LoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private TMPro.TMP_InputField m_EInputTMP_InputField = null;
		private UnityEngine.UI.Image m_EInputImage = null;
		private UnityEngine.UI.Button m_EConfirmButton = null;
		private UnityEngine.UI.Image m_EConfirmImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_LoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
