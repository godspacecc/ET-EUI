using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgRoles))]
	public static  class DlgRolesSystem
	{

		public static void RegisterUIEvent(this DlgRoles self)
		{
			self.View.E_ConfirmButton.AddListenerAsync(() => { return self.OnConfirmClickHandler(); });
			self.View.E_CreateRoleButton.AddListenerAsync(() => { return self.OnCreateRoleClickHandler(); });
			self.View.E_DeleteRoleButton.AddListenerAsync(() => { return self.OnDeleteRoleClickHandler(); });
			self.View.E_RoleListLoopHorizontalScrollRect.AddItemRefreshListener((Transform transform,int index) => { self.OnRoleListRefreshHandler(transform,index); });
		}

		public static void ShowWindow(this DlgRoles self, Entity contextData = null)
		{
			self.RefreshRoleItems();
		}

		public static void RefreshRoleItems(this DlgRoles self)
		{
			int count = self.DomainScene().GetComponent<RoleInfosComponent>().RoleInfos.Count;
			self.AddUIScrollItems(ref self.ScrollItemRoles, count);
			self.View.E_RoleListLoopHorizontalScrollRect.SetVisible(true, count);
		}

		public static async ETTask OnConfirmClickHandler(this DlgRoles self)
		{
			if (self.DomainScene().GetComponent<RoleInfosComponent>().CurrentRoleId == 0)
			{
				self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().AddFloatTips("请选择要登录的角色");
				return;
			}

			try
			{
				long errorcode = await LoginHelper.GetRelamKey(self.DomainScene());
				if (errorcode != ErrorCode.ERR_Success)
				{
					self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().
							AddFloatTips(ErrorConfigCategory.Instance.Get(errorcode).Desc);
					return;
				}
				
				errorcode = await LoginHelper.EnterGame(self.DomainScene());
				if (errorcode != ErrorCode.ERR_Success)
				{
					self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().
							AddFloatTips(ErrorConfigCategory.Instance.Get(errorcode).Desc);
					return;
				}
				
				self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Main);
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Roles);
			}
			catch (Exception e)
			{
				self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().AddFloatTips(e.ToString());
			}
		}

		public static async ETTask OnCreateRoleClickHandler(this DlgRoles self)
		{
			string name = self.View.E_RoleNameTMP_InputField.text;

			if (string.IsNullOrEmpty(name))
			{
				self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().AddFloatTips("角色名字不能为空");
				return;
			}

			try
			{
				long errorcode = await LoginHelper.CreateRole(self.DomainScene(), name);
				if (errorcode != ErrorCode.ERR_Success)
				{
					self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().
							AddFloatTips(ErrorConfigCategory.Instance.Get(errorcode).Desc);
					return;
				}

				self.RefreshRoleItems();
			}
			catch (Exception e)
			{
				self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().AddFloatTips(e.ToString());
			}
		}
		
		public static async ETTask OnDeleteRoleClickHandler(this DlgRoles self)
		{
			if (self.DomainScene().GetComponent<RoleInfosComponent>().CurrentRoleId == 0)
			{
				self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().AddFloatTips("请选择要删除的角色");
				return;
			}

			try
			{
				long errorcode = await LoginHelper.DeleteRole(self.DomainScene());
				if (errorcode != ErrorCode.ERR_Success)
				{
					self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().
							AddFloatTips(ErrorConfigCategory.Instance.Get(errorcode).Desc);
					return;
				}

				self.RefreshRoleItems();
			}
			catch (Exception e)
			{
				self.DomainScene().GetComponent<UIComponent>().GetDlgLogic<DlgTips>().AddFloatTips(e.ToString());
			}
		}
		
		public static void OnRoleListRefreshHandler(this DlgRoles self, Transform transform, int index)
		{
			Scroll_Item_serverTest serverTest = self.ScrollItemRoles[index].BindTrans(transform);
			RoleInfo roleInfo = self.DomainScene().GetComponent<RoleInfosComponent>().RoleInfos[index];
			serverTest.EI_serverTestImage.color = roleInfo.Id == self.DomainScene().GetComponent<RoleInfosComponent>().CurrentRoleId ? Color.red : Color.cyan;
			serverTest.E_serverTestTipText.SetText(roleInfo.Name);
			serverTest.E_SelectButton.AddListener((() =>
			{
				self.OnSelectRoleItemHandler(roleInfo.Id);
			}));
		}
		
		public static void OnSelectRoleItemHandler(this DlgRoles self, long roleId)
		{
			self.DomainScene().GetComponent<RoleInfosComponent>().CurrentRoleId = roleId;
			Log.Debug($"选择了角色Id:{roleId}");
			self.View.E_RoleListLoopHorizontalScrollRect.RefillCells();
		}
	}
}
