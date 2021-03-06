﻿using DrawingPad.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawingPad.Drawable
{
    public class DrawableNull : DrawableVisual
    {
        public static readonly DrawableNull Empty = new DrawableNull();

        private static readonly PointCollection TrackerPoints = new PointCollection();

        public DrawableNull() : base(null)
        {

        }

        public override PointCollection GetCircleHandles()
        {
            return TrackerPoints;
        }

        public override PointCollection GetRectangleHandles()
        {
            throw new NotImplementedException();
        }

        public override Point GetRotationHandle()
        {
            throw new NotImplementedException();
        }

        protected override void RenderCore(DrawingContext dc)
        {
        }
    }
}
