using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Lxsh.Project.JobsClass
{
    public static class XmlHelper
    {
		public static TaskList ReadXMLFile()
		{
			TaskList taskList = new TaskList();
			string xmlPath = ConfigurationManager.AppSettings["xmlPath"].ToString();
			string fileName = AppDomain.CurrentDomain.BaseDirectory + xmlPath;
			string requestStr = string.Empty;
			if (File.Exists(fileName))
			{
				XmlDocument document = new XmlDocument();
				document.Load(fileName);
				requestStr = document.OuterXml;
				StringReader sr = new StringReader(requestStr);
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskList));
				taskList = xmlSerializer.Deserialize(sr) as TaskList;
			}
			return taskList;
		}
		
	
	}
	/// <summary>
	/// 用于多个任务的适配解析
	/// </summary>
	[XmlRoot(ElementName = "TaskList")]
	public class TaskList
	{
		[XmlElement(ElementName = "TaskConfigModel")]
		public List<TaskConfigModel> taskConfigModel { get; set; }
	}
	public class TaskConfigModel
	{
		/// <summary>
		/// 分组名称
		/// </summary>
		public string GroupName { get; set; }
		/// <summary>
		/// Job名称
		/// </summary>
		public string JobName { get; set; }
		/// <summary>
		/// 触发器名称
		/// </summary>
		public string TriggerName { get; set; }
		/// <summary>
		/// 时间间隔类型
		/// </summary>
		public int TimeIntervalType { get; set; }
		/// <summary>
		/// 时间间隔
		/// </summary>
		public int TimeInterval { get; set; }
		/// <summary>
		/// 时，TimeIntervalTypeEnum.DailyTime 时有效
		/// </summary>
		public int Hours { get; set; }
		/// <summary>
		/// 分，TimeIntervalTypeEnum.DailyTime 时有效
		/// </summary>
		public int Minutes { get; set; }
		/// <summary>
		/// JOB对应的class
		/// </summary>
		public string JobClass { get; set; }

		/// <summary>
		/// 是否启用
		/// </summary>
		public bool Enable { get; set; }
	}
}
