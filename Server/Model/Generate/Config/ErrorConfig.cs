using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class ErrorConfigCategory : ProtoObject, IMerge
    {
        public static ErrorConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, ErrorConfig> dict = new Dictionary<int, ErrorConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<ErrorConfig> list = new List<ErrorConfig>();
		
        public ErrorConfigCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            ErrorConfigCategory s = o as ErrorConfigCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            foreach (ErrorConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public ErrorConfig Get(int id)
        {
            this.dict.TryGetValue(id, out ErrorConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (ErrorConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, ErrorConfig> GetAll()
        {
            return this.dict;
        }

        public ErrorConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class ErrorConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		[ProtoMember(1)]
		public int Id { get; set; }
		/// <summary>描述</summary>
		[ProtoMember(2)]
		public string Desc { get; set; }

	}
}
