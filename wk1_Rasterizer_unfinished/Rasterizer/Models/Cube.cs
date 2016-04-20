using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasterizer.Libary;

namespace Rasterizer.Models
{
    class Cube : IFigure
    {
        private List<Vector3> _vertices = new List<Vector3>();
        private List<List<int>> _polygons = new List<List<int>>();

        public List<Vector3> VerticeList => _vertices;

        public List<List<int>> PolygonList => _polygons;

        public int VerticeNumber => VerticeNumber;


        public Cube()
        {
            //Define Cube Points
            VerticeList.Add(new Vector3(-1, -1, -1));
            VerticeList.Add(new Vector3(1, -1, -1));
            VerticeList.Add(new Vector3(1, 1, -1));
            VerticeList.Add(new Vector3(-1, 1, -1));
            VerticeList.Add(new Vector3(-1, -1, 1));
            VerticeList.Add(new Vector3(1, -1, 1));
            VerticeList.Add(new Vector3(1, 1, 1));
            VerticeList.Add(new Vector3(-1, 1, 1));

            //Define Cube Polygons
            PolygonList.Add(new List<int>() { 0, 1, 2, 3 });
            PolygonList.Add(new List<int>() { 4, 5, 6, 7 });
            PolygonList.Add(new List<int>() { 0, 1, 5, 4 });
            PolygonList.Add(new List<int>() { 2, 3, 7, 6 });
        }
    }
}
