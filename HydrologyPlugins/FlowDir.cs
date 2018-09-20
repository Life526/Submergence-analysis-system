using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;
using GeoAPI.Geometries;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace HydrologyPlugins
{
    class FlowDir
    {
        IRaster ExecuteRaster = null;
        string SavePath = null;
        IMap MainMap = null;
        public FlowDir(IRaster InputRaster,string OutputPath,IMap InputMap)
        {
            ExecuteRaster = InputRaster;
            SavePath = OutputPath;
            MainMap = InputMap;
        }

        public void Execute()
        {
            string[] options = new string[1];
            IRaster DirectionRaster = Raster.CreateRaster("Direction" + ".bgd", null, ExecuteRaster.NumColumns, ExecuteRaster.NumRows, 1, ExecuteRaster.DataType, options);
            DirectionRaster.Bounds = ExecuteRaster.Bounds;
            DirectionRaster.NoDataValue = ExecuteRaster.NoDataValue;
            DirectionRaster.Projection = ExecuteRaster.Projection;

            IRaster LabelRaster = Raster.CreateRaster("Label" + ".bgd", null, ExecuteRaster.NumColumns, ExecuteRaster.NumRows, 1, ExecuteRaster.DataType, options);
            LabelRaster.Bounds = ExecuteRaster.Bounds;
            LabelRaster.NoDataValue = ExecuteRaster.NoDataValue;
            LabelRaster.Projection = ExecuteRaster.Projection;

            try
            {
                FlowDir_Thread fd_thread = new FlowDir_Thread(DirectionRaster, LabelRaster, ExecuteRaster);
                Thread thread1 = new Thread(new ThreadStart(fd_thread.Pos_start));
                thread1.Start();
                Thread thread2 = new Thread(new ThreadStart(fd_thread.Rev_start));
                thread2.Start();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            MainMap.Layers.Add(DirectionRaster);
            int LayerNum = MainMap.Layers.Count;
            string FileName = Path.GetFileName(SavePath);
            MainMap.Layers[LayerNum - 1].LegendText = FileName;
            MainMap.Refresh();
            DirectionRaster.SaveAs(SavePath);
            MessageBox.Show("Completed program.","Admin",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
