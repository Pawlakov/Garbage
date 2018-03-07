﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace MN
{
    public partial class Form1 : Form
    {
        private const double variance = 24.0;
        private const double meanValue = 60.0;
        //
        private const int exactCount = 1000;
        private const double exactStep = 0.1;
        //
        private const int discreteCount = 100;
        private const double discreteStep = 1.0;
        private const float boxRadius = 2.0f;
        //
        private float? plotHeight;
        //
        private PointF[] exact = new PointF[exactCount];
        private PointF[] discrete = new PointF[discreteCount];
        public Form1()
        {
            plotHeight = null;
            InitializeComponent();
            ResizeRedraw = true;
            exact = Calc(exactCount, exactStep);
            discrete = Calc(discreteCount, discreteStep);
            Paint += new PaintEventHandler(Draw);
        }
        private PointF[] Calc(int count, double step)
        {
            PointF[] result = new PointF[count];
            double x;
            for (int i = 0; i < count; ++i)
            {
                x = i * step;
                double exponentNominator = Math.Pow(x - meanValue, 2.0);
                double exponentDenominator = 2.0 * variance;
                double exponent = -(exponentNominator / exponentDenominator);
                double nominator = Math.Pow(Math.E, exponent);
                double denominator = Math.Sqrt(2.0 * Math.PI * variance);
                double y = nominator / denominator;
                result[i] = new PointF((float)(x), (float)(y));
            }
            return result;
        }
        private void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            DrawCoordinates(e);
            PointF[] transformedExact = Transform(exact);
            PointF[] transformedDiscrete = Transform(discrete);
            foreach (PointF point in transformedDiscrete)
                e.Graphics.DrawLines(Pens.Red, PointToMarker(point));
            e.Graphics.DrawLines(Pens.Blue, transformedExact);
        }
        private void DrawCoordinates(PaintEventArgs e)
        {
            float currentX;
            for(int i = 0; i < 11; ++i)
            {
                currentX = 10.0f + i * 10.0f * ((Width - 20) / 100.0f);
                e.Graphics.DrawLine(Pens.Black, new PointF(currentX, 0), new PointF(currentX, Height));
            }
        }
        private float PlotHeight
        {
            get
            {
                if (!plotHeight.HasValue)
                {
                    plotHeight = exact[0].Y;
                    foreach (PointF point in exact)
                    {
                        if (point.Y > plotHeight)
                            plotHeight = point.Y;
                    }
                }
                return plotHeight.Value;
            }
        }
        private PointF[] Transform(PointF[] values)
        {
            PointF[] result = new PointF[values.Length];
            float horizontalScale = (Width - 20) / 100.0f;
            float verticalScale = (Height - 20 - 40) / PlotHeight;
            for (int i = 0; i < values.Length; ++i)
                result[i] = new PointF(values[i].X * horizontalScale + 10, Height - 40 - (values[i].Y * verticalScale + 10));
            return result;
        }
        private PointF[] PointToMarker(PointF point)
        {
            PointF[] result = new PointF[5];
            result[0] = new PointF(point.X - boxRadius, point.Y - boxRadius);
            result[1] = new PointF(point.X - boxRadius, point.Y + boxRadius);
            result[2] = new PointF(point.X + boxRadius, point.Y + boxRadius);
            result[3] = new PointF(point.X + boxRadius, point.Y - boxRadius);
            result[4] = new PointF(point.X - boxRadius, point.Y - boxRadius);
            return result;
        }
    }
}