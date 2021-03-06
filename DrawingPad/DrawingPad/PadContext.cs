﻿using DotNEToolkit;
using DrawingPad.DataModels;
using DrawingPad.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DrawingPad
{
    public class PadContext
    {
        #region 类变量

        private static log4net.ILog logger = log4net.LogManager.GetLogger("PadContext");

        #endregion

        #region 常量定义

        private const int DefaultWidth = 1;
        private const int LineWidth = 2;

        public const int CircleTrackerRadius = 4;

        public const int RectangleTrackerSize = 8;

        /// <summary>
        /// 连接线到图形的最小边距
        /// </summary>
        public const int MinimalMargin = 20;

        /// <summary>
        /// 缩放图形的时候，图形最小的大小
        /// </summary>
        public const int MinimalVisualSize = 30;

        #endregion

        #region 画刷定义

        public static readonly Brush TrackerBackground = Brushes.White;

        public static readonly Brush DefaultFillBrush = Brushes.White;
        public static readonly Pen DefaultPen = new Pen(Brushes.Black, 1);

        public const int ConnectionLocationTolerance = 20;

        #endregion

        #region 画笔定义

        public static readonly Pen TrackerPen = new Pen(Brushes.Black, DefaultWidth);

        #endregion

        #region 常量定义

        public static readonly string ToolboxJsonPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "toolbox.json");

        #endregion

        #region 单例

        private static PadContext context = new PadContext();

        public static PadContext Context { get { return context; } }

        #endregion

        #region 属性

        public ToolboxViewModel ToolboxVM { get; private set; }
        
        public ObservableCollection<GraphicsGroupVM> GroupList { get; private set; }

        #endregion

        private PadContext()
        {
        }

        #region 公开接口

        public void Initialize()
        {
            this.GroupList = new ObservableCollection<GraphicsGroupVM>();

            List<ToolboxGroup> groups;
            if (!JSONHelper.TryParseFile<List<ToolboxGroup>>(ToolboxJsonPath, out groups))
            {
                logger.Error("加载工具箱分组失败");
                return;
            }

            foreach (ToolboxGroup group in groups)
            {
                GraphicsGroupVM groupVM = new GraphicsGroupVM()
                {
                    ID = group.ID,
                    Name = group.Name,
                };

                foreach (ToolboxItem toolboxItem in group.Items)
                {
                    GraphicsVM gvm = new GraphicsVM()
                    {
                        ID = toolboxItem.ID,
                        Name = toolboxItem.Name,
                        IconURI = toolboxItem.Icon,
                        Type = toolboxItem.Type
                    };

                    groupVM.Items.Add(gvm);
                }

                this.GroupList.Add(groupVM);
            }

            this.ToolboxVM = new ToolboxViewModel();
        }

        #endregion
    }
}
