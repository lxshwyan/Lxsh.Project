using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQrCode
{
    public class Book
    {
        /// <summary>
        /// ID标识符
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        public string Press { get; set; }

        /// <summary>
        /// 国际标准图书编号
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// 下载网址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { get; set; }
    }
}
