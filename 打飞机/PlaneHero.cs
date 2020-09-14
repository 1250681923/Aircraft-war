using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 打飞机.Properties;
namespace 打飞机
{
    class PlaneHero : PlaneFather
    {
        //导入玩家飞机的图片 存储到字段中
        private static Image imgPlane = Resources.hero1;
        //再去调用父类的构造函数
        public PlaneHero(int x, int y, int speed, int life, Direction dir)
            : base(x, y, imgPlane, speed, life, dir)
        { }


        //必须要重写GameObject中抽象函数Draw，将自己绘制到屏幕上
        public override void Draw(Graphics g)
        {
            g.DrawImage(imgPlane, this.X, this.Y, this.Width / 2, this.Height / 2);
        }


        //让玩家飞机跟着鼠标走
        public void MouseMove(MouseEventArgs e)
        {
            this.X = e.X;
            this.Y = e.Y;
        }


        //提供一个开炮的函数
        public void Fire()
        { 
            //初始化我们的玩家子弹到游戏中
            SingleObject.GetSingle().AddGameObject(new HeroZiDan(this, 10, 1));
        }

        public override void IsOver()
        {
            SingleObject.GetSingle().AddGameObject(new HeroBoom(this.X, this.Y));
        }

    }
}
