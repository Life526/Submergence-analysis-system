using DotSpatial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydrologyPlugins
{
    class FlowAcc_Thread
    {
        IRaster AccumulationRaster = null;
        IRaster LabelRaster = null;
        IRaster ExecuteRaster = null;

        public FlowAcc_Thread(IRaster Accumulation, IRaster Label, IRaster Execute)
        {
            AccumulationRaster = Accumulation;
            LabelRaster = Label;
            ExecuteRaster = Execute;
        }

        //Get the Neibor pixel list
        private List<RasterPixel> NeiborList(RasterPixel rp)
        {
            List<RasterPixel> Neibor_L = new List<RasterPixel>();
            if (0 <= rp.Column - 1)
            {
                if(ExecuteRaster.Value[rp.Row,rp.Column-1]!=ExecuteRaster.NoDataValue)
                {
                RasterPixel rp_Left = new RasterPixel(rp.Row, rp.Column - 1, ExecuteRaster.Value[rp.Row, rp.Column - 1]);
                Neibor_L.Add(rp_Left);
                }
            }
            if ((0 <= rp.Column - 1) && (0 <= rp.Row - 1))
            {
                if(ExecuteRaster.Value[rp.Row-1,rp.Column-1]!=ExecuteRaster.NoDataValue)
                {
                RasterPixel rp_TopLeft = new RasterPixel(rp.Row - 1, rp.Column - 1, ExecuteRaster.Value[rp.Row - 1, rp.Column - 1]);
                Neibor_L.Add(rp_TopLeft);
                }
            }
            if ((0 <= rp.Row - 1))
            {
                if(ExecuteRaster.Value[rp.Row-1,rp.Column]!=ExecuteRaster.NoDataValue)
                {
                RasterPixel rp_Top = new RasterPixel(rp.Row - 1, rp.Column, ExecuteRaster.Value[rp.Row - 1, rp.Column]);
                Neibor_L.Add(rp_Top);
                }
            }
            if ((rp.Row - 1 >= 0) && (rp.Column + 1 < ExecuteRaster.NumColumns))
            {
                if((ExecuteRaster.Value[rp.Row-1,rp.Column+1]!=ExecuteRaster.NoDataValue))
                {
                RasterPixel rp_TopRight = new RasterPixel(rp.Row - 1, rp.Column + 1, ExecuteRaster.Value[rp.Row - 1, rp.Column + 1]);
                Neibor_L.Add(rp_TopRight);
                }
            }
            if ((rp.Column + 1 < ExecuteRaster.NumColumns))
            {
                if(ExecuteRaster.Value[rp.Row,rp.Column+1]!=ExecuteRaster.NoDataValue)
                {
                RasterPixel rp_Right = new RasterPixel(rp.Row, rp.Column + 1, ExecuteRaster.Value[rp.Row, rp.Column + 1]);
                Neibor_L.Add(rp_Right);
                }
            }
            if ((rp.Column + 1 < ExecuteRaster.NumColumns) && (rp.Row + 1 < ExecuteRaster.NumRows))
            {
                if(ExecuteRaster.Value[rp.Row+1,rp.Column+1]!=ExecuteRaster.NoDataValue)
                {
                RasterPixel rp_BottomRight = new RasterPixel(rp.Row + 1, rp.Column + 1, ExecuteRaster.Value[rp.Row + 1, rp.Column + 1]);
                Neibor_L.Add(rp_BottomRight);
                }
            }
            if (rp.Row + 1 < ExecuteRaster.NumRows)
            {
                if(ExecuteRaster.Value[rp.Row+1,rp.Column]!=ExecuteRaster.NoDataValue)
                {
                RasterPixel rp_Bottom = new RasterPixel(rp.Row + 1, rp.Column, ExecuteRaster.Value[rp.Row + 1, rp.Column]);
                Neibor_L.Add(rp_Bottom);
                }
            }
            if ((rp.Row + 1 < ExecuteRaster.NumRows) && (rp.Column - 1 >= 0))
            {
                if(ExecuteRaster.Value[rp.Row+1,rp.Column-1]!=ExecuteRaster.NoDataValue)
                {
                RasterPixel rp_BottomLeft = new RasterPixel(rp.Row + 1, rp.Column - 1, ExecuteRaster.Value[rp.Row + 1, rp.Column - 1]);
                Neibor_L.Add(rp_BottomLeft);
                }
            }
            return Neibor_L;
        }

        
        //Get direction pixel
        private RasterPixel Get_Direction_Pixel(RasterPixel mid_rp,RasterPixel neibor_pixel)
        {
            if ((mid_rp.Row - neibor_pixel.Row == 0) && (mid_rp.Column - neibor_pixel.Column == 1)) //Left
            {
                if(ExecuteRaster.Value[neibor_pixel.Row,neibor_pixel.Column]==1)
                {
                    RasterPixel TempRaster = new RasterPixel(neibor_pixel.Row,neibor_pixel.Column,neibor_pixel.Value);
                    return TempRaster;
                }
            }
            else if ((mid_rp.Row - neibor_pixel.Row == 0) && (mid_rp.Column - neibor_pixel.Column == -1)) //Right
            {
                if (ExecuteRaster.Value[neibor_pixel.Row, neibor_pixel.Column] == 16)
                {
                    RasterPixel TempRaster = new RasterPixel(neibor_pixel.Row, neibor_pixel.Column, neibor_pixel.Value);
                    return TempRaster;
                }
            }
            else if ((mid_rp.Column - neibor_pixel.Column == 0) && (mid_rp.Row - neibor_pixel.Row == 1)) //Top
            {
                if (ExecuteRaster.Value[neibor_pixel.Row, neibor_pixel.Column] == 4)
                {
                    RasterPixel TempRaster = new RasterPixel(neibor_pixel.Row, neibor_pixel.Column, neibor_pixel.Value);
                    return TempRaster;
                }
            }
            else if ((mid_rp.Column - neibor_pixel.Column == 0) && (mid_rp.Row - neibor_pixel.Row == -1)) //Bottom
            {
                if (ExecuteRaster.Value[neibor_pixel.Row, neibor_pixel.Column] == 64)
                {
                    RasterPixel TempRaster = new RasterPixel(neibor_pixel.Row, neibor_pixel.Column, neibor_pixel.Value);
                    return TempRaster;
                }
            }
            else if ((mid_rp.Row - neibor_pixel.Row == 1) && (mid_rp.Column - neibor_pixel.Column == 1)) //TopLeft
            {
                if (ExecuteRaster.Value[neibor_pixel.Row, neibor_pixel.Column] == 2)
                {
                    RasterPixel TempRaster = new RasterPixel(neibor_pixel.Row, neibor_pixel.Column, neibor_pixel.Value);
                    return TempRaster;
                }
            }
            else if ((mid_rp.Row - neibor_pixel.Row == 1) && (mid_rp.Column - neibor_pixel.Column == -1)) //TopRight
            {
                if (ExecuteRaster.Value[neibor_pixel.Row, neibor_pixel.Column] == 8)
                {
                    RasterPixel TempRaster = new RasterPixel(neibor_pixel.Row, neibor_pixel.Column, neibor_pixel.Value);
                    return TempRaster;
                }
            }
            else if ((mid_rp.Row - neibor_pixel.Row == -1) && (mid_rp.Column - neibor_pixel.Column == 1)) //BottomLeft
            {
                if (ExecuteRaster.Value[neibor_pixel.Row, neibor_pixel.Column] == 128)
                {
                    RasterPixel TempRaster = new RasterPixel(neibor_pixel.Row, neibor_pixel.Column, neibor_pixel.Value);
                    return TempRaster;
                }
            }
            else if ((mid_rp.Row - neibor_pixel.Row == -1) && (mid_rp.Column - neibor_pixel.Column == -1))  //BottomRight
            {
                if (ExecuteRaster.Value[neibor_pixel.Row, neibor_pixel.Column] == 32)
                {
                    RasterPixel TempRaster = new RasterPixel(neibor_pixel.Row, neibor_pixel.Column, neibor_pixel.Value);
                    return TempRaster;
                }
            }
            return null;
        }

        public void Pos_Accumulate()
        {
            for (int i = 0; i < ExecuteRaster.NumRows; i++)
            {
                for (int j = 0; j < ExecuteRaster.NumColumns; j++)
                {
                    if (LabelRaster.Value[i, j] == 0.0)
                    {
                        if (ExecuteRaster.Value[i, j] != ExecuteRaster.NoDataValue)
                        {
                            Stack<RasterPixel> Stack1 = new Stack<RasterPixel>();
                            long Accumulation_Count = 0;
                            RasterPixel NewRp = new RasterPixel(i, j, ExecuteRaster.Value[i, j]);
                            Stack1.Push(NewRp);
                            LabelRaster.Value[NewRp.Row, NewRp.Column] = LabelRaster.NoDataValue;
                            do
                            {
                                RasterPixel tempRP = Stack1.Pop();
                                List<RasterPixel> RpArr = NeiborList(tempRP);
                                for (int a = 0; a < RpArr.Count; a++)
                                {
                                    RasterPixel tempRP2 = Get_Direction_Pixel(tempRP, RpArr[a]);
                                    if (tempRP2 != null)
                                    {
                                        if (LabelRaster.Value[tempRP2.Row, tempRP2.Column] == 0.0)
                                        {
                                            Stack1.Push(tempRP2);
                                            Accumulation_Count++;
                                            LabelRaster.Value[tempRP2.Row, tempRP2.Column] = LabelRaster.NoDataValue;
                                        }
                                    }
                                }
                            } while (Stack1.Count != 0);
                            AccumulationRaster.Value[i, j] = Accumulation_Count;
                        }
                        else
                        {
                            AccumulationRaster.Value[i, j] = AccumulationRaster.NoDataValue;
                        }
                    }
                }
            }
        }

        public void Rev_Accumulate()
        {
            for (int i = ExecuteRaster.NumRows-1; i >=0; i--)
            {
                for (int j = ExecuteRaster.NumColumns - 1; j >=0; j--)
                {
                    if (LabelRaster.Value[i, j] == 0.0)
                    {
                        if (ExecuteRaster.Value[i, j] != ExecuteRaster.NoDataValue)
                        {
                            Stack<RasterPixel> Stack1 = new Stack<RasterPixel>();
                            long Accumulation_Count = 0;
                            RasterPixel NewRp = new RasterPixel(i, j, ExecuteRaster.Value[i, j]);
                            Stack1.Push(NewRp);
                            LabelRaster.Value[NewRp.Row, NewRp.Column] = LabelRaster.NoDataValue;
                            do
                            {
                                RasterPixel tempRP = Stack1.Pop();
                                List<RasterPixel> RpArr = NeiborList(tempRP);
                                for (int a = 0; a < RpArr.Count; a++)
                                {
                                    RasterPixel tempRP2 = Get_Direction_Pixel(tempRP, RpArr[a]);
                                    if (tempRP2 != null)
                                    {
                                        if (LabelRaster.Value[tempRP2.Row, tempRP2.Column] == 0.0)
                                        {
                                            Stack1.Push(tempRP2);
                                            Accumulation_Count++;
                                            LabelRaster.Value[tempRP2.Row, tempRP2.Column] = LabelRaster.NoDataValue;
                                        }
                                    }
                                }
                            } while (Stack1.Count != 0);
                            AccumulationRaster.Value[i, j] = Accumulation_Count;
                        }
                        else
                        {
                            AccumulationRaster.Value[i, j] = AccumulationRaster.NoDataValue;
                        }
                    }
                }
            }
        }

        public void Execute()
        {
            
            
        }
    }
}
