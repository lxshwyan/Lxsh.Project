using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lxsh.Project.RectangleDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private bool JudgeRectangle(Rectangle rectangle1, Rectangle rectangle2)
        {

            ///**
            //* 区域说明：以第5块区域为其中一个矩形初始位置，将其余上下左右等如此分割成9份进行判断
            //* 
            //*   1  |  2  |  3
            //*  ----------------
            //*   4  |  5  |  6
            //*  ----------------
            //*   7  |  8  |  9
            //* 
            //**/
            bool flag = true; //设置标记值，默认为重叠

            //if (rectangle1.X > rectangle2.X + rectangle2.Width || rectangle1.Y > rectangle2.Y + rectangle2.Height) flag=false; //初始点在3，6，7，8，9区域
            //else if (rectangle1.X + rectangle1.Width < rectangle2.X || rectangle1.Y + rectangle1.Height < rectangle2.Y) flag = false; //初始点在1区域
            // /**
            // * 下方注掉的代码用于判断矩形内含，取消注释则内含显示不重叠，即可用于判断是否相交
            // */
            //else if (rectangle1.X > rectangle2.X && rectangle1.Y > rectangle2.Y && rectangle1.X + rectangle1.Width < rectangle2.X + rectangle2.Width && rectangle1.Y + rectangle1.Height < rectangle2.Y + rectangle2.Height) flag = false; //初始点在5区域
            //return flag;

            if (rectangle1.IntersectsWith(rectangle2)) flag=false;

            return flag;
        }

    }
}
