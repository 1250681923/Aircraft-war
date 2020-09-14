using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 打飞机.Properties;
namespace 打飞机
{
    class HeroZiDan : ZiDan
    {
        private static Image imgHero = Resources.bullet1;
        public HeroZiDan(PlaneFather pf, int speed, int power)
            : base(pf, imgHero, speed, power)
        { }

    }
}
