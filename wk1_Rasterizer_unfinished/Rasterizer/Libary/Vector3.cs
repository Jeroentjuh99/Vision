using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasterizer
{
	struct Vector3
	{
		public float[] data;
		public float x { get { return data[0]; } set { data[0] = value; } }
		public float y { get { return data[1]; } set { data[1] = value; } }
		public float z { get { return data[2]; } set { data[2] = value; } }

		public float length { get { return (float)Math.Sqrt(x * x + y * y + z * z); } }

		public Vector3(float x, float y, float z)
		{
			data = new float[4];
			this.x = x;
			this.y = y;
			this.z = z;
            data[3] = 1;
		}


		public Vector3 normalize()
		{
            Vector3 v = new Vector3();
            float length = (float)Math.Sqrt((Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2)));

            if(length <= 0) { throw new ArgumentNullException("Lenght <= 0"); }

            for(int i = 0; i < 3; i++)
            {
                v.data[i] = data[i] / length;
            }

            return v;
        }

        public static Vector3 operator *(Vector3 vec, float f)
		{
            for(int i = 0; i < 3; i++)
            {
                vec.data[i] *= f;
            }
            return vec;
		}
		public static Vector3 operator *(float f, Vector3 vec)
		{
            for (int i = 0; i < 3; i++)
            {
                vec.data[i] *= f;
            }
            return vec;
        }

		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
            for(int i = 0; i < 3; i++)
            {
                a.data[i] -= b.data[i];
            }

            return a;
		}

		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
            for (int i = 0; i < 3; i++)
            {
                a.data[i] += b.data[i];
            }

            return a;
        }

		public Vector3 cross(Vector3 other)
		{
            Vector3 a = new Vector3();
            a.data[0] = (data[1] * other.data[2]) - (data[2] * other.data[1]);
            a.data[1] = (data[2] * other.data[0]) - (data[0] * other.data[2]);
            a.data[2] = (data[0] * other.data[1]) - (data[1] * other.data[0]);

            return a;
		}

		public float dot(Vector3 other)
		{
            float a = 0;

            for (int i = 0; i < 3; i++)
            {
                a += data[i] * other.data[i];
            }

            return a;
		}

	}
}
