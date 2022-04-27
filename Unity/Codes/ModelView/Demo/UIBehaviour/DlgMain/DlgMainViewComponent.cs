
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgMainViewComponent : Entity,IAwake,IDestroy 
	{
		public TMPro.TextMeshProUGUI E_LevelTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LevelTextMeshProUGUI == null )
     			{
		    		this.m_E_LevelTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"UserLevel_Info/UserLevel/E_Level");
     			}
     			return this.m_E_LevelTextMeshProUGUI;
     		}
     	}

		public TMPro.TextMeshProUGUI E_NameTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NameTextMeshProUGUI == null )
     			{
		    		this.m_E_NameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"UserLevel_Info/UserLevel/E_Name");
     			}
     			return this.m_E_NameTextMeshProUGUI;
     		}
     	}

		public UnityEngine.UI.Button E_ShopButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ShopButton == null )
     			{
		    		this.m_E_ShopButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"HomeMenu/E_Shop");
     			}
     			return this.m_E_ShopButton;
     		}
     	}

		public UnityEngine.UI.Image E_ShopImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ShopImage == null )
     			{
		    		this.m_E_ShopImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"HomeMenu/E_Shop");
     			}
     			return this.m_E_ShopImage;
     		}
     	}

		public UnityEngine.UI.Button E_HeroesButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeroesButton == null )
     			{
		    		this.m_E_HeroesButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"HomeMenu/E_Heroes");
     			}
     			return this.m_E_HeroesButton;
     		}
     	}

		public UnityEngine.UI.Image E_HeroesImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeroesImage == null )
     			{
		    		this.m_E_HeroesImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"HomeMenu/E_Heroes");
     			}
     			return this.m_E_HeroesImage;
     		}
     	}

		public UnityEngine.UI.Button E_InventoryButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InventoryButton == null )
     			{
		    		this.m_E_InventoryButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"HomeMenu/E_Inventory");
     			}
     			return this.m_E_InventoryButton;
     		}
     	}

		public UnityEngine.UI.Image E_InventoryImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InventoryImage == null )
     			{
		    		this.m_E_InventoryImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"HomeMenu/E_Inventory");
     			}
     			return this.m_E_InventoryImage;
     		}
     	}

		public UnityEngine.UI.Button E_GuildButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GuildButton == null )
     			{
		    		this.m_E_GuildButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"HomeMenu/E_Guild");
     			}
     			return this.m_E_GuildButton;
     		}
     	}

		public UnityEngine.UI.Image E_GuildImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GuildImage == null )
     			{
		    		this.m_E_GuildImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"HomeMenu/E_Guild");
     			}
     			return this.m_E_GuildImage;
     		}
     	}

		public UnityEngine.UI.Button E_UpgradeButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpgradeButton == null )
     			{
		    		this.m_E_UpgradeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"HomeMenu/E_Upgrade");
     			}
     			return this.m_E_UpgradeButton;
     		}
     	}

		public UnityEngine.UI.Image E_UpgradeImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpgradeImage == null )
     			{
		    		this.m_E_UpgradeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"HomeMenu/E_Upgrade");
     			}
     			return this.m_E_UpgradeImage;
     		}
     	}

		public UnityEngine.UI.Button E_PlayButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PlayButton == null )
     			{
		    		this.m_E_PlayButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Play");
     			}
     			return this.m_E_PlayButton;
     		}
     	}

		public UnityEngine.UI.Image E_PlayImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PlayImage == null )
     			{
		    		this.m_E_PlayImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Play");
     			}
     			return this.m_E_PlayImage;
     		}
     	}

		public UnityEngine.UI.Button E_BossDungeonButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BossDungeonButton == null )
     			{
		    		this.m_E_BossDungeonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_BossDungeon");
     			}
     			return this.m_E_BossDungeonButton;
     		}
     	}

		public UnityEngine.UI.Image E_BossDungeonImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BossDungeonImage == null )
     			{
		    		this.m_E_BossDungeonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BossDungeon");
     			}
     			return this.m_E_BossDungeonImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_LevelTextMeshProUGUI = null;
			this.m_E_NameTextMeshProUGUI = null;
			this.m_E_ShopButton = null;
			this.m_E_ShopImage = null;
			this.m_E_HeroesButton = null;
			this.m_E_HeroesImage = null;
			this.m_E_InventoryButton = null;
			this.m_E_InventoryImage = null;
			this.m_E_GuildButton = null;
			this.m_E_GuildImage = null;
			this.m_E_UpgradeButton = null;
			this.m_E_UpgradeImage = null;
			this.m_E_PlayButton = null;
			this.m_E_PlayImage = null;
			this.m_E_BossDungeonButton = null;
			this.m_E_BossDungeonImage = null;
			this.uiTransform = null;
		}

		private TMPro.TextMeshProUGUI m_E_LevelTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_NameTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_ShopButton = null;
		private UnityEngine.UI.Image m_E_ShopImage = null;
		private UnityEngine.UI.Button m_E_HeroesButton = null;
		private UnityEngine.UI.Image m_E_HeroesImage = null;
		private UnityEngine.UI.Button m_E_InventoryButton = null;
		private UnityEngine.UI.Image m_E_InventoryImage = null;
		private UnityEngine.UI.Button m_E_GuildButton = null;
		private UnityEngine.UI.Image m_E_GuildImage = null;
		private UnityEngine.UI.Button m_E_UpgradeButton = null;
		private UnityEngine.UI.Image m_E_UpgradeImage = null;
		private UnityEngine.UI.Button m_E_PlayButton = null;
		private UnityEngine.UI.Image m_E_PlayImage = null;
		private UnityEngine.UI.Button m_E_BossDungeonButton = null;
		private UnityEngine.UI.Image m_E_BossDungeonImage = null;
		public Transform uiTransform = null;
	}
}
