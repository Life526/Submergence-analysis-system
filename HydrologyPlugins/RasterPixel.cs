using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydrologyPlugins
{
    class RasterPixel
    {
        public double Value;
        public int Row, Column;
        public RasterPixel(int x,int y,double v)
        {
            Row = x;
            Column = y;
            Value = v;
        }
    }
}
