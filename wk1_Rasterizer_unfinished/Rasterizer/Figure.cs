using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasterizer.Libary;

namespace Rasterizer
{
    interface IFigure
    {
        List<Vector3> VerticeList { get; } 
        List<List<int>> PolygonList { get; }
        int VerticeNumber { get; }
    }
}
