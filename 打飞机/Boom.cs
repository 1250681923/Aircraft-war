using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 打飞机
{
    abstract class Boom : GameObject
    {
        //只需要调用父类的构造函数
        //在播放爆炸图片的时候 只需要知道爆炸图片应该播放的坐标就O啦
        public Boom(int x, int y)
            : base(x, y)
        {

        }
    }
}
