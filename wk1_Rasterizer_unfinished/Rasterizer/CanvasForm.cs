using Rasterizer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Rasterizer.Libary;

namespace Rasterizer
{
	public partial class CanvasForm : Form
	{

        private IFigure _figure;
        private float _rotation, _rotationSpeed;
        private Vector3 _rotationVector = new Vector3(0, 0, 0);
	    private int _zoom = -5;
	    private int _xValue, _yValue = 0;
	    private Color _backround = Color.FromArgb(54, 54, 54);
        private Color _penColor = Color.Goldenrod;

        public CanvasForm()
		{
			InitializeComponent();
			DoubleBuffered = true;
            Focus();
		}

        private void Form1_Load(object sender, EventArgs e)
		{
            //initialise vertices & polygons here
            _figure = new Cube();    //Load Cube
        }

		private void Timer_Tick(object sender, EventArgs e)
		{
            _rotation += _rotationSpeed;
			Invalidate();
		}

		private void Canvas_Paint(object sender, PaintEventArgs e)
		{
            var g = e.Graphics;
			g.Clear(_backround);
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var pen = new Pen(_penColor, 3);
            DrawLegend(g, pen);
            DrawFigure(g, pen);
		}
     
        private void DrawFigure(Graphics g, Pen pen)
        {
            foreach (var p in _figure.PolygonList)
            {
                for (var i = 0; i < p.Count; i++)
                {
                    g.DrawLine(pen, Rotate(p, i), Rotate(p, (i + 1) % p.Count));
                }
            }
        }

        private void DrawLegend(Graphics g, Pen pen)
        {
            var drawFont = new Font("Roboto", 10, FontStyle.Bold);
            var drawBrush = new SolidBrush(Color.White);
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

        private Point Rotate(IReadOnlyList<int> p, int i)
        {
            var matr = Matrix.Perspective((float)(Math.PI / 2), (float)Width / Height, 0.1f, 80f);
            var v = _figure.VerticeList[p[i]];
            v = Matrix.Rotation(_rotation, _rotationVector) * v;
            v = Matrix.Translate(new Vector3(0, 0, _zoom)) * v;
            v = matr * v;

            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            // ReSharper disable once PossibleLossOfFraction
            int x = (int)(Width / 2 + v.X / v.Z * Width / 2) + _xValue;
            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            // ReSharper disable once PossibleLossOfFraction
            int y = (int)(Height / 2 + v.Y / v.Z * Height / 2) + _yValue;
            return new Point(x,y);
        }

	    private void ChangeColor(bool foreground)
	    {
            var dialog = new ColorDialog();
	        var r = dialog.ShowDialog();
	        if (r != DialogResult.OK) return;
	        if (!foreground)
	            _backround = dialog.Color;
	        else
	            _penColor = dialog.Color;
	    }

        private void CanvasForm_KeyDown(object sender, KeyEventArgs e)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.KeyCode)
            {
                //rotation
                case Keys.A:
                    _rotationVector = new Vector3(0, 1, 0);
                    _rotationSpeed += 0.1f;
                    break;
                case Keys.D:
                    _rotationVector = new Vector3(0, 1, 0);
                    _rotationSpeed -= 0.1f;
                    break;
                case Keys.W:
                    _rotationVector = new Vector3(1, 0, 0);
                    _rotationSpeed -= 0.1f;
                    break;
                case Keys.S:
                    _rotationVector = new Vector3(1, 0, 0);
                    _rotationSpeed += 0.1f;
                    break;
                case Keys.Q:
                    _rotationVector = new Vector3(0, 0, 1);
                    _rotationSpeed -= 0.1f;
                    break;
                case Keys.E:
                    _rotationVector = new Vector3(0, 0, 1);
                    _rotationSpeed += 0.1f;
                    break;

                //add vertices
                case Keys.Add:
                    switch (_figure.GetType().Name)
                    {
                        case "Cylinder":
                            _figure = new Cylinder(_figure.VerticeNumber + 1);
                            break;
                        case "Cone":
                            _figure = new Cone(_figure.VerticeNumber + 1);
                            break;
                    }
                    break;
                case Keys.Subtract:
                    switch (_figure.GetType().Name)
                    {
                        case "Cylinder":
                            _figure = new Cylinder(_figure.VerticeNumber - 1);
                            break;
                        case "Cone":
                            _figure = new Cone(_figure.VerticeNumber - 1);
                            break;
                    }
                    break;

                //change shape
                case Keys.D1:
                    _figure = new Cube();
                    break;
                case Keys.D2:
                    _figure = new Cylinder();
                    break;
                case Keys.D3:
                    _figure = new Cone();
                    break;
                case Keys.D4:
                    _figure = new Obj();
                    break;

                //zoom
                case Keys.R:
                    _zoom += 1;
                    break;
                case Keys.F:
                    _zoom -= 1;
                    break;
                case Keys.T:
                    _zoom = -5;
                    break;

                //translate
                case Keys.Z:
                    _xValue += 5;
                    break;
                case Keys.X:
                    _xValue -= 5;
                    break;
                case Keys.C:
                    _xValue = 0;
                    break;
                case Keys.V:
                    _yValue += 5;
                    break;
                case Keys.B:
                    _yValue -= 5;
                    break;
                case Keys.N:
                    _yValue = 0;
                    break;

                //color
                case Keys.K:
                    ChangeColor(true);
                    break;
                case Keys.L:
                    ChangeColor(false);
                    break;
                   
                //default
                default:
                    _rotationVector = new Vector3(0, 0, 0);
                    _rotationSpeed = 0f;
                    break;
            }
        }
    }
}
