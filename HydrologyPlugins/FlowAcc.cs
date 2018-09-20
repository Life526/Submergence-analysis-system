using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Controls;
using DotSpatial.Extensions;
using DotSpatial.Projections;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace HydrologyPlugins
{
    class FlowAcc
    {
        IRaster ExecuteRaster = null;
        string SavePath = null;
        IMap MainMap = null;

        public FlowAcc(IRaster InputRaster, string OutputPath, IMap InputMap)
        {
            ExecuteRaster = InputRaster;
            SavePath = OutputPath;
            MainMap = InputMap;
        }

        public void Execute()
        {
            string[] options = new string[1];
            IRaster AccumulationRaster = Raster.CreateRaster("Accumulation" + ".bgd", null, ExecuteRaster.NumColumns, ExecuteRaster.NumRows, 1, typeof(long), options);
            AccumulationRaster.Bounds = ExecuteRaster.Bounds;
            AccumulationRaster.NoDataValue = ExecuteRaster.NoDataValue;
            AccumulationRaster.Projection = ExecuteRaster.Projection;

            IRaster LabelRaster = Raster.CreateRaster("Label" + ".bgd", null, ExecuteRaster.NumColumns, ExecuteRaster.NumRows, 1, ExecuteRaster.DataType, options);
            LabelRaster.Bounds = ExecuteRaster.Bounds;
            LabelRaster.NoDataValue = ExecuteRaster.NoDataValue;
            LabelRaster.Projection = ExecuteRaster.Projection;

            try
            {
                FlowAcc_Thread fa_thread = new FlowAcc_Thread(AccumulationRaster, LabelRaster, ExecuteRaster);
                Thread thread1 = new Thread(new ThreadStart(fa_thread.Pos_Accumulate));
                thread1.Start();
                Thread thread2 = new Thread(new ThreadStart(fa_thread.Rev_Accumulate));
                thread2.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MainMap.Layers.Add(AccumulationRaster);
            int LayerNum = MainMap.Layers.Count;
            string FileName = Path.GetFileName(SavePath);
            MainMap.Layers[LayerNum - 1].LegendText = FileName;
            AccumulationRaster.SaveAs(SavePath);
            MessageBox.Show("Completed program.", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
