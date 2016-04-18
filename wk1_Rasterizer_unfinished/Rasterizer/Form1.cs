using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rasterizer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			this.DoubleBuffered = true;
		}
		List<Vector3> vertices = new List<Vector3>();
		List<List<int>> polygons = new List<List<int>>();
		float rotation;


		private void Form1_Load(object sender, EventArgs e)
		{
            //initialise vertices & polygons here
            vertices.Add(new Vector3(-1,-1,-1));
            vertices.Add(new Vector3(1, -1, -1));
            vertices.Add(new Vector3(1, 1, -1));
            vertices.Add(new Vector3(-1, 1, -1));
            vertices.Add(new Vector3(-1, -1, 1));
            vertices.Add(new Vector3(1, -1, 1));
            vertices.Add(new Vector3(1, 1, 1));
            vertices.Add(new Vector3(-1, 1, 1));

            polygons.Add(new List<int>() { 0, 1, 2, 3 });
            polygons.Add(new List<int>() { 4, 5, 6, 7 });
            polygons.Add(new List<int>() { 0, 1, 5, 4 });
            polygons.Add(new List<int>() { 2, 3, 7, 6 });
        }

		private void timer1_Tick(object sender, EventArgs e)
		{
            rotation += 0.01f;
			Invalidate();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
            float x, y, x2, y2;
            Matrix matr = Matrix.perspective((float)(Math.PI / 2), (float)Width / Height, 0.1f, 20f);

            var g = e.Graphics;
			g.Clear(Color.Black);

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			Pen pen = new Pen(Color.DarkGreen, 5);

            for(int i = 0; i < vertices.Count; i++)
            {
                Vector3 v = vertices[i];
                //rotation here
                v = Matrix.rotation(rotation, new Vector3(0, 1, 0)) * v;
                v = Matrix.translate(new Vector3(0, 0, -5)) * v;
                v = matr * v;


                x = (float)(Width / 2 + v.x / v.z * Width / 2);
                y = (float)(Height / 2 + v.y / v.z * Height / 2);
                g.DrawRectangle(pen, x - 5, y - 5, 10, 10);
  
             //   g.DrawLine(pen, x, y, vertices[i+1].x, vertices[i+1].y);
            }
            
            foreach(var p in polygons)
            {
                for(int i = 0; i < p.Count; i++)
                {
                    Vector3 v = vertices[p[i]];
                    Vector3 v2 = vertices[p[(i+1)%p.Count]];

                    //rotation here
                    v = Matrix.rotation(rotation, new Vector3(0, 1, 0)) * v;
                    v = Matrix.translate(new Vector3(0, 0, -5)) * v;
                    v = matr * v;


                    x = (float)(Width / 2 + v.x / v.z * Width / 2);
                    y = (float)(Height / 2 + v.y / v.z * Height / 2);

                    //rotation here
                    v2 = Matrix.rotation(rotation, new Vector3(0, 1, 0)) * v2;
                    v2 = Matrix.translate(new Vector3(0, 0, -5)) * v2;
                    v2 = matr * v2;


                    x2 = (float)(Width / 2 + v2.x / v2.z * Width / 2);
                    y2 = (float)(Height / 2 + v2.y / v2.z * Height / 2);

                    g.DrawLine(pen, x, y, x2, y2);
                }
            }
			
		}
	}
}
