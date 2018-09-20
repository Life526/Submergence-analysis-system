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
using DotSpatial.Extensions;
using DotSpatial.Projections;
using DotSpatial.Symbology;
using System.IO;

namespace HydrologyPlugins
{
    public partial class Snap_pour_point : Form
    {
        IMap MainMap = null;
        public Snap_pour_point(IMap InputMap)
        {
            InitializeComponent();
            MainMap = InputMap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Snap_pour_point_Load(object sender, EventArgs e)
        {
            //Add the layers in the map to the combox
            IMapRasterLayer[] RasterArr = MainMap.GetRasterLayers();
            if (RasterArr.Length > 0)
            {
                for (int i = 0; i < RasterArr.Length; i++)
                {
                    string RasterName = RasterArr[i].LegendText;
                    this.comboBox1.Items.Add(RasterName);
                    this.comboBox2.Items.Add(RasterName);
                }
            }

            IMapPointLayer[] PointArr = MainMap.GetPointLayers();
            if(PointArr.Length>0)
            {
                for(int i=0;i<PointArr.Length;i++)
                {
                    string PointName = PointArr[i].LegendText;
                    this.comboBox3.Items.Add(PointName);
                }
            }
        }

        bool fileSelect1 = false;
        bool fileSelect2 = false;
        bool fileSelect3 = false;

        //Open data
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a raster data";
            ofd.Filter = "Raster(*.tif;*.img)|*.tif;*.img";
            ofd.ValidateNames = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.comboBox1.Text = ofd.FileName;
                fileSelect2 = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a raster data";
            ofd.Filter = "Raster(*.tif;*.img)|*.tif;*.img";
            ofd.ValidateNames = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.comboBox2.Text = ofd.FileName;
                fileSelect3 = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a raster data";
            ofd.Filter = "Raster(*.tif;*.img)|*.tif;*.img";
            ofd.ValidateNames = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.comboBox3.Text = ofd.FileName;
                fileSelect1 = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save the raster date";
            sfd.Filter = "Shapefile(*.shp)|*.shp";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = sfd.FileName;
            }
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            string InputPoint = this.comboBox3.Text.Trim();
            string InputAccRaster = this.comboBox1.Text.Trim();
            string InputSurRaster = this.comboBox2.Text.Trim();
            string InputRadius = this.textBox1.Text.Trim();
            string OutputPoint = this.textBox2.Text.Trim();
            int fileIndex1 = this.comboBox3.SelectedIndex;
            int fileIndex2 = this.comboBox1.SelectedIndex;
            int fileIndex3 = this.comboBox2.SelectedIndex;
            if(InputPoint==""||InputAccRaster==""||InputSurRaster==""||InputRadius==""||OutputPoint=="")
            {
                MessageBox.Show("The parameters are not complete.","Admin",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(fileSelect1==true&&fileSelect2==true&&fileSelect3==true)
            {

            }
        }
    }
}
