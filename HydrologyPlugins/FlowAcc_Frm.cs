using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;
using System.IO;

namespace HydrologyPlugins
{
    public partial class FlowAcc_Frm : Form
    {
        IMap MainMap = null;
        public FlowAcc_Frm(IMap InputMap)
        {
            InitializeComponent();
            MainMap = InputMap;
        }

        private void FlowDir_Frm_Load(object sender, EventArgs e)
        {
            //Add the rasterlayers in the map to the combox
            IMapRasterLayer[] RasterArr = MainMap.GetRasterLayers();
            if (RasterArr.Length > 0)
            {
                for (int i = 0; i < RasterArr.Length; i++)
                {
                    string RasterName = RasterArr[i].LegendText;
                    this.comboBox1.Items.Add(RasterName);
                }
            }
        }

        //File select
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a raster data";
            ofd.Filter = "Raster(*.tif;*.img)|*.tif;*.img";
            ofd.ValidateNames = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.comboBox1.Text = ofd.FileName;
            }
        }

        //Save file
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save the raster date";
            sfd.Filter = "Raster(*.tif;*.img)|*.tif;*.img";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = sfd.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //Output text changed
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string OutputString = this.textBox1.Text.Trim();
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Visible = false;
            if (File.Exists(OutputString))
            {
                this.pictureBox1.Enabled = true;
                this.pictureBox1.Visible = true;
                this.toolTip1.SetToolTip(this.pictureBox1, "The file has existed.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string OutputString = this.textBox1.Text.Trim();
            string InputString = this.comboBox1.Text.Trim();
            //The input is null
            if ((InputString == "") || (OutputString == ""))
            {
                MessageBox.Show("The parameters are not complete.", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (File.Exists(OutputString))
            {
                MessageBox.Show("The output file has existed.", "Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int selectIndex = this.comboBox1.SelectedIndex;
            if ((selectIndex < 0) && (InputString != "")) //Input data by selecting from folder
            {
                RasterSymbolizer RasterSymbol = new RasterSymbolizer();
                IRasterSymbolizer IRasterSymbol = RasterSymbol;
                MapRasterLayer RasterLayer = new MapRasterLayer(InputString, RasterSymbol); //Open the raster layer
                IRaster Raster = RasterLayer.DataSet;
                FlowAcc fa = new FlowAcc(Raster, OutputString, MainMap);
                fa.Execute();
            }
            else if (selectIndex >= 0) //Input data by selecting from combox
            {
                IMapRasterLayer[] RasterArr = MainMap.GetRasterLayers();
                IMapRasterLayer RasterLayer = RasterArr[selectIndex];
                IRaster Raster = RasterLayer.DataSet;
                FlowAcc fa = new FlowAcc(Raster, OutputString, MainMap);
                fa.Execute();
            }
        }
    }
}
