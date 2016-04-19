using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasterizer.Models
{
    class Cone : Figure
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

        public Cone()
        {
            //Define Points
            ReplaceVerticeNumber(verticeNr);
        }

        public Cone(int i)
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
            List<int> p = new List<int>();

            vertices.Add(new Vector3(0, -2, 0));

            for (var theta = 0.0; theta < 2 * Math.PI; theta += step)
            {
                k++;
                float x = (float)(r * Math.Cos(theta));
                float z = (float)(r * Math.Sin(theta));
                vertices.Add(new Vector3(x, y, z));
                polygons.Add(new List<int>() { 0, vertices.Count - 1 });
                p.Add(k);
            }
            polygons.Add(p);
        }

    }
}
