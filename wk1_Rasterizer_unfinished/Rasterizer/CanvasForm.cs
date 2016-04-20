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
        private Vector3 rotationVector = new Vector3(0, 0, 0);
	    private int zoom = -5;
	    private int xValue, yValue = 0;
	    private Color backround = Color.FromArgb(54, 54, 54);
        private Color penColor = Color.Goldenrod;

        public CanvasForm()
		{
			InitializeComponent();
			DoubleBuffered = true;
            Focus();
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
			g.Clear(backround);
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen pen = new Pen(penColor, 3);
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
            g.DrawString("Obj file", drawFont, drawBrush, 40, 110);
            g.DrawString("W,A,S,D,Q,E - ", drawFont, drawBrush, 20, 140);
            g.DrawString("Rotate", drawFont, drawBrush, 120, 140);
            g.DrawString("Z,X,C,V,B,N - ", drawFont, drawBrush, 20, 170);
            g.DrawString("Move", drawFont, drawBrush, 115, 170);
            g.DrawString("K,L - ", drawFont, drawBrush, 20, 200);
            g.DrawString("Change color", drawFont, drawBrush, 55, 200);
        }

        private Point Rotate(List<int> p, int i)
        {
            Matrix matr = Matrix.perspective((float)(Math.PI / 2), (float)Width / Height, 0.1f, 80f);
            Vector3 v = figure.verticeList[p[i]];
            v = Matrix.rotation(rotation, rotationVector) * v;
            v = Matrix.translate(new Vector3(0, 0, zoom)) * v;
            v = matr * v;

            int x = (int)(Width / 2 + v.x / v.z * Width / 2) + xValue;
            int y = (int)(Height / 2 + v.y / v.z * Height / 2) + yValue;
            return new Point(x,y);
        }

	    private void changeColor(bool foreground)
	    {
            ColorDialog dialog = new ColorDialog();
	        DialogResult r = dialog.ShowDialog();
	        if (r != DialogResult.OK) return;
	        if (!foreground)
	            backround = dialog.Color;
	        else
	            penColor = dialog.Color;
	    }

        private void CanvasForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //rotation
                case Keys.A:
                    rotationVector = new Vector3(0, 1, 0);
                    rotationSpeed += 0.1f;
                    break;
                case Keys.D:
                    rotationVector = new Vector3(0, 1, 0);
                    rotationSpeed -= 0.1f;
                    break;
                case Keys.W:
                    rotationVector = new Vector3(1, 0, 0);
                    rotationSpeed -= 0.1f;
                    break;
                case Keys.S:
                    rotationVector = new Vector3(1, 0, 0);
                    rotationSpeed += 0.1f;
                    break;
                case Keys.Q:
                    rotationVector = new Vector3(0, 0, 1);
                    rotationSpeed -= 0.1f;
                    break;
                case Keys.E:
                    rotationVector = new Vector3(0, 0, 1);
                    rotationSpeed += 0.1f;
                    break;

                //add vertices
                case Keys.Add:
                    switch (figure.GetType().Name)
                    {
                        case "Cylinder":
                            figure = new Cylinder(figure.verticeNumber + 1);
                            break;
                        case "Cone":
                            figure = new Cone(figure.verticeNumber + 1);
                            break;
                        default:
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
                        default:
                            break;
                    }
                    break;

                //change shape
                case Keys.D1:
                    figure = new Cube();
                    break;
                case Keys.D2:
                    figure = new Cylinder();
                    break;
                case Keys.D3:
                    figure = new Cone();
                    break;
                case Keys.D4:
                    figure = new obj();
                    break;

                //zoom
                case Keys.R:
                    zoom += 1;
                    break;
                case Keys.F:
                    zoom -= 1;
                    break;
                case Keys.T:
                    zoom = -5;
                    break;

                //translate
                case Keys.Z:
                    xValue += 5;
                    break;
                case Keys.X:
                    xValue -= 5;
                    break;
                case Keys.C:
                    xValue = 0;
                    break;
                case Keys.V:
                    yValue += 5;
                    break;
                case Keys.B:
                    yValue -= 5;
                    break;
                case Keys.N:
                    yValue = 0;
                    break;

                //color
                case Keys.K:
                    changeColor(true);
                    break;
                case Keys.L:
                    changeColor(false);
                    break;
                   
                //default
                default:
                    rotationVector = new Vector3(0, 0, 0);
                    rotationSpeed = 0f;
                    break;
            }
        }
    }
}
