using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydrologyPlugins
{
    class RasterPixel_2
    {
        public double Value1,Value2;
        public int Row, Column;
        public RasterPixel_2(int x,int y,double v1,double v2)
        {
            Row = x;
            Column = y;
            Value1 = v1;
            Value2 = v2;
        }
    }
}
