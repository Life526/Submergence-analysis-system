﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotSpatial.Controls;
using DotSpatial.Extensions;
using DotSpatial.Symbology;
using DotSpatial.Controls.Header;

namespace HydrologyPlugins
{
    public class HydrologyPlugins:Extension
    {
        public override void Activate()
        {
            base.Activate();
        }

        public override void Deactivate()
        {
            ////从菜单区域移除插件
            App.HeaderControl.RemoveAll(); 
            base.Deactivate();
        }

        private void AddMenu()
        {
            const string HydroMenuKey = "kHydrology";

            //知识点1：如何定义菜单？
            //Root menu
            App.HeaderControl.Add(new RootItem(HydroMenuKey, "Hydrology"));//定义 Root menu，其key是添加子菜单的凭据。 SortOrder=100 决定菜单出现位置顺序          
                                                                       //Add some child menus
            App.HeaderControl.Add(new SimpleActionItem(HydroMenuKey, "Fill", Func_Fill));//位于哪个Root menu之下，标题，以及菜单响应方法。
            App.HeaderControl.Add(new SimpleActionItem(HydroMenuKey, "Flow Direction", Func_FlowDir));//位于哪个Root menu之下，标题，以及菜单响应方法。
            App.HeaderControl.Add(new SimpleActionItem(HydroMenuKey, "Flow Accumulation", Func_FlowAccu));//位于哪个Root menu之下，标题，以及菜单响应方法。
            App.HeaderControl.Add(new SimpleActionItem(HydroMenuKey, "Snap Pour Point", Func_Snap));//位于哪个Root menu之下，标题，以及菜单响应方法。
            App.HeaderControl.Add(new SimpleActionItem(HydroMenuKey, "Connection", Func_Connect));//位于哪个Root menu之下，标题，以及菜单响应方法。
            App.HeaderControl.Add(new SimpleActionItem(HydroMenuKey, "Submergence Analysis", Func_Submergence));//位于哪个Root menu之下，标题，以及菜单响应方法。
        }

        private void Func_Fill(object sender, EventArgs e)
        {

        }

        private void Func_FlowDir(object sender, EventArgs e)
        {

        }

        private void Func_FlowAccu(object sender, EventArgs e)
        {

        }

        private void Func_Snap(object sender, EventArgs e)
        {

        }

        private void Func_Connect(object sender, EventArgs e)
        {

        }

        private void Func_Submergence(object sender, EventArgs e)
        {

        }
    }
}
