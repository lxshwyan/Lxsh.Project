using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Lxsh.Project.SSOLoginCenter
{
    public static class SerializeExtension
    {
        private static readonly JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Local,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        private static readonly JsonSerializerSettings CamelCaseJsonSerializerSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Local,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };


        public static string ToJson(this object data, bool isIndented, bool isCamalCase)
        {
            if (data == null) return null;
            return JsonConvert.SerializeObject(data, isIndented ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None, isCamalCase ? CamelCaseJsonSerializerSettings : DefaultJsonSerializerSettings);
        }

        public static string ToJson(this object data)
        {
            if (data == null) return null;
            return JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.None, DefaultJsonSerializerSettings);
        }

        public static string ToJsonCamalCase(this object data)
        {
            return ToJson(data, false, true);
        }
        
        /// <summary>
        /// 将XML字符串反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T XmlToObject<T>(this string strJson)
        {
            System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
            try
            {
                byte[] array = Encoding.UTF8.GetBytes(strJson);
                MemoryStream stream = new MemoryStream(array);
                StreamReader reader = new StreamReader(stream);
                //xdoc.LoadXml(strJson);
                //XmlNodeReader reader = new XmlNodeReader(xdoc.DocumentElement);
                XmlSerializer ser = new XmlSerializer(typeof(T));
                object obj = ser.Deserialize(reader);
                return (T)obj;
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 将对象序列化成xml字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToXmlStr<T>(this T t)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            MemoryStream mem = new MemoryStream();
            System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(mem, Encoding.UTF8);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writer, t, ns);
            writer.Close();
            return Encoding.UTF8.GetString(mem.ToArray(), 0, mem.ToArray().Length);
        }

        /// <summary>
        /// 将对象序列化成xml字符串(去掉声明)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToXml<T>(this T obj, string secondNodeName = "")
                where T : class
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            //去除xml声明
            settings.OmitXmlDeclaration = true;
            settings.Encoding = Encoding.Default;
            MemoryStream mem = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(mem, settings))
            {
                //去除默认命名空间xmlns:xsd和xmlns:xsi
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlSerializer formatter = null;
                if (!string.IsNullOrWhiteSpace(secondNodeName))
                {
                    XmlRootAttribute root = new XmlRootAttribute(secondNodeName);
                    formatter = new XmlSerializer(typeof(T), root);
                }
                else
                {
                    formatter = new XmlSerializer(typeof(T));
                }

                formatter.Serialize(writer, obj, ns);

            }

            return Encoding.Default.GetString(mem.ToArray());
        }

        /// <summary>
        /// 将XML字符串反序列化成对象(去掉声明)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<T> GetResultFromServiceXML<T>(string dataOutXml, string nodeNameString) where T : class
        {
            string expDataStr = dataOutXml;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(expDataStr);
            XmlNode x = xmlDocument.SelectSingleNode(nodeNameString);
            List<T> ObjectList = new List<T>() { };
            foreach (XmlNode xmlElement in x.ChildNodes)
            {
                XmlElement e = (XmlElement)xmlElement;
                //expDataStr =e.OuterXml;//获得对象节点内容
                //T xof = expDataStr.ToXmlObject<T>();
                //var item = xof.RobotMap<T, M>();
                //if (item != null)
                //{
                //    ObjectList.Add(item);
                //}
                if (e.Name == typeof(T).Name)
                {
                    expDataStr = e.OuterXml;//获得对象节点内容

                    T xof = expDataStr.XmlToObject<T>();
                    ObjectList.Add(xof);
                }

            }
            return ObjectList;
        }

        /// <summary>
        /// 修改根节点名称
        /// </summary>
        /// <param name="xmlString">xml字符串</param>
        /// <param name="oldNodeName">修改对象节点</param>
        /// <param name="newNodeName">修改后节点名称</param>
        /// <returns></returns>
        public static string UpdateRootNodeName(string xmlString, string newRootNodeName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            XmlElement xmlElem = xmlDocument.DocumentElement;//获取根节点
            xmlDocument.InnerXml = Regex.Replace(xmlDocument.InnerXml, @"(?s)(?<=</?)" + xmlElem.Name + "(?=>)", newRootNodeName);
            return xmlDocument.InnerXml;


        }

        /// <summary>
        ///根节点下添加一个节点
        /// </summary>
        /// <param name="xmlString">xml字符串</param>
        /// <param name="oldNodeName">修改对象节点</param>
        /// <param name="newNodeName">修改后节点名称</param>
        /// <returns></returns>
        public static string AddRootNodeName(string xmlString, string newNodeName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            XmlNode ele = xmlDocument.CreateNode(XmlNodeType.Element, newNodeName, "");
            XmlNode root = xmlDocument.DocumentElement;

            while (root.HasChildNodes)
                ele.AppendChild(root.FirstChild);

            root.PrependChild(ele);
            return xmlDocument.InnerXml;

        }

    }

    public static class EnumExtension
    {
        public static object GetAttributeInfo<T>(this Enum value, string property, Boolean nameInstead = false) where T : Attribute
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            FieldInfo field = type.GetField(name);
            T attribute = Attribute.GetCustomAttribute(field, typeof(T)) as T;

            if (attribute == null && nameInstead == true)
            {
                return name;
            }
            if (attribute == null) return null;
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var pro = properties.Find(property, true);
            if (pro != null)
            {
                return pro.GetValue(attribute);
            }
            return null;
        }
        
        public static List<string> GetEnumIds<T>()
        {
            List<string> ids = new List<string>();
            foreach (int myCode in Enum.GetValues(typeof(T)))
            {
                ids.Add(myCode.ToString());
            }
            return ids;
        }
    }

    public static class LinqExtension
    {
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }

        public static IEnumerable<T> Except<T, V>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, V> keySelector)
        {
            return first.Except(second, new CommonEqualityComparer<T, V>(keySelector));
        }

        public static IEnumerable<T> Intersect<T, V>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, V> keySelector)
        {
            return first.Intersect(second, new CommonEqualityComparer<T, V>(keySelector));
        }

        public static IEnumerable<TResult> FullOuterJoin<TOuter, TInner, TKey, TResult>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner, TResult> resultSelector)
        where TInner : class
        where TOuter : class
        {
            var innerLookup = inner.ToLookup(innerKeySelector);
            var outerLookup = outer.ToLookup(outerKeySelector);

            var innerJoinItems = inner
                .Where(innerItem => !outerLookup.Contains(innerKeySelector(innerItem)))
                .Select(innerItem => resultSelector(null, innerItem));

            return outer
                .SelectMany(outerItem =>
                {
                    var innerItems = innerLookup[outerKeySelector(outerItem)];

                    return innerItems.Any() ? innerItems : new TInner[] { null };
                }, resultSelector)
                .Concat(innerJoinItems);
        }

        public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            return outer.GroupJoin(
                inner,
                outerKeySelector,
                innerKeySelector,
                (o, i) =>
                    new { o = o, i = i.DefaultIfEmpty() })
                    .SelectMany(m => m.i.Select(inn =>
                        resultSelector(m.o, inn)
                        ));

        }

        public static IEnumerable<TResult> RightJoin<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            return inner.GroupJoin(
                outer,
                innerKeySelector,
                outerKeySelector,
                (i, o) =>
                    new { i = i, o = o.DefaultIfEmpty() })
                    .SelectMany(m => m.o.Select(outt =>
                        resultSelector(outt, m.i)
                        ));

        }

        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            //var invokedExpression = Expression.Invoke(second, first.Parameters.Cast<Expression>());
            //return Expression.Lambda<Func<T, bool>>(Expression.And(first.Body, invokedExpression), first.Parameters);

            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            //var invokedExpression = Expression.Invoke(second, first.Parameters.Cast<Expression>());
            //return Expression.Lambda<Func<T, bool>>(Expression.Or(first.Body, invokedExpression), first.Parameters);

            return first.Compose(second, Expression.Or);
        }
    }

    public static class StringExtension
    {
        /// <summary>
        /// 向Url中添加参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AddParam(this string url, string key, string value)
        {
            Regex reg = new Regex(string.Format("{0}=[^&]*", key), RegexOptions.IgnoreCase);
            Regex reg1 = new Regex("[&]{2,}", RegexOptions.IgnoreCase);
            string _url = reg.Replace(url, "");
            if (_url.IndexOf("?") == -1)
                _url += string.Format("?{0}={1}", key, value);//?  
            else
                _url += string.Format("&{0}={1}", key, value);//&  
            _url = reg1.Replace(_url, "&");
            _url = _url.Replace("?&", "?");
            return _url;
        }

        /// <summary>
        /// 获取Url参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string QueryString(this string url, string name)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "http://" + url;
            Uri uri = new Uri(url);
            string queryString = uri.Query;
            NameValueCollection col = GetQueryString(queryString, null, true);
            string searchKey = col[name];
            return searchKey;
        }

        /// <summary>
        /// 将查询字符串解析转换为名值集合.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="encoding"></param>
        /// <param name="isEncoded"></param>
        /// <returns></returns>
        private static NameValueCollection GetQueryString(string queryString, Encoding encoding, bool isEncoded)
        {
            queryString = queryString.Replace("?", "");
            NameValueCollection result = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrEmpty(queryString))
            {
                int count = queryString.Length;
                for (int i = 0; i < count; i++)
                {
                    int startIndex = i;
                    int index = -1;
                    while (i < count)
                    {
                        char item = queryString[i];
                        if (item == '=')
                        {
                            if (index < 0)
                            {
                                index = i;
                            }
                        }
                        else if (item == '&')
                        {
                            break;
                        }
                        i++;
                    }
                    string key = null;
                    string value = null;
                    if (index >= 0)
                    {
                        key = queryString.Substring(startIndex, index - startIndex);
                        value = queryString.Substring(index + 1, (i - index) - 1);
                    }
                    else
                    {
                        key = queryString.Substring(startIndex, i - startIndex);
                    }
                    if (isEncoded)
                    {
                        result[MyUrlDeCode(key, encoding)] = MyUrlDeCode(value, encoding);
                    }
                    else
                    {
                        result[key] = value;
                    }
                    if ((i == (count - 1)) && (queryString[i] == '&'))
                    {
                        result[key] = string.Empty;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 解码URL.
        /// </summary>
        /// <param name="encoding">null为自动选择编码</param>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string MyUrlDeCode(string str, Encoding encoding)
        {
            if (encoding == null)
            {
                Encoding utf8 = Encoding.UTF8;
                //首先用utf-8进行解码                     
                string code = HttpUtility.UrlDecode(str.ToUpper(), utf8);
                //将已经解码的字符再次进行编码.
                string encode = HttpUtility.UrlEncode(code, utf8).ToUpper();
                if (str == encode)
                    encoding = Encoding.UTF8;
                else
                    encoding = Encoding.GetEncoding("gb2312");
            }
            return HttpUtility.UrlDecode(str, encoding);
        }

        public static string ToString(this object obj, string formate)
        {
            if (obj == null) return null;
            if (string.IsNullOrEmpty(formate)) return obj.ToString();
            return string.Format(new AcctNumberFormat(), formate, obj);
        }
    }

    public class CommonEqualityComparer<T, V> : IEqualityComparer<T>
    {
        private Func<T, V> keySelector;

        public CommonEqualityComparer(Func<T, V> keySelector)
        {
            this.keySelector = keySelector;
        }

        public bool Equals(T x, T y)
        {
            return EqualityComparer<V>.Default.Equals(keySelector(x), keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return EqualityComparer<V>.Default.GetHashCode(keySelector(obj));
        }
    }

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }

    public class AcctNumberFormat : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string fmt, object arg, IFormatProvider formatProvider)
        {
            if (arg == null) return null;
            if (string.IsNullOrEmpty(fmt)) return arg.ToString();

            if (arg is long || arg is long?)
                return ((long)arg).ToString(fmt);

            if (arg is double || arg is double?)
                return ((double)arg).ToString(fmt);

            if (arg is DateTime || arg is DateTime?)
                return ((DateTime)arg).ToString(fmt);

            if (arg is short || arg is short?)
                return ((short)arg).ToString(fmt);

            if (arg is int || arg is int?)
                return ((int)arg).ToString(fmt);

            if (arg is Guid || arg is Guid?)
                return ((Guid)arg).ToString(fmt);

            if (arg is decimal || arg is decimal?)
                return ((decimal)arg).ToString(fmt);

            if (arg is TimeSpan || arg is TimeSpan?)
                return ((TimeSpan)arg).ToString(fmt);

            return arg.ToString();
        }
    }
}

