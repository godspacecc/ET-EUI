using System;
using System.Collections.Generic;

namespace ET
{
	public  class DlgTips :Entity,IAwake,IUILogic
	{
		public DlgTipsViewComponent View { get => this.Parent.GetComponent<DlgTipsViewComponent>();}
		public long CreateBoxTimer;
		public Queue<string> FloatTips = new Queue<string>();
		public List<ES_ErrorDefault> ErrorDefaults = new List<ES_ErrorDefault>();
	}
}
