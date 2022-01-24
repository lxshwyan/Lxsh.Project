using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.DictionaryDemocc
{
    public class DictionaryWapper<TKey, TValue>
    {
        public object objLock = new object();
        private Dictionary<TKey, TValue> _dict;
        public event EventHandler<ValueChangedEventArgs<TKey>> OnValueChanged;
        public DictionaryWapper(Dictionary<TKey, TValue> dic)
        {
            _dict = dic;
        }
        public TValue this[TKey Key]
        {
            get { return _dict[Key]; }
            set
            {
                lock (objLock)
                {
                    try
                    {
                        if (_dict.ContainsKey(Key) && _dict[Key] != null && !_dict[Key].Equals(value))
                        {
                            OnValueChanged(this, new ValueChangedEventArgs<TKey>(Key));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"检测值变更或者触发值变更事件，发生未知异常{ex}");
                    }
                    finally
                    {
                        _dict[Key] = value;
                    }
                }
            }

        }
    }
    public class ValueChangedEventArgs<TK> : EventArgs
    {
        public TK Key { get; set; }
        public ValueChangedEventArgs(TK key)
        {
            Key = key;
        }
    }
}
