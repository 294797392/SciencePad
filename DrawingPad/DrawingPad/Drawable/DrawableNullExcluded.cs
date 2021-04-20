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
    public class DrawableNullExcluded : DrawableVisual
    {
        public override int CircleHandles { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        public override int RectangleHandles { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

        public DrawableNullExcluded(GraphicsBase graphics) : 
            base(graphics)
        {
        }

        public override bool Contains(Point p)
        {
            throw new NotImplementedException();
        }

        protected override void RenderCore(DrawingContext dc)
        {
            throw new NotImplementedException();
        }
    }
}
