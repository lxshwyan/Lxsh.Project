/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.Demo
*文件名： GenericClass
*创建人： Lxsh
*创建时间：2018/12/18 10:27:18
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/18 10:27:18
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Demo
{   
    public class GenericClass
    {
        public void Test()
        {
            {
                Arror arror = new Arror();
                Bird bird = new Arror();
            }
            {
                //协变：接口泛型参数加了个out，就是为了解决刚才的不和谐
                ICustomerListOut<Arror> customerList1 = new CustomerListOut<Arror>();
                ICustomerListOut<Bird> customerList2 = new CustomerListOut<Arror>();
            }
            {//逆变
                ICustomerListIn<Arror> customerList2 = new CustomerList<Arror>();
                ICustomerListIn<Arror> customerList1 = new CustomerList<Bird>();

                //customerList1.Show()

                ICustomerListIn<Bird> birdList1 = new CustomerList<Bird>();
                birdList1.Show(new Arror());
                birdList1.Show(new Bird());

                Action<Arror> act = new Action<Bird>((Bird i) => { });
            }


        }
    }
    public class Bird
    {
        string Name { get; }
    }
   
    public class Arror : Bird
    {
         
        public int Id { get; set; }

        public string Name => "麻雀";
      
        public void Test()
        {

        }
    }

    /// <summary>
    ///  逆变：只能修饰传入参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomerListIn<in T>
    {
        void Show(T t);
    }

    public class CustomerList<T> : ICustomerListIn< T>
    {
        public void Show(T t)
        {
            Console.WriteLine("test in");
        }
    }
    /// <summary>
    /// out 协变 只能是返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomerListOut<out T>
    {
        T Get();
    }
    public class CustomerListOut<T> : ICustomerListOut< T>
    {
        public T Get()
        {
            return default(T);
        }
    }
}