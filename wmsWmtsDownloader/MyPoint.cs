using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmsWmtsDownloader
{
    class MyPoint
    {
        public double x { get; set; }
        public double y { get; set; }

        public MyPoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public MyPoint()
        {
        }
    }
}
