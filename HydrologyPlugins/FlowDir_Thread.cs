using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotSpatial.Data;
using System.Threading;
using GeoAPI.Geometries;

namespace HydrologyPlugins
{
    class FlowDir_Thread
    {
        IRaster DirectionRaster = null;
        IRaster LabelRaster = null;
        IRaster ExecuteRaster = null;
        public FlowDir_Thread(IRaster Direction,IRaster Label,IRaster Execute)
        {
            DirectionRaster = Direction;
            LabelRaster = Label;
            ExecuteRaster = Execute;
        }

        //Execute the drop of two cells
        private double Drop(RasterPixel rp1, RasterPixel rp2) //rp1 is the middle pixel.
        {
            Coordinate coor1 = ExecuteRaster.CellToProj(rp1.Row, rp1.Column);
            Coordinate coor2 = ExecuteRaster.CellToProj(rp2.Row, rp2.Column);
            double distance = coor1.Distance(coor2);
            double change_z = rp1.Value - rp2.Value;
            double drop = change_z / distance;
            return drop;
        }

        //Get max value
        private double GetMaxValue(List<RasterPixel> RsList)
        {
            double MaxValue = RsList[0].Value;
            for (int i = 0; i < RsList.Count; i++)
            {
                if (RsList[i].Value >= MaxValue)
                {
                    MaxValue = RsList[i].Value;
                }
            }
            return MaxValue;
        }

        //Get the number of max value pixel
        private int Num_MaxValue(List<RasterPixel> RsList, double MaxValue)
        {
            int Num_Max = 0;
            for (int i = 0; i < RsList.Count; i++)
            {
                if (RsList[i].Value == MaxValue)
                {
                    Num_Max++;
                }
            }
            return Num_Max;
        }

        //Get the max value pixel's direction
        private int Pixel_MaxValue(List<RasterPixel> RsList, double MaxValue, RasterPixel rp)
        {
            int row = 0, column = 0;
            for (int i = 0; i < RsList.Count; i++)
            {
                if (RsList[i].Value == MaxValue)
                {
                    row = RsList[i].Row;
                    column = RsList[i].Column;
                }
            }
            if ((rp.Row - row == 0) && (rp.Column - column == 1)) //Left
            {
                return 16;
            }
            else if ((rp.Row - row == 0) && (rp.Column - column == -1)) //Right
            {
                return 1;
            }
            else if ((rp.Column - column == 0) && (rp.Row - row == 1)) //Top
            {
                return 64;
            }
            else if ((rp.Column - column == 0) && (rp.Row - row == -1)) //Bottom
            {
                return 4;
            }
            else if ((rp.Row - row == 1) && (rp.Column - column == 1)) //TopLeft
            {
                return 32;
            }
            else if ((rp.Row - row == 1) && (rp.Column - column == -1)) //TopRight
            {
                return 128;
            }
            else if ((rp.Row - row == -1) && (rp.Column - column == 1)) //BottomLeft
            {
                return 8;
            }
            else if ((rp.Row - row == -1) && (rp.Column - column == -1))  //BottomRight
            {
                return 2;
            }
            else
            {
                return -1;
            }
        }

