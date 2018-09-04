using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition; //1、微软给我们提供的强大的组件化开发框架 
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flood
{
    public partial class Form1 : Form
    {
        //2、Export出去的类型和名称都要和Import标注的属性匹配，类型可以写ITest, 也可以写Test  
        [Export("Shell", typeof(ContainerControl))]
        private static ContainerControl Shell;
        public Form1()
        {
            InitializeComponent();
            if (DesignMode) return;
            Shell = this;//3、DotSpatial的插件设计机制（框架），将ContainerControl类型的名称为Shell的容器控件作为插件的承载容器，此处即this窗体为插件容器。
            appManager1.LoadExtensions();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
