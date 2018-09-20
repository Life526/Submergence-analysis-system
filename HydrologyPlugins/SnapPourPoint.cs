using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Extensions;
using DotSpatial.Projections;
using DotSpatial.Symbology;
using GeoAPI.CoordinateSystems;
using GeoAPI.Geometries;
using System.Windows.Forms;
using System.Data;
using NetTopologySuite.Geometries;

namespace HydrologyPlugins
{
    class SnapPourPoint
    {
        IMapPointLayer pointLayer = null;
        IRaster Accumulation = null;
        IRaster Surface = null;
        double Radius = 0.0;
        string savePath = null;
        IRaster Result = null;
        IRaster Label = null;
        IMap MainMap = null;

        public SnapPourPoint(IMapPointLayer InputPoint,IRaster InputAccumulation,IRaster InputSurface,double InputRadius,string InputSave,IRaster InputResult,IRaster InputLabel,IMap InputMap)
        {
            pointLayer = InputPoint;
            Accumulation = InputAccumulation;
            Surface = InputSurface;
            Radius = InputRadius;
            savePath = InputSave;
            Result = InputResult;
            Label = InputLabel;
            MainMap = InputMap;
        }

        //Convert point to pixel
        private RcIndex Point_to_Raster(IMapPointLayer point,IRaster ExecuteRaster)
        {
            IFeatureSet featureSet = point.DataSet;
            if (featureSet.Features.Count > 1 || featureSet.Features.Count <= 0)
            {
                //只能有一个点，不能没有点
                MessageBox.Show("The point is invalid!", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return new RcIndex(-9999, -9999);
            }
            Coordinate coor = featureSet.Features[0].Geometry.Coordinates[0];  //获取点要素的坐标
            RcIndex rc = ExecuteRaster.Bounds.ProjToCell(coor);  //获取点要素对应的栅格坐标
            return rc;
        }

        //Convert pixel to point
        private Coordinate Rc_to_Coor(int i,int j,IRaster ExecuteRaster)
        {
            Coordinate coor = ExecuteRaster.CellToProj(i,j);
            return coor;
        }

        //Caculate the distance of two pixel
        private double Pixel_Distance(int i,int j,int a,int b,IRaster ExecuteRaster)
        {
            Coordinate coor1 = Rc_to_Coor(i,j,ExecuteRaster);
            Coordinate coor2 = Rc_to_Coor(a, b, ExecuteRaster);
            double distance = coor1.Distance(coor2);
            return distance;
        }

        //The pixel is on the boundary
        private bool is_boundary_pixel(int i,int j,IRaster ExecuteRaster)
        {
            if (((0 <= i) && (i < ExecuteRaster.NumRows)) && ((0 <= j) && (j < ExecuteRaster.NumColumns)))
            {
                if(ExecuteRaster.Value[i,j]!=ExecuteRaster.NoDataValue)
                {
                    return true;
                }
            }
            return false;
        }

        //The distance is in the radius
        private bool in_Radius(int i,int j,int a,int b,double Radius,IRaster ExecuteRaster)
        {
            double dis = Pixel_Distance(i,j,a,b,ExecuteRaster);
            if(dis<=Radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void is_Satisfy(int midRow,int midCol,int NeiborRow,int NeiborCol,double dis,IRaster InAccRaster,IRaster InSurRaster,IRaster InLabel,Stack<RasterPixel_2> RpArr)
        {
            if (is_boundary_pixel(NeiborRow, NeiborCol, InAccRaster))
            {
                if (in_Radius(midRow, midCol, NeiborRow, NeiborCol, dis, InAccRaster))
                {
                    RasterPixel_2 rp = new RasterPixel_2(NeiborRow,NeiborCol,InAccRaster.Value[NeiborRow,NeiborCol],InSurRaster.Value[NeiborRow,NeiborCol]);
                    RpArr.Push(rp);
                    InLabel.Value[rp.Row, rp.Column] = rp.Value2;
                }
            }
        }

        private RasterPixel_2 Search_pour_pixel(Stack<RasterPixel_2> RpArr)
        {
            Stack<RasterPixel_2> tempArr=new Stack<RasterPixel_2>();
            if(RpArr.Count==0)
            {
                MessageBox.Show("Error in search pour pixel.","Admin",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
            double MaxAcc = RpArr.ElementAt(0).Value1;
            double MaxDEM = RpArr.ElementAt(0).Value2;
            for(int i=0;i<tempArr.Count;i++) //Get max accumulation
            {
                if(RpArr.ElementAt(i).Value1>MaxAcc)
                {
                    MaxAcc = RpArr.ElementAt(i).Value1;
                }
            }
            for (int i = 0; i < tempArr.Count; i++)  //Get max dem
            {
                if (RpArr.ElementAt(i).Value2 > MaxDEM)
                {
                    MaxDEM = RpArr.ElementAt(i).Value2;
                }
            }
            for(int i=0;i<tempArr.Count;i++)
            {
                if (RpArr.ElementAt(i).Value1 == MaxAcc)
                {
                    tempArr.Push(RpArr.ElementAt(i));
                }
            }
            for(int i=0;i<tempArr.Count;i++)
            {
                if(RpArr.ElementAt(i).Value2==MaxDEM)
                {
                    return RpArr.ElementAt(i);
                }
            }
            return null;
        }

        public void Execute()
        {
            RcIndex rc = Point_to_Raster(pointLayer, Accumulation);
            RasterPixel_2 rp1 = new RasterPixel_2(rc.Row,rc.Column,Accumulation.Value[rc.Row,rc.Column],Surface.Value[rc.Row,rc.Column]);
            Stack<RasterPixel_2> RpArr1 = new Stack<RasterPixel_2>();
            Stack<RasterPixel_2> RpArr2 = new Stack<RasterPixel_2>();
            RpArr1.Push(rp1);
            Label.Value[rp1.Row, rp1.Column] = rp1.Value2;
            do
            {
                RasterPixel_2 rp2 = RpArr1.Pop();
                RpArr2.Push(rp2);
                is_Satisfy(rp2.Row,rp2.Column,rp2.Row,rp2.Column-1,Radius,Accumulation,Surface,Label,RpArr1); //Left
                is_Satisfy(rp2.Row, rp2.Column, rp2.Row, rp2.Column + 1, Radius, Accumulation, Surface, Label, RpArr1); //Right
                is_Satisfy(rp2.Row, rp2.Column, rp2.Row-1, rp2.Column, Radius, Accumulation, Surface, Label, RpArr1); //Top
                is_Satisfy(rp2.Row, rp2.Column, rp2.Row+1, rp2.Column, Radius, Accumulation, Surface, Label, RpArr1); //Bottom
                is_Satisfy(rp2.Row, rp2.Column, rp2.Row-1, rp2.Column - 1, Radius, Accumulation, Surface, Label, RpArr1); //TopLeft
                is_Satisfy(rp2.Row, rp2.Column, rp2.Row - 1, rp2.Column + 1, Radius, Accumulation, Surface, Label, RpArr1); //TopRight
                is_Satisfy(rp2.Row, rp2.Column, rp2.Row + 1, rp2.Column - 1, Radius, Accumulation, Surface, Label, RpArr1); //BottomLeft
                is_Satisfy(rp2.Row, rp2.Column, rp2.Row + 1, rp2.Column + 1, Radius, Accumulation, Surface, Label, RpArr1); //BottomRight
            } while (RpArr1.Count > 0);
            RasterPixel_2 rp3 = Search_pour_pixel(RpArr2);
            Coordinate ResultCoor = Rc_to_Coor(rp3.Row,rp3.Column,Accumulation);

            FeatureSet pointF = new FeatureSet(FeatureType.Point);
            pointF.Projection = Accumulation.Projection;
            DataColumn Column1 = new DataColumn("ID");
            pointF.DataTable.Columns.Add(Column1);
            DataColumn Column2 = new DataColumn("Accumulation");
            pointF.DataTable.Columns.Add(Column2);
            DataColumn Column3 = new DataColumn("Elevation");
            pointF.DataTable.Columns.Add(Column3);

            Point point = new Point(ResultCoor);
            IFeature currentFeature = pointF.AddFeature(point);
            currentFeature.DataRow["ID"] = 0;
            currentFeature.DataRow["Accumulation"] = rp3.Value1;
            currentFeature.DataRow["Elevation"] = rp3.Value2;
            pointF.SaveAs(savePath,false);
            MainMap.Layers.Add(pointF);
        }
    }
}
