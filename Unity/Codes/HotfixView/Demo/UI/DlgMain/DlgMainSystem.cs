using System.Collections;
using System.Collections.Generic;
using System;
using ET.EventType;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgMainSystem
	{

		public static void RegisterUIEvent(this DlgMain self)
		{
			//self.View.E_HeroesButton.AddListenerAsync(() => { return self.OnRoleButtonClickHandler();});
			self.View.E_HeroesButton.AddListener(()=>{ 
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Main);
				self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_SelectCharacter); 
			});
			self.View.E_ShopButton.AddListener(()=>{ 
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Main);
				self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Shop); 
			});
			self.View.E_InventoryButton.AddListener(()=>{ 
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Main);
				self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Equipment); 
			});
		}

		public static void ShowWindow(this DlgMain self, Entity contextData = null)
		{
			self.Refresh().Coroutine();
		}

		public static async ETTask Refresh(this DlgMain self)
		{
			Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
			NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
			
			self.View.E_LevelTextMeshProUGUI.SetText($"Lv.{numericComponent.GetAsInt((int)NumericType.Hp)}");

			await ETTask.CompletedTask;
		}

		public static async ETTask OnRoleButtonClickHandler(this DlgMain self)
		{
			//打开角色信息界面
			await ETTask.CompletedTask;
		}
	}
}
