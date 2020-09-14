using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 打飞机
{
    class SingleObject
    {
        //单例设计模式
        //1、构造函数私有化
        private SingleObject()
        { }
        //2、声明全局唯一的对象
        private static SingleObject _single = null;
        //3、提供一个静态函数用于返回一个唯一的对象
        public static SingleObject GetSingle()
        {
            if (_single == null)
            {
                _single = new SingleObject();
            }
            return _single;
        }

        //存储的背景在游戏中唯一的对象
        public BackGround BG
        {
            get;
            set;
        }

        //存储的是玩家飞机在游戏中唯一的对象
        public PlaneHero PH
        {
            get;
            set;
        }
        //声明一个集合对象用来存储玩家子弹
        List<HeroZiDan> listHeroZiDan = new List<HeroZiDan>();

        //声明一个集合对象来存储敌人飞机对象
        public List<PlaneEnemy> listPlaneEnemy = new List<PlaneEnemy>();

        //声明一个集合来存放我们敌人的子弹
        List<EnemyZiDan> listEnemyZiDan = new List<EnemyZiDan>();

        //声明一个集合来存储敌人爆炸的对象
        List<EnemyBoom> listEnemyBoom = new List<EnemyBoom>();

        //声明一个集合来存储玩家爆炸的对象
        List<HeroBoom> listHeroBoom = new List<HeroBoom>();

        //下面呢，我要写一个函数，将我们床架的游戏对象，添加到我们的窗体中
        public void AddGameObject(GameObject go)
        {
            if (go is BackGround)
            {
                this.BG = go as BackGround;
            }
            else if (go is PlaneHero)
            {
                this.PH = go as PlaneHero;
            }
            else if (go is HeroZiDan)
            {
                listHeroZiDan.Add(go as HeroZiDan);
            }
            else if (go is PlaneEnemy)
            {
                listPlaneEnemy.Add(go as PlaneEnemy);
            }
            else if (go is EnemyBoom)
            {
                listEnemyBoom.Add(go as EnemyBoom);
            }
            else if(go is EnemyZiDan)
            {
                listEnemyZiDan.Add(go as EnemyZiDan);
            }
            else if (go is HeroBoom)
            {
                listHeroBoom.Add(go as HeroBoom);
            }
        }



        //将游戏对象从游戏中移除
        public void RemoveGameObject(GameObject go)
        {
            //移除飞机
            if (go is PlaneEnemy)
            {
                listPlaneEnemy.Remove(go as PlaneEnemy);
            }
            //玩家子弹打出边界后 将玩家子弹同样的移除
            else if (go is HeroZiDan)
            {
                listHeroZiDan.Remove(go as HeroZiDan);
            }
            else if (go is EnemyBoom)
            {
                listEnemyBoom.Remove(go as EnemyBoom);
            }
            else if (go is EnemyZiDan)
            {
                listEnemyZiDan.Remove(go as EnemyZiDan);
            }
            else if (go is HeroBoom)
            {
                listHeroBoom.Remove(go as HeroBoom);
            }
        }


        public void Draw(Graphics g)
        {
            this.BG.Draw(g);//向窗体中绘制的是背景
            this.PH.Draw(g);//绘制的是玩家飞机
            for (int i = 0; i < listHeroZiDan.Count; i++)
            {
                listHeroZiDan[i].Draw(g);
            }
            for (int i = 0; i < listPlaneEnemy.Count; i++)
            {
                listPlaneEnemy[i].Draw(g);
            }

            for (int i = 0; i < listEnemyBoom.Count; i++)
            {
                listEnemyBoom[i].Draw(g);
            }

            for (int i = 0; i < listEnemyZiDan.Count; i++)
            {
                listEnemyZiDan[i].Draw(g);
            }
            for (int i = 0; i < listHeroBoom.Count; i++)
            {
                listHeroBoom[i].Draw(g);
            }
        }

        //记录玩家的分数
        public int Score
        {
            get;
            set;
        }

        public void PZJC()
        {
            #region 判断玩家的子弹是否打到了敌人的身上
            for (int i = 0; i < listHeroZiDan.Count; i++)
            {
                for (int j = 0; j < listPlaneEnemy.Count; j++)
                {
                    if (listHeroZiDan[i].GetRectangle().IntersectsWith(listPlaneEnemy[j].GetRectangle()))
                    { 
                        //如果条件成立 则说明发生了碰撞
                        //也就是玩家的子弹打到了敌人的身上

                        //敌人的生命值应该减少
                        listPlaneEnemy[j].Life -= listHeroZiDan[i].Power;
                        //生命值减少后 应该判断敌人是否死亡
                        listPlaneEnemy[j].IsOver();
                        //玩家子弹打到了敌人身上后 应该将玩家子弹销毁
                        listHeroZiDan.Remove(listHeroZiDan[i]);
                        break;
                    }
                }
            }
            #endregion


            #region 判断敌人的子弹是否打到了玩家身上
            for (int i = 0; i < listEnemyZiDan.Count; i++)
            {
                if (listEnemyZiDan[i].GetRectangle().IntersectsWith(this.PH.GetRectangle()))
                { 
                    //让玩家发生爆炸 但不死亡
                    this.PH.IsOver();
                    break;
                }
            }
            #endregion

            #region 判断玩家是否和敌人飞机发生相撞

            for (int i = 0; i < listPlaneEnemy.Count; i++)
            {
                if (listPlaneEnemy[i].GetRectangle().IntersectsWith(this.PH.GetRectangle()))
                {
                    listPlaneEnemy[i].Life = 0;
                    listPlaneEnemy[i].IsOver();
                    break;
                }
            }

            #endregion

        }

    }
}
