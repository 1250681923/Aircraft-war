using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 打飞机
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitialGame();
        }

        static Random r = new Random();

        /// <summary>
        /// 初始化游戏
        /// </summary>
        public void InitialGame()
        { 
            //首先需要初始化的是我们的背景
            SingleObject.GetSingle().AddGameObject(new BackGround(0, -850, 5));
            //现在开始初始化玩家飞机
            SingleObject.GetSingle().AddGameObject(new PlaneHero(100, 100, 5, 3, Direction.Up));

            //初始化我们的敌人飞机
            InitialPlaneEnemy();
         
        }

        private void InitialPlaneEnemy()
        {
            for (int i = 0; i < 4; i++)
            {
                SingleObject.GetSingle().AddGameObject(new PlaneEnemy(r.Next(0, this.Width), -400, r.Next(0, 2)));
            }
            //不应该每次都出现最大的那个飞机，应该有一个几率出现
            if (r.Next(0, 100) > 80)
            { 
                //百分之二十的几率出现大飞机
                SingleObject.GetSingle().AddGameObject(new PlaneEnemy(r.Next(0, this.Width), -400, 2));
            }
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //当窗体被重新绘制的时候 会执行当前事件
            //窗体被重新绘制的时候 我就绘    制我的背景
            SingleObject.GetSingle().Draw(e.Graphics);
            string score=SingleObject.GetSingle().Score.ToString();
            //绘制玩家的分数
            e.Graphics.DrawString(score, new Font("宋体", 20, FontStyle.Bold), Brushes.Red, new Point(0, 0));
        }

        private void timerBG_Tick(object sender, EventArgs e)
        {
            //每50毫秒让窗体发生重绘
            this.Invalidate();
            //不停的去判断敌人飞机的数量
            //获取当前敌人飞机的数量
            int count = SingleObject.GetSingle().listPlaneEnemy.Count;
            if (count <= 1)
            { 
                //再次对飞机进行初始化
                InitialPlaneEnemy();
            }

            //不停的碰撞检测
            SingleObject.GetSingle().PZJC();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //在窗体加载的时候 解决窗体闪烁问题
            //将图像绘制到缓冲区减少闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //当鼠标在窗体进行移动的时候 ，让飞机跟着鼠标的移动而移动
            SingleObject.GetSingle().PH.MouseMove(e);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //判断玩家是否在窗体摁下了左键
            //如果摁下了左键 调用调用玩家飞机开炮的方法
            SingleObject.GetSingle().PH.Fire();
        }
    }
}
