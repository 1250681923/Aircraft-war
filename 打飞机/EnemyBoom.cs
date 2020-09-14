using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 打飞机.Properties;
namespace 打飞机
{
    class EnemyBoom : Boom
    {
        //需要倒入每种飞机爆炸时候的图片

        private Image[] imgs1 = {
                                    Resources.enemy0_down11,
                                    Resources.enemy0_down2,
                                    Resources.enemy0_down3,
                                    Resources.enemy0_down4
                                };
        private Image[] imgs2 = { 
                                Resources.enemy1_down11,
                                Resources.enemy1_down2,
                                Resources.enemy1_down3,
                                Resources.enemy1_down4
                                };
        private Image[] imgs3 = { 
                                    Resources.enemy2_down11,
                                    Resources.enemy2_down2,
                                    Resources.enemy2_down3,
                                    Resources.enemy2_down4,
                                    Resources.enemy2_down5,
                                    Resources.enemy2_down6
                                };

        //在爆炸的时候 我们需要知道当前爆炸的是哪家飞机
        //根据敌人飞机的类型来播放对应的爆炸图片
        public int Type
        {
            get;
            set;
        }

        public EnemyBoom(int x, int y,int type)
            : base(x, y)
        {
            this.Type = type;
        }

        public override void Draw(Graphics g)
        {
            //在将爆炸图片绘制到窗体的时候 需要根据当前飞机的类型来绘制
            switch (this.Type)
            { 
                case 0:
                    for (int i = 0; i < imgs1.Length; i++)
                    {
                        g.DrawImage(imgs1[i], this.X, this.Y);
                    }
                    break;
                case 1:
                    for (int i = 0; i < imgs2.Length; i++)
                    {
                        g.DrawImage(imgs2[i], this.X, this.Y);
                    }
                    break;
                case 2:
                    for (int i = 0; i < imgs3.Length; i++)
                    {
                        g.DrawImage(imgs3[i], this.X, this.Y);
                    }
                    break;
            }

            //爆炸图片播放完成后 就应该销毁
            SingleObject.GetSingle().RemoveGameObject(this);
        }




    }
}
