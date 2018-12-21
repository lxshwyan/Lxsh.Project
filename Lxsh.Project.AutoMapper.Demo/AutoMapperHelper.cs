/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.AutoMapper.Demo
*文件名： AutoMapperHelper
*创建人： Lxsh
*创建时间：2018/12/20 12:26:18
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/20 12:26:18
*修改人：Lxsh
*描述：
************************************************************************/
using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Lxsh.Project.AutoMapper.Demo
{
 
        /// <summary>
        /// AutoMapper扩展帮助类
        /// </summary>
        public static class AutoMapperHelper
        {
            /// <summary>
            ///  类型映射
            /// </summary>
            public static T MapTo<T>(this object obj)
            {
                if (obj == null) return default(T);
             //   Mapper.CreateMap(obj.GetType(), typeof(T));
                return Mapper.Map<T>(obj);
            }
            /// <summary>
            /// 集合列表类型映射
            /// </summary>
            public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
            {
                foreach (var first in source)
                {
                    var type = first.GetType();
                //    Mapper.CreateMap(type, typeof(TDestination));
                    break;
                }
                return Mapper.Map<List<TDestination>>(source);
            }
            /// <summary>
            /// 集合列表类型映射
            /// </summary>
            public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
            {
                //IEnumerable<T> 类型需要创建元素的映射
            //    Mapper.CreateMap<TSource, TDestination>();
                return Mapper.Map<List<TDestination>>(source);
            }
            /// <summary>
            /// 类型映射
            /// </summary>
            public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
                        where TSource : class
                        where TDestination : class
            {
                if (source == null) return destination;
                //Mapper.CreateMap<TSource, TDestination>();
                return Mapper.Map(source, destination);
            }
            /// <summary>
            /// DataReader映射
            /// </summary>
            public static IEnumerable<T> DataReaderMapTo<T>(this IDataReader reader)
            {
                Mapper.Reset();
              //  Mapper.CreateMap<IDataReader, IEnumerable<T>>();
                return Mapper.Map<IDataReader, IEnumerable<T>>(reader);
            }
        }
    }