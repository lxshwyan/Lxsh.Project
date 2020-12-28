using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Lxsh.Project.NewtonsoftDemo
{
    class Program
    {
        static void Main(string[] args)
        {
             JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
                return settings;
            };

            Console.WriteLine("Hello World!");
            TestSerializeObject();
            TestMapping();
            TestOnDeserialized();

        }
        static void TestSerializeObject()
        {
            var reportModel = new ReportModel()
            {
                ProductName = "法式小众设计感长裙气质显瘦纯白色仙女连衣裙",
                TotalPayment = 100,
               // TotalCustomerCount = 2,
                //TotalProductCount = 333
            };
            var Json = JsonConvert.SerializeObject(reportModel);
            //var Json = JsonConvert.SerializeObject(reportModel,Formatting.Indented);
            //var Json = JsonConvert.SerializeObject(reportModel, Formatting.Indented, new JsonSerializerSettings
            //{
            //    DefaultValueHandling = DefaultValueHandling.Ignore//针对不赋值的可以不序列化
            //}); 
            Console.WriteLine(Json);
        }

        static void TestMapping()
        {
            var json = "{'title':'法式小众设计感长裙气质显瘦纯白色仙女连衣裙','customercount':1000,'totalPayment':100.0,'totalProductCount':10000}";

            var reportModel = JsonConvert.DeserializeObject<ReportModel>(json);

        }
        static void TestOnDeserialized()//提取未知字段
        {
            var json = "{'OrderTitle':'女装大佬', 'Created':'2020/6/23','Memo':'订单备注','Name':'lxsh'}";
            MemoryTraceWriter traceWriter = new MemoryTraceWriter();

            var order = JsonConvert.DeserializeObject<Order>(json,new JsonSerializerSettings() { 
             TraceWriter=traceWriter
            });

            Console.WriteLine(order);
            Console.WriteLine(traceWriter.ToString());
        }

    }
   // [JsonObject(MemberSerialization.OptOut)]
    public class ReportModel
    {
        [JsonProperty("title")]
        public string ProductName { get; set; }
        [JsonProperty("customercount")]
        public int TotalCustomerCount { get; set; }
       // [JsonIgnore]
        public decimal TotalPayment { get; set; }
        public int TotalProductCount { get; set; }
    }
    public class Order
    {
        public string OrderTitle { get; set; }

        public DateTime Created { get; set; }

        [JsonExtensionData]
        private IDictionary<string, JToken> _additionalData;

        public Order()
        {
            _additionalData = new Dictionary<string, JToken>();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            var dict = _additionalData;
            foreach (var item in _additionalData)
            {
                Console.WriteLine($"key={item.Key},value={item.Value}");
            }
        }

        public override string ToString()
        {
            return $"OrderTitle={OrderTitle}, Created={Created}";
        }
    }

}
