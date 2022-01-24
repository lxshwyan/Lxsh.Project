using System;
using System.Collections.Generic;

namespace Lxsh.Project.DictionaryDemocc
{
    class Program
    {
        static void Main(string[] args)
        {
            var _dictionaryWapper = new DictionaryWapper<string, string>(new Dictionary<string, string> { });
            
            _dictionaryWapper.OnValueChanged += new EventHandler<ValueChangedEventArgs<string>>(OnConfigUsedChanged);
            _dictionaryWapper["name"] = "lxsh";
            _dictionaryWapper["name"] = "lxsh1";
        }
        public static void OnConfigUsedChanged(object sender, ValueChangedEventArgs<string> e)
        {
           
            Console.WriteLine($"字典{e.Key}的值发生变更，请注意...{(sender as DictionaryWapper<string, string>)["name"].ToString()}");
        }
    }
}
