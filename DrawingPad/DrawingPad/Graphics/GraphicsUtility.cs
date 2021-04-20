﻿using DrawingPad.Drawable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawingPad.Graphics
{
    public static class GraphicsUtility
    {
        public static Point GetCenter(this Rect rect)
        {
            return new Point(rect.Location.X + rect.Width / 2, rect.Location.Y + rect.Height / 2);
        }

        /// <summary>
        /// 获取点p相对于visual的顶点位置
        /// 注意，点P一定是一个顶点
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static GraphicsVertexLocation GetVertexLocation(GraphicsBase graphics, Point p)
        {
            Rect bounds = graphics.GetBounds();

            Point leftTop = bounds.Location;

            if (p.X == leftTop.X && p.Y == leftTop.Y + bounds.Height / 2)
            {
                return GraphicsVertexLocation.LeftCenter;
            }
            else if (p.X == leftTop.X + bounds.Width / 2 && p.Y == leftTop.Y)
            {
                return GraphicsVertexLocation.TopCenter;
            }
            else if (p.X == leftTop.X + bounds.Width / 2 && p.Y == leftTop.Y + bounds.Height)
            {
                return GraphicsVertexLocation.BottomCenter;
            }
            else if (p.X == leftTop.X + bounds.Width && p.Y == leftTop.Y + bounds.Height / 2)
            {
                return GraphicsVertexLocation.RightCenter;
            }
            else if (p.X == leftTop.X && p.Y == leftTop.Y)
            {
                return GraphicsVertexLocation.TopLeft;
            }
            else if (p.X == bounds.TopRight.X && p.Y == bounds.TopRight.Y)
            {
                return GraphicsVertexLocation.TopRight;
            }
            else if (p.X == bounds.BottomLeft.X && p.Y == bounds.BottomLeft.Y)
            {
                return GraphicsVertexLocation.BottomLeft;
            }
            else if (p.X == bounds.BottomRight.X && p.Y == bounds.BottomRight.Y)
            {
                return GraphicsVertexLocation.BottomRight;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取点P相对于图形的位置
        /// 点P可以是任意一点
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static RelativeLocation GetRelativeLocation(GraphicsBase graphics, Point p)
        {
            Rect bounds = graphics.GetBounds();

            if (p.X >= bounds.TopLeft.X && p.X <= bounds.TopRight.X && p.Y <= bounds.TopLeft.Y)
            {
                // 点在图形的上方
                return RelativeLocation.Top;
            }

            if (p.X <= bounds.TopLeft.X && p.Y >= bounds.TopLeft.Y && p.Y <= bounds.BottomLeft.Y)
            {
                // 点在图形的左边
                return RelativeLocation.Left;
            }

            if (p.Y >= bounds.BottomLeft.Y && p.X >= bounds.TopLeft.X && p.X <= bounds.TopRight.X)
            {
                // 点在图形的下面
                return RelativeLocation.Bottom;
            }

            if (p.X >= bounds.TopRight.X && p.Y >= bounds.TopRight.Y && p.Y <= bounds.BottomRight.Y)
            {
                // 点在图形的右边
                return RelativeLocation.Right;
            }

            if (p.X < bounds.TopLeft.X && p.Y < bounds.TopLeft.Y)
            {
                // 点在图形的左上方
                return RelativeLocation.TopLeft;
            }

            if (p.X > bounds.TopRight.X && p.Y < bounds.TopRight.Y)
            {
                // 点在图形的右上方
                return RelativeLocation.TopRight;
            }

            if (p.X < bounds.BottomLeft.X && p.Y > bounds.BottomLeft.Y)
            {
                // 点在图形的左下方
                return RelativeLocation.BottomLeft;
            }

            if (p.X > bounds.BottomRight.X && p.Y > bounds.BottomRight.Y)
            {
                // 点在图形的右下方
                return RelativeLocation.BottomRight;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startGraphics">连接线的第一个图形</param>
        /// <param name="startPoint">第一个图形上的连接点的坐标</param>
        /// <param name="cursorPos">当前的鼠标坐标</param>
        /// <param name="endGraphics">连接的第二个图形</param>
        /// <returns></returns>
        public static List<Point> MakeConnectionPoints(GraphicsBase startGraphics, Point startPoint, Point cursorPos, GraphicsBase endGraphics)
        {
            double cursorX = cursorPos.X;
            double cursorY = cursorPos.Y;
            double startX = startPoint.X;
            double startY = startPoint.Y;

            if (startX == cursorX && startY == cursorY)
            {
                return null;
            }

            GraphicsVertexLocation vertex = GetVertexLocation(startGraphics, startPoint);

            Rect startGraphicsBounds = startGraphics.GetBounds();

            List<Point> pointList = new List<Point>();

            pointList.Add(startPoint);

            if (cursorX > startX && cursorY > startY)
            {
                // 往右下方拖动
                Console.WriteLine("往右下方拖动");

                switch (vertex)
                {
                    case GraphicsVertexLocation.LeftCenter:
                        {
                            // 左边的点，往右下方拖动
                            pointList.Add(new Point(startX - PadContext.MinimalMargin, startY));
                            pointList.Add(new Point(startX - PadContext.MinimalMargin, cursorY));

                            // 和另外一个图形连接起来了
                            if (endGraphics != null)
                            {
                                // 计算鼠标处于第二个图形的什么位置
                                RelativeLocation location = GetRelativeLocation(endGraphics, cursorPos);
                            }

                            break;
                        }

                    case GraphicsVertexLocation.TopCenter:
                        {
                            // 上边的点，往右下方拖动
                            pointList.Add(new Point(startX, startY - PadContext.MinimalMargin));
                            pointList.Add(new Point(cursorX, startY - PadContext.MinimalMargin));
                            break;
                        }

                    case GraphicsVertexLocation.RightCenter:
                        {
                            // 右边的点，往右下方拖动
                            pointList.Add(new Point(cursorX, startY));
                            break;
                        }

                    case GraphicsVertexLocation.BottomCenter:
                        {
                            // 下边的点，往右下方拖动
                            pointList.Add(new Point(startX, cursorY));
                            break;
                        }
                }
            }
            else if (cursorX > startX && cursorY < startY)
            {
                // 往右上方拖动
                Console.WriteLine("往右上方拖动");

                switch (vertex)
                {
                    case GraphicsVertexLocation.LeftCenter:
                        {
                            // 左边的点，往右上方拖动
                            pointList.Add(new Point(startX - PadContext.MinimalMargin, startY));
                            pointList.Add(new Point(startX - PadContext.MinimalMargin, cursorY));
                            break;
                        }

                    case GraphicsVertexLocation.TopCenter:
                        {
                            // 上边的点，往右上方拖动
                            pointList.Add(new Point(startX, cursorY));
                            break;
                        }

                    case GraphicsVertexLocation.RightCenter:
                        {
                            // 右边的点，往右上方拖动
                            pointList.Add(new Point(cursorX, startY));
                            break;
                        }

                    case GraphicsVertexLocation.BottomCenter:
                        {
                            // 下边的点，往右上方拖动
                            pointList.Add(new Point(startX, startY + PadContext.MinimalMargin));
                            pointList.Add(new Point(cursorX, startY + PadContext.MinimalMargin));
                            break;
                        }
                }
            }
            else if (cursorX < startX && cursorY > startY)
            {
                // 往左下方拖动
                Console.WriteLine("往左下方拖动");

                switch (vertex)
                {
                    case GraphicsVertexLocation.LeftCenter:
                        {
                            // 左边的点，往左下方拖动
                            pointList.Add(new Point(cursorX, startPoint.Y));
                            break;
                        }

                    case GraphicsVertexLocation.TopCenter:
                        {
                            // 上边的点，往左下方拖动
                            pointList.Add(new Point(startX, startY - PadContext.MinimalMargin));
                            pointList.Add(new Point(cursorX, startY - PadContext.MinimalMargin));
                            break;
                        }

                    case GraphicsVertexLocation.RightCenter:
                        {
                            // 右边的点，往左下方拖动
                            pointList.Add(new Point(startX + PadContext.MinimalMargin, startY));
                            pointList.Add(new Point(startX + PadContext.MinimalMargin, cursorY));
                            break;
                        }

                    case GraphicsVertexLocation.BottomCenter:
                        {
                            // 下边的点，往左下方拖动
                            pointList.Add(new Point(startX, cursorY));
                            break;
                        }
                }
            }
            else if (cursorX < startX && cursorY < startY)
            {
                // 往左上方拖动
                Console.WriteLine("往左上方拖动");

                switch (vertex)
                {
                    case GraphicsVertexLocation.LeftCenter:
                        {
                            // 左边的点，往左上方拖动
                            pointList.Add(new Point(cursorX, startPoint.Y));
                            break;
                        }

                    case GraphicsVertexLocation.TopCenter:
                        {
                            // 上边的点，往左上方拖动
                            pointList.Add(new Point(startX, cursorY));
                            break;
                        }

                    case GraphicsVertexLocation.RightCenter:
                        {
                            // 右边的点，往左上方拖动
                            pointList.Add(new Point(startX + PadContext.MinimalMargin, startY));
                            pointList.Add(new Point(startX + PadContext.MinimalMargin, cursorY));
                            break;
                        }

                    case GraphicsVertexLocation.BottomCenter:
                        {
                            // 下边的点，往左上方拖动
                            pointList.Add(new Point(startX, startY + PadContext.MinimalMargin));
                            pointList.Add(new Point(cursorX, startY + PadContext.MinimalMargin));
                            break;
                        }
                }
            }
            else if (cursorX > startX && cursorY == startY)
            {
                // 往右拖动
                Console.WriteLine("往右拖动");

                switch (vertex)
                {
                    case GraphicsVertexLocation.LeftCenter:
                        {
                            break;
                        }

                    case GraphicsVertexLocation.TopCenter:
                        {
                            break;
                        }

                    case GraphicsVertexLocation.RightCenter:
                        {
                            break;
                        }

                    case GraphicsVertexLocation.BottomCenter:
                        {
                            break;
                        }
                }
            }
            else if (cursorX < startX && cursorY == startY)
            {
                // 往左拖动
                Console.WriteLine("往左拖动");

                switch (vertex)
                {
                    case GraphicsVertexLocation.LeftCenter:
                        {
                            break;
                        }

                    case GraphicsVertexLocation.TopCenter:
                        {
                            break;
                        }

                    case GraphicsVertexLocation.RightCenter:
                        {
                            break;
                        }

                    case GraphicsVertexLocation.BottomCenter:
                        {
                            break;
                        }
                }
            }
            else if (cursorX == startX && cursorY > startY)
            {
                // 往下拖动
                Console.WriteLine("往下拖动");

                switch (vertex)
                {
                    case GraphicsVertexLocation.LeftCenter:
                        {
                            break;
                        }

                    case GraphicsVertexLocation.TopCenter:
                        {
                            break;
                        }

                    case GraphicsVertexLocation.RightCenter:
                        {
                            break;
                        }

                    case GraphicsVertexLocation.BottomCenter:
                        {
                            break;
                        }
                }
            }
            else if (cursorX == startX && cursorY < startY)
            {
                // 往上拖动
                Console.WriteLine("往上拖动");

                switch (vertex)
                {
                    case GraphicsVertexLocation.LeftCenter:
                        {
                            break;
                        }

                    case GraphicsVertexLocation.TopCenter:
                        {
                            break;
                        }

                    case GraphicsVertexLocation.RightCenter:
                        {
                            break;
                        }

                    case GraphicsVertexLocation.BottomCenter:
                        {
                            break;
                        }
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            pointList.Add(cursorPos);

            return pointList;
        }

        /// <summary>
        /// 左上角和右下角组成一个矩形
        /// </summary>
        /// <param name="leftTop"></param>
        /// <param name="rightBottom"></param>
        /// <returns></returns>
        public static Rect MakeRect(Point leftTop, Point rightBottom)
        {
            Rect rect = new Rect()
            {
                Location = leftTop,
                Width = rightBottom.X - leftTop.X,
                Height = rightBottom.Y - leftTop.Y
            };
            return rect;
        }
    }
}
