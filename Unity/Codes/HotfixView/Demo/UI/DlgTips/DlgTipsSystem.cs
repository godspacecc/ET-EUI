using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[Timer(TimerType.ShowTipBoxTimer)]
	public class PopTipBoxTime: ATimer<DlgTips>
	{
		public override void Run(DlgTips self)
		{
			try
			{
				self.ShowTipBoxTimer();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
	}

	[FriendClass(typeof(DlgTips))]
	public static class DlgTipsSystem
	{
		public static void RegisterUIEvent(this DlgTips self)
		{
			//self.RegisterCloseEvent(self.View);
		}

		public static void ShowWindow(this DlgTips self, Entity contextData = null)
		{
			
		}

		public static void AddFloatTips(this DlgTips self, string text)
		{
			self.FloatTips.Enqueue(text);
			self.PushAction();
		}

		public static void PushAction(this DlgTips self)
		{
			if (self.CreateBoxTimer == 0)
			{
				self.CreateBoxTimer = TimerComponent.Instance.NewRepeatedTimer(100, TimerType.ShowTipBoxTimer, self);
			}
		}

		public static async void ShowTipBoxTimer(this DlgTips self)
		{
			if (self.FloatTips.Count > 0)
			{
				string text = self.FloatTips.Dequeue();
				//获取盒子
				GameObject go = GameObjectPoolHelper.GetObjectFromPool("ES_ErrorDefault", true, 5);
				if (go == null)
				{
					Log.Error("检测到无法获取资源");
					return;
				}
				//设置盒子父节点
				go.transform.SetParent(self.View.E_Popup_MessageRectTransform, false);
				ES_ErrorDefault errorDefault = ObjectPool.Instance.Fetch(typeof(ES_ErrorDefault)) as ES_ErrorDefault;
				errorDefault.uiTransform = go.transform;
				errorDefault.E_MessageTextMeshProUGUI.text = text;
				self.ErrorDefaults.Add(errorDefault);
				//动态变化盒子大小 1.1-1-0.4
				go.transform.DOScale(1.1f, 0.4f);

				if (self.ErrorDefaults.Count > 3)
				{
					Log.Debug("正在提前回收");
					self.OnTimeUp(self.ErrorDefaults[0]);
				}
				
				 //动太移动盒子位置
				 for (int i = 0; i < self.ErrorDefaults.Count; i++)
				 {
				 	float y = self.GetHeight(i);
				 	Log.Debug(y.ToString());
				 	self.ErrorDefaults[i].uiTransform.DOLocalMove(new Vector3(go.transform.localPosition.x, y, 0), 0.08f);
				 }
				 //设置3.5秒后删除盒子
				await TimerComponent.Instance.WaitAsync(3500);
				self.OnTimeUp(errorDefault);
			}
			else
			{
				TimerComponent.Instance.Remove(ref self.CreateBoxTimer);
			}
		}

		private static float GetHeight(this DlgTips self, int idx)
		{
			float y = 0;
			for (int i = 0; i < self.ErrorDefaults.Count - idx; i++)
			{
				y = y + self.ErrorDefaults[i].uiTransform.GetComponent<RectTransform>().rect.height;
				Log.Debug(y.ToString());
			}

			return y;
		}

		private static void OnTimeUp(this DlgTips self, ES_ErrorDefault errorDefault)
		{
			if (self.ErrorDefaults.Contains(errorDefault))
			{
				Log.Debug("正在回收");
				self.ErrorDefaults.Remove(errorDefault);
				GameObjectPoolHelper.ReturnObjectToPool(errorDefault.uiTransform.gameObject);
				ObjectPool.Instance.Recycle(errorDefault);
			}
		}
	}
}
