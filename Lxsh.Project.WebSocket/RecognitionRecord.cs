using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.WebSocketDemo
{
    public class RecognitionRecord
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public string avatar { get; set; }
        public int direction { get; set; }
        public float verifyScore { get; set; }
        public int receptionUserId { get; set; }
        public string receptionUserName { get; set; }
        public Group[] groups { get; set; }
        public string deviceName { get; set; }
        public string sn { get; set; }
        public string signDate { get; set; }
        public int signTime { get; set; }
        public string signAvatar { get; set; }
        public string signBgAvatar { get; set; }
        public string mobile { get; set; }
        public string icNumber { get; set; }
        public string idNumber { get; set; }
        public string jobNumber { get; set; }
        public string remark { get; set; }
        public int entryMode { get; set; }
        public string signTimeZone { get; set; }
        public string docPhoto { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string address { get; set; }
        public string location { get; set; }
        public int abnormalType { get; set; }
        public string userIcNumber { get; set; }
        public string userIdNumber { get; set; }
    }

    public class Group
    {
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
    }

    public class Result
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public string Desc { get; set; }

        public RecognitionRecord Data { get; set; }
    }
}


