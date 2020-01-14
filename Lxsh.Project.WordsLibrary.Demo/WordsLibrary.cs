/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.WordsLibrary.Demo
*文件名： WordsLibrary
*创建人： Lxsh
*创建时间：2019/11/23 15:37:49
*描述
*=======================================================================
*修改标记
*修改时间：2019/11/23 15:37:49
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxsh.Project.WordsLibrary.Demo
{
  public  class WordsLibrary
    {
        /// <summary>
        /// 词库树结构类
        /// </summary>
        public class ItemTree
        {
            public char Item { get; set; }
            public bool IsEnd { get; set; }
            public List<ItemTree> Child { get; set; }
        }

        /// <summary>
        /// 词库树
        /// </summary>
        public ItemTree Library { get; private set; }

        /// <summary>
        /// 敏感词组
        /// </summary>
        public string[] Words { get; protected set; } 
        /// <summary>
        /// 敏感词库
        /// </summary>
        /// <param name="words">敏感词组</param>
        public WordsLibrary(string[] words) 
        {    
            Words = words;
            LoadWords();
            Init();
          
        }

        /// <summary>
        /// 加载 敏感词组，可被重写以自定义 如何加载 敏感词组
        /// </summary>
        public virtual void LoadWords()
        {
        }

        /// <summary>
        /// 词库初始化
        /// </summary>
        private void Init()
        {
            if (Words == null)
                Words = new[] { "" };

            Library = new ItemTree() { Item = 'R', IsEnd = false, Child = CreateTree(Words) };
        }

        /// <summary>
        /// 创建词库树
        /// </summary>
        /// <param name="words">敏感词组</param>
        /// <returns></returns>
        private List<ItemTree> CreateTree(string[] words)
        {
            List<ItemTree> tree = null;

            if (words != null && words.Length > 0)
            {
                tree = new List<ItemTree>();

                foreach (var item in words)
                    if (!string.IsNullOrEmpty(item))
                    {
                        char cha = item[0];

                        ItemTree node = tree.Find(e => e.Item == cha);
                        if (node != null)
                            AddChildTree(node, item);
                        else
                            tree.Add(CreateSingleTree(item));
                    }
            }

            return tree;
        }

        /// <summary>
        /// 创建单个完整树
        /// </summary>
        /// <param name="word">单个敏感词</param>
        /// <returns></returns>
        private ItemTree CreateSingleTree(string word)
        {
            //根节点，此节点 值为空
            ItemTree root = new ItemTree();
            //移动 游标
            ItemTree p = root;

            for (int i = 0; i < word.Length; i++)
            {
                ItemTree child = new ItemTree() { Item = word[i], IsEnd = false, Child = null };
                p.Child = new List<ItemTree>() { child };
                p = child;
            }
            p.IsEnd = true;

            return root.Child.First();
        }

        /// <summary>
        /// 附加分支子树
        /// </summary>
        /// <param name="childTree">子树</param>
        /// <param name="word">单个敏感词</param>
        private void AddChildTree(ItemTree childTree, string word)
        {
            //移动 游标
            ItemTree p = childTree;

            for (int i = 1; i < word.Length; i++)
            {
                char cha = word[i];
                List<ItemTree> child = p.Child;

                if (child == null)
                {
                    ItemTree node = new ItemTree() { Item = cha, IsEnd = false, Child = null };
                    p.Child = new List<ItemTree>() { node };
                    p = node;
                }
                else
                {
                    ItemTree node = child.Find(e => e.Item == cha);
                    if (node == null)
                    {
                        node = new ItemTree() { Item = cha, IsEnd = false, Child = null };
                        child.Add(node);
                        p = node;
                    }
                    else
                        p = node;
                }
            }
            p.IsEnd = true;
        }

    }
}