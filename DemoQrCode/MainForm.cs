using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoQrCode
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            List<Book> lstBooks = new List<Book>() {
                new Book { ID=1, Name="克鲁苏神话", Author="H.P.洛夫克拉夫特", Press="浙江文艺出版社", ISBN="9787533951115", Url="https://item.jd.com/12287342.html", Introduction="假设你的脚边有一只蚂蚁在爬，你不会在意有没有踩死它，因为它太渺小了，是死还是活，对你来说没有分毫影响。在\"克苏鲁神话\"中描述的远古邪神的眼中，人类就是那只蚂蚁。" },
                new Book { ID=2, Name="狗样的假期", Author="孟瑶", Press="中国友谊出版公司", ISBN="9787505742000", Url="https://item.jd.com/12277846.html", Introduction="马谷子是一家公司的老板，冷漠自私，一心追求成功，终日疲于奔命，却一点都不快乐。安静下来时，他也在感慨自己活的不如狗，狗狗可以傻吃糊涂睡一点不累。念头闪过时，正赶上流星划过夜空，于是他的愿望就被抽中实现了。" },
                new Book { ID=3, Name="使女的故事", Author="玛格丽特·阿特伍德", Press="上海译文出版社", ISBN="9787532776337", Url="https://item.jd.com/12280426.html", Introduction="奥芙弗雷德是基列共和国的一名使女。她是这个国家中为数不多能够生育的女性之一，被分配到没有后代的指挥官家庭，帮助他们生育子嗣。和这个国家里的其他女性一样，她没有行动的自由，被剥夺了财产、工作和阅读的权利。除了某些特殊的日子，使女们每天只被允许结伴外出一次购物，她们的一举一动都受到“眼目”的监视。" },
                new Book { ID=4, Name="万物有灵", Author="领读文化", Press="北京时代华文书局", ISBN="9787569919127", Url="https://item.jd.com/12266889.html", Introduction="《万物有灵：诗经 里的草木鸟兽鱼虫》收录了日本江户时代儒学家细井徇所绘《诗经名物图解》中的196幅名物彩色图谱，同时配以215名物的习性、状态等辅助文字以及130多首对应的《诗经》篇目原文；" },
                new Book { ID=5, Name="魔鬼在你身后", Author="丹·西蒙斯", Press="江苏凤凰文艺出版社", ISBN="9787559402189", Url="https://item.jd.com/12270014.html", Introduction="你所做的一切，真的是出于自己的意志吗？也许在你身边，就有一些这样的人：他们对你吹毛求疵，指手画脚；甚至会喜怒无常，同你争执不下，把自己的意志强加于你。你会听他们的话吗？魔鬼就生活在我们当中，而这就是关于他们的史诗。要杀死魔鬼，先杀死你心中的恐惧。" },
                new Book { ID=6, Name="琅琊榜", Author="海宴", Press="四川文艺出版社", ISBN="9787541148613", Url="https://item.jd.com/12261573.html", Introduction="一卷风云琅琊榜，囊尽天下奇英才。他远在江湖，却名动帝辇，只因神秘莫测而又言出必准的琅琊阁，突然断言他是“麒麟之才”，“得之可得天下”。然而，身为太子与誉王竞相拉拢招揽的对象，他竟然出人意料地舍弃了这两个皇位争夺的热门人选，转而投向默默无闻、不受皇帝宠爱的靖王。" },
                new Book { ID=7, Name="浮生六记", Author="沈复", Press="天津人民出版社", ISBN="9787201094014", Url="https://item.jd.com/11757204.html", Introduction="《浮生六记》是清代文人沈复写作的自传散文。因其以真言述真情，从不刻意造作，得以浑然天成，独树一帜，达\"乐而不淫，哀而不伤\"之境界，深为后世文人所推崇，流传至今，已成经典。" }
            };
            this.dgBooks.AutoGenerateColumns = false;
            this.bsBooks.DataSource = lstBooks;
            this.bnBooks.BindingSource = bsBooks;
            this.dgBooks.DataSource = bsBooks;
        }

        private void dgBooks_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dgBooks.Rows)
            {
                if (row.Selected)
                {
                    Book book = row.DataBoundItem as Book;
                    this.txtName.Text = book.Name;
                    this.txtAuthor.Text = book.Author;
                    this.txtPress.Text = book.Press;
                    this.txtIntroduction.Text = book.Introduction;
                    Bitmap image1= BarcodeHelper.Generate2(book.ISBN, this.pbIsbn.Width, this.pbIsbn.Height);
                    image1.Save(Application.StartupPath + "\\img1\\" + book.Name + ".jpg", ImageFormat.Jpeg);//自己创建一个文件夹，放入生成的图片（根目录下）
                     this.pbIsbn.Image = image1;
                    Bitmap image2 = BarcodeHelper.Generate1(book.Url, this.pbUrl.Width, this.pbUrl.Height);
                    image2.Save(Application.StartupPath + "\\img2\\" + book.Name + ".jpg", ImageFormat.Jpeg);//自己创建一个文件夹，放入生成的图片（根目录下）
                    this.pbUrl.Image = image2;
                    Bitmap image3 = BarcodeHelper.Generate3(book.Url, this.pbUrl2.Width, this.pbUrl2.Height);
                    image3.Save(Application.StartupPath + "\\img3\\" + book.Name + ".jpg", ImageFormat.Jpeg);//自己创建一个文件夹，放入生成的图片（根目录下）
                    this.pbUrl2.Image = image3;
                }
            }
        }
    }
}
