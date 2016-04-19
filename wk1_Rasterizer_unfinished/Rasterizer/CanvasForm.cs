using Rasterizer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Rasterizer
{
	public partial class CanvasForm : Form
	{

        private Figure figure;
        private float rotation, rotationSpeed;
        private bool fillFlag;

        public CanvasForm()
		{
			InitializeComponent();
			DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
		{
            //initialise vertices & polygons here
            figure = new Cube();    //Load Cube
        }

		private void Timer_Tick(object sender, EventArgs e)
		{
            rotation += rotationSpeed;
			Invalidate();
		}

		private void Canvas_Paint(object sender, PaintEventArgs e)
		{
            var g = e.Graphics;
			g.Clear(Color.FromArgb(54,54,54));
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen pen = new Pen(Color.Goldenrod, 3);
            DrawLegend(g, pen);
            DrawFigure(g, pen);
		}
     
        private void DrawFigure(Graphics g, Pen pen)
        {
            foreach (var p in figure.polygonList)
            {
                for (int i = 0; i < p.Count; i++)
                {
                    g.DrawLine(pen, Rotate(p, i), Rotate(p, (i + 1) % p.Count));
                }
            }
        }

        private void DrawLegend(Graphics g, Pen pen)
        {
            Font drawFont = new Font("Roboto", 10, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            g.DrawString("1 - ", drawFont, drawBrush, 20, 20);
            g.DrawString("Cube ", drawFont, drawBrush, 40, 20);
            g.DrawString("2 - ", drawFont, drawBrush, 20, 50);
            g.DrawString("Cylinder ", drawFont, drawBrush, 40, 50);
            g.DrawString("3 - ", drawFont, drawBrush, 20, 80);
            g.DrawString("Cone", drawFont, drawBrush, 40, 80);
            g.DrawString("4 - ", drawFont, drawBrush, 20, 110);
            g.DrawString("5 - ", drawFont, drawBrush, 20, 140);
        }

        private Point Rotate(List<int> p, int i)
        {
            Matrix matr = Matrix.perspective((float)(Math.PI / 2), (float)Width / Height, 0.1f, 20f);
            Vector3 v = figure.verticeList[p[i]];
            v = Matrix.rotation(rotation, new Vector3(0, 1, 0)) * v;
            v = Matrix.translate(new Vector3(0, 0, -5)) * v;
            v = matr * v;

            int x = (int)(Width / 2 + v.x / v.z * Width / 2);
            int y = (int)(Height / 2 + v.y / v.z * Height / 2);
            return new Point(x,y);
        }

        private void CanvasForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    rotationSpeed += 0.1f;
                    break;
                case Keys.Right:
                    rotationSpeed -= 0.1f;
                    break;
                case Keys.Up:
                    break;
                case Keys.Down:
                    break;
                case Keys.Add:
                    switch (figure.GetType().Name)
                    {
                        case "Cylinder":
                            figure = new Cylinder(figure.verticeNumber + 1);
                            break;
                        case "Cone":
                            figure = new Cone(figure.verticeNumber + 1);
                            break;
                    }
                    break;
                case Keys.Subtract:
                    switch (figure.GetType().Name)
                    {
                        case "Cylinder":
                            figure = new Cylinder(figure.verticeNumber - 1);
                            break;
                        case "Cone":
                            figure = new Cone(figure.verticeNumber - 1);
                            break;
                    }
                    break;
                case Keys.D1:
                    figure = new Cube();
                    break;
                case Keys.D2:
                    figure = new Cylinder();
                    break;
                case Keys.D3:
                    figure = new Cone();
                    break;
                default:
                    rotationSpeed = 0f;
                    break;
            }
        }
    }
}
