
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgEquipmentViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image EquipFrameEmptyImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EquipFrameEmptyImage == null )
     			{
		    		this.m_EquipFrameEmptyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Equipment/Left_Panel/Character/EquipSlot_R/EquipFrameEmpty");
     			}
     			return this.m_EquipFrameEmptyImage;
     		}
     	}

		public UnityEngine.UI.Image EquipFrameDimImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EquipFrameDimImage == null )
     			{
		    		this.m_EquipFrameDimImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Equipment/Left_Panel/Character/EquipSlot_R/EquipFrameDim");
     			}
     			return this.m_EquipFrameDimImage;
     		}
     	}

		public UnityEngine.UI.Image EquipFrameEmpty1Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EquipFrameEmpty1Image == null )
     			{
		    		this.m_EquipFrameEmpty1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Equipment/Left_Panel/Character/EquipSlot_L/EquipFrameEmpty1");
     			}
     			return this.m_EquipFrameEmpty1Image;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EquipFrameEmptyImage = null;
			this.m_EquipFrameDimImage = null;
			this.m_EquipFrameEmpty1Image = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_EquipFrameEmptyImage = null;
		private UnityEngine.UI.Image m_EquipFrameDimImage = null;
		private UnityEngine.UI.Image m_EquipFrameEmpty1Image = null;
		public Transform uiTransform = null;
	}
}
