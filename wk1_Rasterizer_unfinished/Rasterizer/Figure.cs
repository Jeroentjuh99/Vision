﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasterizer
{
    interface Figure
    {
        List<Vector3> verticeList { get; } 
        List<List<int>> polygonList { get; }
        int verticeNumber { get; }
    }
}