using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasterizer
{
	struct Matrix
	{
		private float[,] data;

		private Matrix(int size = 4)
		{
			data = new float[size, size];
		}


		public static Matrix identity()
		{
			Matrix m = new Matrix(4);
			m.data[0,0] = 1;
			m.data[1,1] = 1;
			m.data[2,2] = 1;
			m.data[3,3] = 1;
			return m;
		}

		public static Matrix perspective(float fov, float aspect, float zNear, float zFar)
		{
            Matrix m = new Matrix(4);
            m.data[0, 0] = (float)(1 / Math.Tan(fov / 2) / aspect);
            m.data[1, 1] = (float)(1 / Math.Tan(fov / 2));
            m.data[2, 2] = (float)(zFar / (zFar - zNear));
            m.data[2, 3] = (float)((zNear * zFar) / (zFar - zNear));
            m.data[3, 2] = -1;
			return m;
		}

		public static Matrix rotation(float angle, Vector3 axis)
		{
            double c = Math.Cos(angle);
            double s = Math.Sin(angle);
            double d = 1 - c;

            Matrix m = new Matrix(4);

            m.data[0, 0] = (float)(Math.Pow(axis.x, 2) * d + c);
            m.data[0, 1] = (float)(axis.x * axis.y * d - axis.z * s);
            m.data[0, 2] = (float)(axis.x * axis.z * d + axis.y * s);

            m.data[1, 0] = (float)(axis.x * axis.y * d + axis.z * s);
            m.data[1, 1] = (float)(Math.Pow(axis.y, 2) * d + c);
            m.data[1, 2] = (float)(axis.y * axis.z * d - axis.x * s);

            m.data[2, 0] = (float)(axis.x * axis.z * d - axis.y * s);
            m.data[2, 1] = (float)(axis.y * axis.z * d + axis.x * s);
            m.data[2, 2] = (float)(Math.Pow(axis.z, 2) * d + c);

            m.data[3, 3] = 1;

            return m;
		}

		public static Matrix translate(Vector3 offset)
		{
            Matrix m = identity();
            
            for(int i = 0; i < 3; i++)
            {
                m.data[i, 3] = offset.data[i];
            }
            return m;
		}



		public static Vector3 operator * (Matrix mat, Vector3 vec)
		{
            Vector3 v = new Vector3(0, 0, 0);

                for(int i = 0; i < 3; i++)
                {
                    for(int ii = 0; ii < 4; ii++)
                    {
                        v.data[i] += mat.data[i,ii] * vec.data[ii];
                    }
                }
            return v;
		}

		public static Matrix operator * (Matrix mat1, Matrix mat2)
		{
            Matrix m = mat1;

            for(int r = 0; r < 4; r++)
            {
                for(int y = 0; y < 4; y++)
                {
                    m.data[r, y] = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        m.data[r,y] += (mat1.data[r,k] * mat2.data[k,y]);
                    }
                }
            }
            return m;
		}
	}
}
