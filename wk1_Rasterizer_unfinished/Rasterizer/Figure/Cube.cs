using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasterizer.Models
{
    class Cube : Figure
    {
        private List<Vector3> vertices = new List<Vector3>();
        private List<List<int>> polygons = new List<List<int>>();

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

        public Cube()
        {
            //Define Cube Points
            verticeList.Add(new Vector3(-1, -1, -1));
            verticeList.Add(new Vector3(1, -1, -1));
            verticeList.Add(new Vector3(1, 1, -1));
            verticeList.Add(new Vector3(-1, 1, -1));
            verticeList.Add(new Vector3(-1, -1, 1));
            verticeList.Add(new Vector3(1, -1, 1));
            verticeList.Add(new Vector3(1, 1, 1));
            verticeList.Add(new Vector3(-1, 1, 1));

            //Define Cube Polygons
            polygonList.Add(new List<int>() { 0, 1, 2, 3 });
            polygonList.Add(new List<int>() { 4, 5, 6, 7 });
            polygonList.Add(new List<int>() { 0, 1, 5, 4 });
            polygonList.Add(new List<int>() { 2, 3, 7, 6 });
        }
    }
}
