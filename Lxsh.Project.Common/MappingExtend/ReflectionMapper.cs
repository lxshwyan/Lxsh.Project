using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Common.MappingExtend
{
    public class ReflectionMapper
    {
        /// <summary>
        /// 反射
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="tIn"></param>
        /// <returns></returns>
        public static TOut Trans<TIn, TOut>(TIn tIn)
        {
            TOut tOut = Activator.CreateInstance<TOut>();
            foreach (var itemOut in tOut.GetType().GetProperties())
            {
                foreach (var itemIn in tIn.GetType().GetProperties())
                {
                    if (itemOut.Name.Equals(itemIn.Name))
                    {
                        itemOut.SetValue(tOut, itemIn.GetValue(tIn));
                        break;
                    }
                }
            }
            foreach (var itemOut in tOut.GetType().GetFields())
            {
                foreach (var itemIn in tIn.GetType().GetFields())
                {
                    if (itemOut.Name.Equals(itemIn.Name))
                    {
                        itemOut.SetValue(tOut, itemIn.GetValue(tIn));
                        break;
                    }
                }
            }
            return tOut;
        }
    }
}
