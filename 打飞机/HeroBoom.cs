using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 打飞机.Properties;
namespace 打飞机
{
    class HeroBoom:Boom
    {
        private Image[] imgs = { 
                                Resources.hero_blowup_n1,
                                Resources.hero_blowup_n2,
                                Resources.hero_blowup_n3,
                                Resources.hero_blowup_n4
                               };
        public HeroBoom(int x, int y)
            : base(x, y)
        { }

        public override void Draw(Graphics g)
        {
            for (int i = 0; i < imgs.Length; i++)
            {
                g.DrawImage(imgs[i], this.X, this.Y);
            }
            //绘制完成后 应该将爆炸的图片移除
            SingleObject.GetSingle().RemoveGameObject(this);
        }

    }
}
