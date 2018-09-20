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
using DotSpatial.Controls.Header;

namespace MapRefresh
{
    public class MapControlPlugins:Extension
    {
        //Plugin加载时，执行Activate方法。
        public override void Activate()
        {
            AddTool();
            base.Activate();
        }

        //从菜单区域移除插件
        public override void Deactivate()
        {
            App.HeaderControl.RemoveAll();
            base.Deactivate();
        }

        private void AddTool()
        {
            const string MapToolKey = "kMapTool";

            //知识点2：如何定义工具栏？
            //SimpleActionItem一旦设定GroupCaption属性，即按GroupCaption值，在工具栏中以分组按钮（或DropDown控件）等形式出现；
            //不会再以MenuItem形式出现在菜单栏中!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            App.HeaderControl.Add(new SimpleActionItem(MapToolKey, "MapRefresh", RefreshMap)
            {
                GroupCaption = MapToolKey, //按GroupCaption值，在工具栏中以分组按钮（或DropDown控件）
                SortOrder = 10,
                SmallImage = Properties.Resources.Refresh,
                LargeImage = Properties.Resources.Refresh,
                ToolTipText = "MapRefresh"
            });

            string StatusPanelKey = "X:0.000000  Y:0.000000";
            CoordinateTxt.Caption = StatusPanelKey;
            App.ProgressHandler.Add(CoordinateTxt);
            Map TempMap = (Map)App.Map;
            TempMap.GeoMouseMove += new System.EventHandler<DotSpatial.Controls.GeoMouseArgs>(this.CoordinateDisplay);
        }

        StatusPanel CoordinateTxt = new StatusPanel();
        
        private void RefreshMap(object sender, EventArgs e)
        {
            App.Map.Refresh();
        }

        private void CoordinateDisplay(object sender,GeoMouseArgs e)
        {
            string locStr = "X:" + e.GeographicLocation.X.ToString("F6");
            locStr += "  Y:" + e.GeographicLocation.Y.ToString("F6");
            CoordinateTxt.Caption = locStr;
        }
    }
}
