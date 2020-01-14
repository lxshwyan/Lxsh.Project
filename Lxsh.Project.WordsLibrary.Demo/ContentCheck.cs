/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.WordsLibrary.Demo
*文件名： ContentCheck
*创建人： Lxsh
*创建时间：2019/11/23 15:38:33
*描述
*=======================================================================
*修改标记
*修改时间：2019/11/23 15:38:33
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.WordsLibrary.Demo
{
  public  class ContentCheck
    {
        /// <summary>
        /// 检测文本
        /// </summary>
        public string Text { private get; set; }

        /// <summary>
        /// 敏感词库 词树
        /// </summary>
        public WordsLibrary.ItemTree Library { private get; set; }

        /// <summary>
        /// 敏感词检测
        /// </summary>
        public ContentCheck() { }

        /// <summary>
        /// 敏感词检测
        /// </summary>
        /// <param name="library">敏感词库</param>
        public ContentCheck(WordsLibrary library)
        {
            if (library.Library == null)
                throw new Exception("敏感词库未初始化");

            Library = library.Library;
        }

        /// <summary>
        /// 敏感词检测
        /// </summary>
        /// <param name="library">敏感词库</param>
        /// <param name="text">检测文本</param>
        public ContentCheck(WordsLibrary library, string text) : this(library)
        {
            if (text == null)
                throw new Exception("检测文本不能为null");

            Text = text;
        }

        /// <summary>
        /// 检测敏感词
        /// </summary>
        /// <param name="text">检测文本</param>
        /// <returns></returns>
        private Dictionary<int, char> WordsCheck(string text)
        {
            if (Library == null)
                throw new Exception("未设置敏感词库 词树");

            Dictionary<int, char> dic = new Dictionary<int, char>();
            WordsLibrary.ItemTree p = Library;
            List<int> indexs = new List<int>();

            for (int i = 0, j = 0; j < text.Length; j++)
            {
                char cha = text[j];
                var child = p.Child;

                var node = child.Find(e => e.Item == cha);
                if (node != null)
                {
                    indexs.Add(j);
                    if (node.IsEnd || node.Child == null)
                    {
                        if (node.Child != null)
                        {
                            int k = j + 1;
                            if (k < text.Length && node.Child.Exists(e => e.Item == text[k]))
                            {
                                p = node;
                                continue;
                            }
                        }

                        foreach (var item in indexs)
                            dic.Add(item, text[item]);

                        indexs.Clear();
                        p = Library;
                        i = j;
                        ++i;
                    }
                    else
                        p = node;
                }
                else
                {
                    indexs.Clear();
                    if (p.GetHashCode() != Library.GetHashCode())
                    {
                        ++i;
                        j = i;
                        p = Library;
                    }
                    else
                        i = j;
                }
            }

            return dic;
        }

        /// <summary>
        /// 替换敏感词
        /// </summary>
        /// <param name="library">敏感词库</param>
        /// <param name="text">检测文本</param>
        /// <param name="newChar">替换字符</param>
        /// <returns></returns>
        public static string SensitiveWordsReplace(WordsLibrary library, string text, char newChar = '*')
        {
            Dictionary<int, char> dic = new ContentCheck(library).WordsCheck(text);
            if (dic != null && dic.Keys.Count > 0)
            {
                char[] chars = text.ToCharArray();
                foreach (var item in dic)
                    chars[item.Key] = newChar;

                text = new string(chars);
            }

            return text;
        }

        /// <summary>
        /// 替换敏感词
        /// </summary>
        /// <param name="text">检测文本</param>
        /// <param name="newChar">替换字符</param>
        /// <returns></returns>
        public string SensitiveWordsReplace(string text, char newChar = '*')
        {
            Dictionary<int, char> dic = WordsCheck(text);
            if (dic != null && dic.Keys.Count > 0)
            {
                char[] chars = text.ToCharArray();
                foreach (var item in dic)
                    chars[item.Key] = newChar;

                text = new string(chars);
            }

            return text;
        }

        /// <summary>
        /// 替换敏感词
        /// </summary>
        /// <param name="newChar">替换字符</param>
        /// <returns></returns>
        public string SensitiveWordsReplace(char newChar = '*')
        {
            if (Text == null)
                throw new Exception("未设置检测文本");

            return SensitiveWordsReplace(Text, newChar);
        }

        /// <summary>
        /// 查找敏感词
        /// </summary>
        /// <param name="library">敏感词库</param>
        /// <param name="text">检测文本</param>
        /// <returns></returns>
        public static List<string> FindSensitiveWords(WordsLibrary library, string text)
        {
            ContentCheck check = new ContentCheck(library, text);
            return check.FindSensitiveWords();
        }

        /// <summary>
        /// 查找敏感词
        /// </summary>
        /// <param name="text">检测文本</param>
        /// <returns></returns>
        public List<string> FindSensitiveWords(string text)
        {
            Dictionary<int, char> dic = WordsCheck(text);
            if (dic != null && dic.Keys.Count > 0)
            {
                int i = -1;
                string str = "";
                List<string> list = new List<string>();
                foreach (var item in dic)
                {
                    if (i == -1 || i + 1 == item.Key)
                        str += item.Value;
                    else
                    {
                        list.Add(str);
                        str = "" + item.Value;
                    }

                    i = item.Key;
                }
                list.Add(str);

                return list.Distinct().ToList();
            }
            else
                return null;
        }

        /// <summary>
        /// 查找敏感词
        /// </summary>
        /// <returns></returns>
        public List<string> FindSensitiveWords()
        {
            if (Text == null)
                throw new Exception("未设置检测文本");

            return FindSensitiveWords(Text);
        }

    }
}