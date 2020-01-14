
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;

namespace SFBR.RabbitMQ
{
    public enum CategoryMessage
    {   
        Alarm ,//报警
        Door,   //门禁
        System,//系统
        DoorSate, //门禁状态信息
        Supervision,//督查
        DailyManagement,//日常
        Information,//信息报备
        Intelligence,//要情
        Text,// 文本信息
        Counting, //人员工具清点信息
        ABDoor,   //AB门
        Unknown  //未知信息

    }
    [Queue("SFBR.RabbitMQ.MessagesQueue", ExchangeName = "SFBR.RabbitMQ.Exchange")]
    public class RabbitMQMsg
    {  
       public RabbitMQMsg() { }
        /// <summary>
        /// 消息事件ID （建议唯一guid）
        /// </summary>
        public string EventId { get; set; }
        /// <summary>
        /// 消息名称
        /// </summary>
        public string EventName { get; set; }
        /// <summary>
        /// 消息等级
        /// </summary>
        public int EventLevel { get; set; }
        /// <summary>
        /// 消息来源ID
        /// </summary>
        public string SourceId { set; get; }
        /// <summary>
        /// 消息来源名称
        /// </summary>
        public string SourceName { set; get; }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string EventDst { set; get; }
        /// <summary>
        /// 消息类别
        /// </summary>
       public CategoryMessage EventCategory { set; get; }
        /// <summary>
        /// 消息发送时间
        /// </summary>
       public string EventSendTime { set; get; }
        /// <summary>
        /// 消息内容
        /// </summary>
       public string Body { set; get; }   
    }

    /*
  <SystemMessage>
  <Type>类型</Type>
  <Title>标题</Title>
  <Content>内容</Content>
  <DateTime>时间</DateTime>
  </SystemMessage>
  */
    public class SystemMessage
    {
        public string Type { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }
        public string DateTime { set; get; }
    }
    /*
     <DoorMessage>
    <DoorID>流水号</DoorID>
    <AssertID>设备编号</AssertID>
    <AssertName>设备名称</AssertName>
    <CardID>卡号</CardID>
    <Address>地址</Address>
    <DateTime>刷卡时间</DateTime>
    <Photo>人员照片</Photo>
    <Dept>所属部门</Dept>
    <JY>监狱名称</JY>
    <Expression>联动摄像机</Expression>
    <TypeID>1表示设备状态，2表示外来人申请 ，3表示外来人复核通知，4表示车辆放行申请，5表示放行结果通知，6提醒数据更新</TypeID>
    <AssertID>设备ID</AssertID>
    <AssertTypeID>设备类型</AssertTypeID>
    <AssertGroupID>上级资产ID</AssertGroupID>
    <DevType>1 人行A门，2车行A门，3防撞桩，4破胎器，5生命探测仪</DevType>
    <StatuesID>状态</StatuesID>
    <CheckResult>1表示通过，2表示未通过</CheckResult>
    </DoorMessage>
    CmdSituationMessage：要情消息
    */   
    public class DoorMessage
    {
        public string DoorID { set; get; }
        public string AssertID { set; get; }
        public string AssertName { set; get; }
        public string CardID { set; get; }
        public string Address { set; get; }
        public string DateTime { set; get; }
        public string Photo { set; get; }
        public string Dept { set; get; }
        public string JY { set; get; }
        public string Expression { set; get; }
        public int TypeID { set; get; }
        public int AssertTypeID;
        public string AssertGroupID { set; get; }
        public int DevType { set; get; }
        public string StatuesID { set; get; }
        public int CheckResult { set; get; }
    }

    ///*
    // * 
    // <InspectorMessage>
    //<Type>督查(督促、刷新等等)</Type>
    //<SeqNo>编号</SeqNo>
    //<Content>内容</Content>
    //<DateTime>时间</DateTime>
    //</InspectorMessage>
    // * /
    public class InspectorMessage
    {
        public string Type { set; get; }
        public string SeqNo { set; get; }
        public string Content { set; get; }
        public string DateTime { set; get; }
    }

    /*
    <AlarmMessage>
    <AlarmID>流水号</AlarmID>
    <AssertID>设备编号</AssertID>
    <AssertName>设备名称</AssertName>
    <AssertTypeID>设备类型编号</AssertTypeID>
    <TypeName>设备类型名称</TypeName>
    <AlarmCode>报警类型编码</AlarmCode>
    <AlarmCodeName>报警类型名称</AlarmCodeName>
    <AlarmValue>报警内容</AlarmValue>
    <AlarmTime>报警时间</AlarmTime>
    <GroupID>报警区域编号</GroupID>
    <GroupName>报警区域名称</GroupName>
    <GYD>关押点名称</GYD>
    <JY>监狱名称</JY>
    <Expression>联动摄像机</Expression>
    </AlarmMessage>
    */

    public class AlarmMessage
    {
        public string AlarmID { set; get; }
        public string AssertID { set; get; }
        public string AssertName { set; get; }
        public string AssertTypeID { set; get; }
        public string TypeName { set; get; }
        public string AlarmCode { set; get; }
        public string AlarmCodeName { set; get; }
        public string AlarmValue { set; get; }
        public string AlarmTime { set; get; }
        public string GroupID { set; get; }
        public string GroupName { set; get; }
        public string GYD { set; get; }
        public string JY { set; get; }
        public string Expression { set; get; }
        public string EmployeeID { set; get; }
    }
    /// <summary>
    /// 日常管理
    /// </summary>
    public class DailyMessage
    {
        public string Type { set; get; }
        public string SeqNo { set; get; }
        public string Content { set; get; }
        public string DateTime { set; get; }
    }
    /// <summary>
    /// 要情日报
    /// </summary>
    public class ReportMessage
    {
        public string Type { set; get; }
        public string SeqNo { set; get; }
        public string Content { set; get; }
        public string DateTime { set; get; }
    }
    /// <summary>
    /// 信息报备
    /// </summary>
    public class InfomationMessage
    {
        public string Type { set; get; }
        public string SeqNo { set; get; }
        public string Content { set; get; }
        public string DateTime { set; get; }
    }

    public class ABDoorMessage
    {

    }
}
