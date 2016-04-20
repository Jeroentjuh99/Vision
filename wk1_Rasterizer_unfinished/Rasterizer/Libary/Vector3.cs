using System;

namespace Rasterizer.Libary
{
    // ReSharper disable once ArrangeTypeModifiers
	struct Vector3
	{
		public float[] Data;
		public float X { get { return Data[0]; } set { Data[0] = value; } }
		public float Y { get { return Data[1]; } set { Data[1] = value; } }
		public float Z { get { return Data[2]; } set { Data[2] = value; } }

		public float Length => (float)Math.Sqrt(X * X + Y * Y + Z * Z);

	    public Vector3(float x, float y, float z)
		{
			Data = new float[4];
			this.X = x;
			this.Y = y;
			this.Z = z;
            Data[3] = 1;
		}


		public Vector3 Normalize()
		{
            var v = new Vector3();
            var length = (float)Math.Sqrt((Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)));

            if(length <= 0) { throw new ArgumentNullException("Lenght <= 0"); }

            for(var i = 0; i < 3; i++)
            {
                v.Data[i] = Data[i] / length;
            }

            return v;
        }

        public static Vector3 operator *(Vector3 vec, float f)
		{
            for(var i = 0; i < 3; i++)
            {
                vec.Data[i] *= f;
            }
            return vec;
		}
		public static Vector3 operator *(float f, Vector3 vec)
		{
            for (var i = 0; i < 3; i++)
            {
                vec.Data[i] *= f;
            }
            return vec;
        }

		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
            for(var i = 0; i < 3; i++)
            {
                a.Data[i] -= b.Data[i];
            }

            return a;
		}

		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
            for (var i = 0; i < 3; i++)
            {
                a.Data[i] += b.Data[i];
            }

            return a;
        }

		public Vector3 Cross(Vector3 other)
		{
		    var a = new Vector3
		    {
		        Data =
		        {
		            [0] = (Data[1]*other.Data[2]) - (Data[2]*other.Data[1]),
		            [1] = (Data[2]*other.Data[0]) - (Data[0]*other.Data[2]),
		            [2] = (Data[0]*other.Data[1]) - (Data[1]*other.Data[0])
		        }
		    };

		    return a;
		}

		public float Dot(Vector3 other)
		{
            float a = 0;

            for (var i = 0; i < 3; i++)
            {
                a += Data[i] * other.Data[i];
            }

            return a;
		}

	}
}
