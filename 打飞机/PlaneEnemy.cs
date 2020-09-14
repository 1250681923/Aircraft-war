using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 打飞机.Properties;
namespace 打飞机
{
    class PlaneEnemy : PlaneFather
    {
        private static Image img1 = Resources.enemy0;//最小的飞机
        private static Image img2 = Resources.enemy1;//中间的飞机
        private static Image img3 = Resources.enemy2;//最大的飞机
        public PlaneEnemy(int x, int y, int type)
            : base(x, y, GetImage(type), GetSpeed(type), GetLife(type), Direction.Down)
        {
            this.EnemyType = type;
        }

        //因为每一架飞机的大小、生命值、速度都不一样，所以我们需要声明一个标示来标记当前到底属于哪架飞机
        //0--最小的飞机  1--中间的飞机 2--最大的飞机
        public int EnemyType
        {
            get;
            set;
        }

        //下面需要根据我们飞机的类型 分别的写三个函数 用于返回飞机的图片 飞机的速度 飞机的生命值


        //根据飞机的类型 返回对应的图片
        public static Image GetImage(int type)
        {
            //静态函数中只能访问静态成员
            switch (type)
            {
                case 0:
                    return img1;
                case 1:
                    return img2;
                case 2:
                    return img3;
            }
            return null;
        }

        //根据飞机的类型  返回对应的生命值
        public static int GetLife(int type)
        {
            switch (type)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 3;
            }
            return 0;
        }
        //根据飞机的类型 返回对应的速度
        public static int GetSpeed(int type)
        {
            switch (type)
            {
                case 0:
                    return 5;
                case 1:
                    return 6;
                case 2:
                    return 7;
            }
            return 0;
        }


        //我们需要重写父类中的Draw函数 将自己绘制到Form窗体上
        public override void Draw(Graphics g)
        {

            //随着将敌人飞机绘制出来 就让我们的敌人飞机 开始移动
            this.Move();
            //也需要根据不同的飞机类型 来绘制不同的飞机
            switch (this.EnemyType)
            {
                case 0:
                    g.DrawImage(img1, this.X, this.Y);
                    break;
                case 1:
                    g.DrawImage(img2, this.X, this.Y);
                    break;
                case 2:
                    g.DrawImage(img3, this.X, this.Y);
                    break;
            }
        }



        //重写父类中Move函数
        public override void Move()
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
            if (this.Y >= 780)
            {
                this.Y = 1400;//到达窗体底端的时候 让敌人飞机离开窗体
                //同时 当敌人飞机离开窗体的时候 我们应该销毁当前敌人飞机
                SingleObject.GetSingle().RemoveGameObject(this);
            }

            //当敌人的飞机类型是0，并且纵坐标>=某个值之后 我们不停的更换他的横坐标
            if (this.EnemyType == 0 && this.Y >= 200)
            {
                if (this.X >= 0 && this.X <= 220)
                {
                    //表示当前的小飞机在左边的范围内
                    //增加当前飞机的X值
                    this.X += r.Next(0, 50);
                }
                else
                {
                    this.X -= r.Next(0, 50);
                }
            }
            else//飞机类型是1或者2
            { 
                //如果是大飞机的话 就不让你改变横坐标了 而是让你加快速度
                this.Speed += 1;
            }

            //百分之三十的概率发射子弹
            if (r.Next(0, 100) > 90)
            {
                Fire();
            }

        }


        public void Fire()
        {
            SingleObject.GetSingle().AddGameObject(new EnemyZiDan(this, 20, 1));
        }


        static Random r = new Random();

        //判断敌人是否死亡
        public override void IsOver()
        {
            if (this.Life <= 0)
            { 
                //敌人飞机坠毁 应该将敌人飞机从游戏中移除
                SingleObject.GetSingle().RemoveGameObject(this);
                //播放敌人爆炸的图片  
                SingleObject.GetSingle().AddGameObject(new EnemyBoom(this.X, this.Y,this.EnemyType));
                //敌人发生了爆炸 给玩家加分
                //需要根据不同的敌人类型 添加不同的分数
                switch (this.EnemyType)
                { 
                    case 0:
                        //获得单例中记录玩家分数的属性
                        SingleObject.GetSingle().Score += 100;
                        break;
                    case 1:
                        SingleObject.GetSingle().Score += 200;
                        break;
                    case 2:
                        SingleObject.GetSingle().Score += 300;
                        break;
                }
            }
        }

    }
}
