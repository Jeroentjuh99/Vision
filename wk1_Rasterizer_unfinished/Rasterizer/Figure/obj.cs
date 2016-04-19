using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Rasterizer.Models
{
    class obj : Rasterizer.Figure
    {
        private List<Vector3> vertices = new List<Vector3>();
        private List<List<int>> polygons = new List<List<int>>();
        private string name;

        public List<List<int>> polygonList
        {
            get { return polygons; }
        }

        public List<Vector3> verticeList
        {
            get { return vertices; }
        }

        public int verticeNumber
        {
            get { return verticeNumber; }
        }

        public obj()
        {
            //open file
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Object Files|*.obj";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;

            if ((int) dialog.ShowDialog() == 1)
            {
                name = Path.GetFileNameWithoutExtension(dialog.FileName);
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
                            vertices.Add(new Vector3(float.Parse(temparray[1].Replace('.', ',')), float.Parse(temparray[2].Replace('.', ',')), float.Parse(temparray[3].Replace('.', ','))));
                        } else if (temp.StartsWith("f "))
                        {
                            temparray = temp.Split(' ');
                            polygons.Add(new List<int>() {int.Parse(temparray[1].Remove(temparray[1].IndexOf("/"))) % vertices.Count, int.Parse(temparray[2].Remove(temparray[2].IndexOf("/"))) % vertices.Count, int.Parse(temparray[3].Remove(temparray[3].IndexOf("/"))) % vertices.Count });
                        }
                    }
                }
                fileStream.Close();
            }

        }
    }
}
