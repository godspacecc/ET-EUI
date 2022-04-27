using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgServerSystem
	{

		public static void RegisterUIEvent(this DlgServer self)
		{
			self.View.E_ConfirmButton.AddListenerAsync(() => { return self.OnConfirmClickHandler();});
			self.View.E_ServerListLoopHorizontalScrollRect.AddItemRefreshListener((Transform transform, int index) =>
			{
				self.OnScrollItemRefershHandler(transform, index);
			});
		}

		public static void ShowWindow(this DlgServer self, Entity contextData = null)
		{
			int count = self.DomainScene().GetComponent<ServerInfosComponent>().ServerInfoList.Count;
			self.AddUIScrollItems(ref self.ScrollItemServerTests, count);
			self.View.E_ServerListLoopHorizontalScrollRect.SetVisible(true, count);
		}

		public static void HideWindow(this DlgServer self)
		{
			self.RemoveUIScrollItems(ref self.ScrollItemServerTests);
		}

		public static async ETTask OnConfirmClickHandler(this DlgServer self)
		{
			bool isSelect = self.DomainScene().GetComponent<ServerInfosComponent>().CurrentServerdId != 0;
		
			if (!isSelect)
			{
				self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().
						AddFloatTips("请选择区服");
			}
			else
			{
				int errorcode = await LoginHelper.GetRoles(self.DomainScene());
				if (errorcode != ErrorCode.ERR_Success)
				{
					self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().
							AddFloatTips(ErrorConfigCategory.Instance.Get(errorcode).Desc);
					return;
				}
				
				self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Roles);
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Server);
			}
		}

		public static void OnScrollItemRefershHandler(this DlgServer self,Transform transform, int index)
		{
			Scroll_Item_serverTest serverTest = self.ScrollItemServerTests[index].BindTrans(transform);
			ServerInfo serverInfo = self.DomainScene().GetComponent<ServerInfosComponent>().ServerInfoList[index];
			serverTest.EI_serverTestImage.color = serverInfo.Id == self.DomainScene().GetComponent<ServerInfosComponent>().CurrentServerdId ? Color.red : Color.cyan;
			serverTest.E_serverTestTipText.SetText(serverInfo.ServerName);
			serverTest.E_SelectButton.AddListener((() =>
			{
				self.OnSelectServerItemHandler(serverInfo.Id);
			}));
		}

		public static void OnSelectServerItemHandler(this DlgServer self, long serverId)
		{
			self.DomainScene().GetComponent<ServerInfosComponent>().CurrentServerdId = int.Parse(serverId.ToString());
			Log.Debug($"选择了服务器Id:{serverId}");
			self.View.E_ServerListLoopHorizontalScrollRect.RefillCells();
		}
		 

	}
}
