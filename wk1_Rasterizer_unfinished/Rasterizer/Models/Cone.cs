using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasterizer.Libary;

namespace Rasterizer.Models
{
    class Cone : IFigure
    {
        private List<Vector3> _vertices = new List<Vector3>();
        private List<List<int>> _polygons = new List<List<int>>();
        private int _verticeNr = 20;

        public List<Vector3> VerticeList => _vertices;

        public List<List<int>> PolygonList => _polygons;

        public int VerticeNumber => _verticeNr;

        public Cone()
        {
            //Define Points
            ReplaceVerticeNumber(_verticeNr);
        }

        public Cone(int i)
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
            var p = new List<int>();

            _vertices.Add(new Vector3(0, -2, 0));

            for (var theta = 0.0; theta < 2 * Math.PI; theta += step)
            {
                k++;
                var x = (float)(r * Math.Cos(theta));
                var z = (float)(r * Math.Sin(theta));
                _vertices.Add(new Vector3(x, y, z));
                _polygons.Add(new List<int>() { 0, _vertices.Count - 1 });
                p.Add(k);
            }
            _polygons.Add(p);
        }

    }
}
