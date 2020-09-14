using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 打飞机
{
    /// <summary>
    /// 飞机的父类 抽象类
    /// </summary>
    abstract class PlaneFather : GameObject
    {
        private Image imgPlane;//声明一个字段存储飞机的图片
        public PlaneFather(int x, int y, Image img, int speed, int life, Direction dir)
            : base(x, y, img.Width, img.Height, speed, life, dir)
        {
            this.imgPlane = img;
        }

        //我们飞机的父类不需要 重写父类的Draw函数，因为我们玩家飞机跟敌人飞机在绘制自己到窗体的时候 方式各不一样


        //提供一个判断是否死亡的抽象函数 具体怎么死亡 又子类自己去决定
        public abstract void IsOver();
    }
}
