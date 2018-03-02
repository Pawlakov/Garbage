using System;
using System.Drawing;
using System.Windows.Forms;

namespace MN
{
	public partial class Form1 : Form
	{
		private double variance = 1.0;
		private double meanValue = 0.0;
		private PointF[] p = new PointF[1000];
		public Form1()
		{
			InitializeComponent();
			Paint += new PaintEventHandler(Form1_Paint);
			Calc();
		}
		private void Calc()
		{
			double x;
			for (int i = 0; i < 1000; i++)
			{
				x = (i * 0.01) - 5.0;
				double exponentNominator = Math.Pow(x - meanValue, 2.0);
				double exponentDenominator = 2.0 * variance;
				double exponent = -(exponentNominator / exponentDenominator);
				double nominator = Math.Pow(Math.E, exponent);
				double denominator = Math.Sqrt(2.0 * Math.PI * variance);
				double y = nominator / denominator;
				p[i] = new PointF((float)(x*50.0), (float)(y*500.0));
			}
		}
		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			e.Graphics.TranslateTransform(200, 200);
			e.Graphics.ScaleTransform(1, -1);
			e.Graphics.DrawLines(Pens.Blue, p);
		}
	}
}