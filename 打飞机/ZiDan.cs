using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 打飞机
{
    //子弹的父类
    class ZiDan : GameObject
    {
        private Image imgZiDan;//存储子弹图片
        //记录一下子弹的威力
        public int Power
        {
            get;
            set;
        }
        public ZiDan(PlaneFather pf, Image img, int speed, int power)
            : base(pf.X + pf.Width / 2 - 30, pf.Y + pf.Height / 2 - 50, img.Width, img.Height, speed, 0, pf.Dir)
        {
            this.imgZiDan = img;
            this.Power = power;
        }


        //重写GameObject的抽象成员
        public override void Draw(Graphics g)
        {
            this.Move();
            g.DrawImage(imgZiDan, this.X, this.Y, this.Width / 2, this.Height / 2);
        }

        public override void Move()
        {
            switch (this.Dir)
            {
                case Direction.Up:
                    this.Y -= this.Speed;
                    break;
                case Direction.Down:
                    this.Y += this.Speed;
                    break;
            }
            //子弹发出后 控制一下子弹的坐标
            if (this.Y <= 0)
            {
                this.Y = -100;
                //在游戏中移除子弹对象
            }
            if (this.Y >= 780)
            {
                this.Y = 1000;
                //在游戏中移除子弹对象
            }
        }
    }
}
