using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Rasterizer.Libary;

namespace Rasterizer.Models
{
    class Obj : Rasterizer.IFigure
    {
        private List<Vector3> _vertices = new List<Vector3>();
        private List<List<int>> _polygons = new List<List<int>>();
        private string _name;

        public List<List<int>> PolygonList => _polygons;

        public List<Vector3> VerticeList => _vertices;

        public int VerticeNumber => VerticeNumber;

        public Obj()
        {
            //open file
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Object Files|*.obj";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;

            if ((int) dialog.ShowDialog() == 1)
            {
                _name = Path.GetFileNameWithoutExtension(dialog.FileName);
                Stream fileStream = dialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string temp;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        string[] temparray;
                        if (temp.StartsWith("v "))
                        {
                            temparray = temp.Split(' ');
                            _vertices.Add(new Vector3(float.Parse(temparray[1].Replace('.', ',')), float.Parse(temparray[2].Replace('.', ',')), float.Parse(temparray[3].Replace('.', ','))));
                        } else if (temp.StartsWith("f "))
                        {
                            temparray = temp.Split(' ');
                            _polygons.Add(new List<int>() {int.Parse(temparray[1].Remove(temparray[1].IndexOf("/"))) % _vertices.Count, int.Parse(temparray[2].Remove(temparray[2].IndexOf("/"))) % _vertices.Count, int.Parse(temparray[3].Remove(temparray[3].IndexOf("/"))) % _vertices.Count });
                        }
                    }
                }
                fileStream.Close();
            }

        }
    }
}
