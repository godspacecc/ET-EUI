using System;
using TMPro;

namespace ET
{
	public static class DlgLoginSystem
	{

		public static void RegisterUIEvent(this DlgLogin self)
		{
			self.View.E_LoginButton.AddListenerAsync(() => { return self.OnLoginClickHandler(); });
			//self.View.E_LoginButton.AddListener(() => {  self.OnClickLogin(); });
		}

		public static void OnClickLogin(this DlgLogin self)
		{
			self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().AddFloatTips("测试123123");
		}

		public static void ShowWindow(this DlgLogin self, Entity contextData = null)
		{
			self.View.E_AccountTMP_InputField.text = "Uw123456";
			self.View.E_PassWordTMP_InputField.text = "Uw123456";
		}

		public static async ETTask OnLoginClickHandler(this DlgLogin self)
		{
			try
			{
				int errorcode = await LoginHelper.Login(self.DomainScene(),
					ConstValue.LoginAddress,
					self.View.E_AccountTMP_InputField.GetComponent<TMP_InputField>().text,
					self.View.E_PassWordTMP_InputField.GetComponent<TMP_InputField>().text);
				if (errorcode != ErrorCode.ERR_Success)
				{
					self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().
							AddFloatTips(ErrorConfigCategory.Instance.Get(errorcode).Desc);
					return;
				}

				//显示登录之后的逻辑
				errorcode = await LoginHelper.GetServerInfos(self.ZoneScene());
				if (errorcode != ErrorCode.ERR_Success)
				{
					Log.Error(errorcode.ToString());
					return;
				}

				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Login);
				self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Server);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

		public static void HideWindow(this DlgLogin self)
		{

		}

	}
}