        //Judge the direction
        private int Direct(RasterPixel rp)
        {
            //Init neibor
            List<RasterPixel> DropList = new List<RasterPixel>();
            if (0 <= rp.Column - 1)
            {
                //if(ExecuteRaster.Value[rp.Row,rp.Column-1]!=ExecuteRaster.NoDataValue)
                //{
                RasterPixel rp_Left = new RasterPixel(rp.Row, rp.Column - 1, ExecuteRaster.Value[rp.Row, rp.Column - 1]);
                double DropValue = Drop(rp, rp_Left);
                rp_Left.Value = DropValue;
                DropList.Add(rp_Left);
                //}
            }
            if ((0 <= rp.Column - 1) && (0 <= rp.Row - 1))
            {
                //if(ExecuteRaster.Value[rp.Row-1,rp.Column-1]!=ExecuteRaster.NoDataValue)
                //{
                RasterPixel rp_TopLeft = new RasterPixel(rp.Row - 1, rp.Column - 1, ExecuteRaster.Value[rp.Row - 1, rp.Column - 1]);
                double DropValue = Drop(rp, rp_TopLeft);
                rp_TopLeft.Value = DropValue;
                DropList.Add(rp_TopLeft);
                //}
            }
            if ((0 <= rp.Row - 1))
            {
                //if(ExecuteRaster.Value[rp.Row-1,rp.Column]!=ExecuteRaster.NoDataValue)
                //{
                RasterPixel rp_Top = new RasterPixel(rp.Row - 1, rp.Column, ExecuteRaster.Value[rp.Row - 1, rp.Column]);
                double DropValue = Drop(rp, rp_Top);
                rp_Top.Value = DropValue;
                DropList.Add(rp_Top);
                //}
            }
            if ((rp.Row - 1 >= 0) && (rp.Column + 1 < ExecuteRaster.NumColumns))
            {
                //if((ExecuteRaster.Value[rp.Row-1,rp.Column+1]!=ExecuteRaster.NoDataValue))
                //{
                RasterPixel rp_TopRight = new RasterPixel(rp.Row - 1, rp.Column + 1, ExecuteRaster.Value[rp.Row - 1, rp.Column + 1]);
                double DropValue = Drop(rp, rp_TopRight);
                rp_TopRight.Value = DropValue;
                DropList.Add(rp_TopRight);
                //}
            }
            if ((rp.Column + 1 < ExecuteRaster.NumColumns))
            {
                //if(ExecuteRaster.Value[rp.Row,rp.Column+1]!=ExecuteRaster.NoDataValue)
                //{
                RasterPixel rp_Right = new RasterPixel(rp.Row, rp.Column + 1, ExecuteRaster.Value[rp.Row, rp.Column + 1]);
                double DropValue = Drop(rp, rp_Right);
                rp_Right.Value = DropValue;
                DropList.Add(rp_Right);
                //}
            }
            if ((rp.Column + 1 < ExecuteRaster.NumColumns) && (rp.Row + 1 < ExecuteRaster.NumRows))
            {
                //if(ExecuteRaster.Value[rp.Row+1,rp.Column+1]!=ExecuteRaster.NoDataValue)
                //{
                RasterPixel rp_BottomRight = new RasterPixel(rp.Row + 1, rp.Column + 1, ExecuteRaster.Value[rp.Row + 1, rp.Column + 1]);
                double DropValue = Drop(rp, rp_BottomRight);
                rp_BottomRight.Value = DropValue;
                DropList.Add(rp_BottomRight);
                //}
            }
            if (rp.Row + 1 < ExecuteRaster.NumRows)
            {
                //if(ExecuteRaster.Value[rp.Row+1,rp.Column]!=ExecuteRaster.NoDataValue)
                //{
                RasterPixel rp_Bottom = new RasterPixel(rp.Row + 1, rp.Column, ExecuteRaster.Value[rp.Row + 1, rp.Column]);
                double DropValue = Drop(rp, rp_Bottom);
                rp_Bottom.Value = DropValue;
                DropList.Add(rp_Bottom);
                //}
            }
            if ((rp.Row + 1 < ExecuteRaster.NumRows) && (rp.Column - 1 >= 0))
            {
                //if(ExecuteRaster.Value[rp.Row+1,rp.Column-1]!=ExecuteRaster.NoDataValue)
                //{
                RasterPixel rp_BottomLeft = new RasterPixel(rp.Row + 1, rp.Column - 1, ExecuteRaster.Value[rp.Row + 1, rp.Column - 1]);
                double DropValue = Drop(rp, rp_BottomLeft);
                rp_BottomLeft.Value = DropValue;
                DropList.Add(rp_BottomLeft);
                //}
            }

            double MaxValue = GetMaxValue(DropList);
            if (MaxValue < 0) //This pixel is depression
            {
                return -1;
            }
            else if ((MaxValue >= 0) && (Num_MaxValue(DropList, MaxValue) == 1))
            {
                return Pixel_MaxValue(DropList, MaxValue, rp);
            }
            else if ((MaxValue == 0) && (Num_MaxValue(DropList, MaxValue) > 1)) //This pixel is depression
            {
                return Pixel_MaxValue(DropList, MaxValue, rp);
            }
            else if ((MaxValue > 0) && (Num_MaxValue(DropList, MaxValue) > 1))
            {
                return Pixel_MaxValue(DropList, MaxValue, rp);
            }
            else
            {
                return -1;
            }
        }

        private void Pos_Caculate(IRaster direction, IRaster label)
        {
            bool is_break = false;
            for (int i = 0; i < ExecuteRaster.NumRows; i++)
            {
                for (int j = 0; j < ExecuteRaster.NumColumns; j++)
                {
                    if (label.Value[i, j] == 0.0)
                    {
                        if (ExecuteRaster.Value[i, j] != ExecuteRaster.NoDataValue)
                        {
                            RasterPixel rp = new RasterPixel(i, j, ExecuteRaster.Value[i, j]);
                            int Direction = Direct(rp);
                            direction.Value[i, j] = Direction;
                            label.Value[i, j] = ExecuteRaster.NoDataValue;
                        }
                        else
                        {
                            direction.Value[i, j] = ExecuteRaster.NoDataValue;
                            label.Value[i, j] = ExecuteRaster.NoDataValue;
                        }
                    }
                    else
                    {
                        is_break = true;
                        break;
                    }
                }
                if (is_break)
                {
                    break;
                }
            }
        }

        private void Rev_Caculate(IRaster direction, IRaster label)
        {
            bool is_break = false;
            for (int i = ExecuteRaster.NumRows - 1; i >= 0; i--)
            {
                for (int j = ExecuteRaster.NumColumns - 1; j >= 0; j--)
                {
                    if (label.Value[i, j] == 0.0)
                    {
                        if (ExecuteRaster.Value[i, j] != ExecuteRaster.NoDataValue)
                        {
                            RasterPixel rp = new RasterPixel(i, j, ExecuteRaster.Value[i, j]);
                            int Direction = Direct(rp);
                            direction.Value[i, j] = Direction;
                            label.Value[i, j] = ExecuteRaster.NoDataValue;
                        }
                        else
                        {
                            direction.Value[i, j] = ExecuteRaster.NoDataValue;
                            label.Value[i, j] = ExecuteRaster.NoDataValue;
                        }
                    }
                    else
                    {
                        is_break = true;
                        break;
                    }
                }
                if (is_break)
                {
                    break;
                }
            }
        }

        private object objLock = new object();
        public void Pos_start()
        {
            Pos_Caculate(DirectionRaster, LabelRaster);
        }

        public void Rev_start()
        {
            Rev_Caculate(DirectionRaster, LabelRaster);
        }
    }
}
