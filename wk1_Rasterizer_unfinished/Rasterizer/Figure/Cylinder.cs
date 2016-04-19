using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasterizer.Models
{
    class Cylinder : Figure
    {
        private List<Vector3> vertices = new List<Vector3>();
        private List<List<int>> polygons = new List<List<int>>();
        private int verticeNr = 20;

        public List<Vector3> verticeList
        {
            get
            {
                return vertices;
            }
        }

        public List<List<int>> polygonList
        {
            get
            {
                return polygons;
            }
        }

        public int verticeNumber
        {
            get
            {
                return verticeNr;
            }
        }

        public Cylinder()
        {
            //Define Points
            ReplaceVerticeNumber(verticeNr);
        }

        public Cylinder(int i)
        {
            //Define Points
            ReplaceVerticeNumber(i);
            verticeNr = i;
        }


        public void ReplaceVerticeNumber(int nr)
        {
            int k = 0;
            float y = 2;
            double r = 1.5;
            var varStep = nr;
            var step = 2 * Math.PI / varStep;
            List<int> p1 = new List<int>();
            List<int> p2 = new List<int>();

            for (var theta = 0.0; theta < 4 * Math.PI; theta += step)
            {
                if (theta >= 2 * Math.PI && y == 2)
                {
                    y = -2;
                }
                float x = (float)(r * Math.Cos(theta));
                float z = (float)(r * Math.Sin(theta));
                vertices.Add(new Vector3(x, y, z));
                if (k < varStep)
                {
                    p1.Add(k);
                    polygons.Add(new List<int>() { k, k + varStep });
                }
                else
                {
                    p2.Add(k);
                }

                k++;
            }
            polygons.Add(p1);
            polygons.Add(p2);
        }

    }
}
