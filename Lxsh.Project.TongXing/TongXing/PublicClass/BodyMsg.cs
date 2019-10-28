/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：SFBR_Socket.PublicClass
*文件名： BodyMsg
*创建人： Lxsh
*创建时间：2019/8/27 17:40:58
*描述
*=======================================================================
*修改标记
*修改时间：2019/8/27 17:40:58
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFBR_Socket.PublicClass
{
  public  class BodyMsg
    {
        public MsgType MsgType { get; set; }
        public object MsgContent { get; set; }
    }
    public enum MsgType
    {
        /// <summary>
        /// 在线数量
        /// </summary>
        OnLineCount=0,
        /// <summary>
        /// 普通消息
        /// </summary>
        NormalMsg = 1,
        /// <summary>
        /// 主题信息
        /// </summary>
        TitleMsg = 2   
    }
}