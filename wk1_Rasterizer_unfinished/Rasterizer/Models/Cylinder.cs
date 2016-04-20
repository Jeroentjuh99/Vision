using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasterizer.Libary;

namespace Rasterizer.Models
{
    class Cylinder : IFigure
    {
        private List<Vector3> _vertices = new List<Vector3>();
        private List<List<int>> _polygons = new List<List<int>>();
        private int _verticeNr = 20;

        public List<Vector3> VerticeList => _vertices;

        public List<List<int>> PolygonList => _polygons;

        public int VerticeNumber => _verticeNr;

        public Cylinder()
        {
            //Define Points
            ReplaceVerticeNumber(_verticeNr);
        }

        public Cylinder(int i)
        {
            //Define Points
            ReplaceVerticeNumber(i);
            _verticeNr = i;
        }


        public void ReplaceVerticeNumber(int nr)
        {
            int k = 0;
            float y = 2;
            double r = 1.5;
            var varStep = nr;
            var step = 2 * Math.PI / varStep;
            var p1 = new List<int>();
            var p2 = new List<int>();

            for (var theta = 0.0; theta < 4 * Math.PI; theta += step)
            {
                if (theta >= 2 * Math.PI && y == 2)
                {
                    y = -2;
                }
                var x = (float)(r * Math.Cos(theta));
                var z = (float)(r * Math.Sin(theta));
                _vertices.Add(new Vector3(x, y, z));
                if (k < varStep)
                {
                    p1.Add(k);
                    _polygons.Add(new List<int>() { k, k + varStep });
                }
                else
                {
                    p2.Add(k);
                }

                k++;
            }
            _polygons.Add(p1);
            _polygons.Add(p2);
        }

    }
}
