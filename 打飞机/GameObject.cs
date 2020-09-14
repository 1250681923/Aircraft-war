using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 打飞机
{


    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    /// <summary>
    /// 这是所有游戏对象的父类，封装着所有子类所共有的成员
    /// </summary>
    abstract class GameObject
    {
        #region 横纵坐标、宽度、高度、速度、生命值、方向
        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public int Speed
        {
            get;
            set;
        }

        public int Life
        {
            get;
            set;
        }

        public Direction Dir
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="speed"></param>
        /// <param name="life"></param>
        /// <param name="dir"></param>
        public GameObject(int x, int y, int width, int height, int speed, int life, Direction dir)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Speed = speed;
            this.Life = life;
            this.Dir = dir;
        }


        //每个游戏对象在使用GDI+对象绘制自己到窗体的时候，绘制的方式都各不一样、。
        //所以我们需要在父类中提供一个绘制对象的抽象函数
        public abstract void Draw(Graphics g);

        //在提供一个用于碰撞检测的函数 返回当前游戏对象的矩形

        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Height);
        }

        public GameObject(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }


        /// <summary>
        /// 移动的虚方法，每个子类如果有不一样的地方，则重写
        /// </summary>
        public virtual void Move()
        {
            //根据游戏对象的方向进行移动
            switch (this.Dir)
            { 
                case Direction.Up:
                    this.Y -= this.Speed;
                    break;

                case Direction.Down:
                    this.Y += this.Speed;
                    break;
                case Direction.Left:
                    this.X -= this.Speed;
                    break;
                case Direction.Right:
                    this.X += this.Speed;
                    break;
            }

            //移动完成后 判断一下游戏对象是否超出了窗体
            if (this.X <= 0)
            {
                this.X = 0;
            }
            if (this.X >= 400)
            {
                this.X = 400;
            }
            if (this.Y <= 0)
            {
                this.Y = 0;
            }
            if (this.Y >= 700)
            {
                this.Y = 700;
            }
        }


    }
}
