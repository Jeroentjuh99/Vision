using System;

namespace Rasterizer.Libary
{
    // ReSharper disable once ArrangeTypeModifiers
	struct Matrix
	{
	    // ReSharper disable once FieldCanBeMadeReadOnly.Local
		private float[,] _data;

		private Matrix(int size = 4)
		{
			_data = new float[size, size];
		}


		public static Matrix Identity()
		{
		    var m = new Matrix(4)
		    {
		        _data =
		        {
		            [0, 0] = 1,
		            [1, 1] = 1,
		            [2, 2] = 1,
		            [3, 3] = 1
		        }
		    };
		    return m;
		}

		public static Matrix Perspective(float fov, float aspect, float zNear, float zFar)
		{
		    var m = new Matrix(4)
		    {
		        _data =
		        {
		            [0, 0] = (float) (1/Math.Tan(fov/2)/aspect),
		            [1, 1] = (float) (1/Math.Tan(fov/2)),
		            [2, 2] = (float) (zFar/(zFar - zNear)),
		            [2, 3] = (float) ((zNear*zFar)/(zFar - zNear)),
		            [3, 2] = -1
		        }
		    };
		    return m;
		}

		public static Matrix Rotation(float angle, Vector3 axis)
		{
		    var c = Math.Cos(angle);
            var s = Math.Sin(angle);
            var d = 1 - c;

		    var m = new Matrix(4)
		    {
		        _data =
		        {
		            [0, 0] = (float) (Math.Pow(axis.X, 2)*d + c),
		            [0, 1] = (float) (axis.X*axis.Y*d - axis.Z*s),
		            [0, 2] = (float) (axis.X*axis.Z*d + axis.Y*s),
		            [1, 0] = (float) (axis.X*axis.Y*d + axis.Z*s),
		            [1, 1] = (float) (Math.Pow(axis.Y, 2)*d + c),
		            [1, 2] = (float) (axis.Y*axis.Z*d - axis.X*s),
		            [2, 0] = (float) (axis.X*axis.Z*d - axis.Y*s),
		            [2, 1] = (float) (axis.Y*axis.Z*d + axis.X*s),
		            [2, 2] = (float) (Math.Pow(axis.Z, 2)*d + c),
		            [3, 3] = 1
		        }
		    };

		    return m;
		}

		public static Matrix Translate(Vector3 offset)
		{
            var m = Identity();
            
            for(var i = 0; i < 3; i++)
            {
                m._data[i, 3] = offset.Data[i];
            }
            return m;
		}



		public static Vector3 operator * (Matrix mat, Vector3 vec)
		{
            var v = new Vector3(0, 0, 0);

                for(var i = 0; i < 3; i++)
                {
                    for(var ii = 0; ii < 4; ii++)
                    {
                        v.Data[i] += mat._data[i,ii] * vec.Data[ii];
                    }
                }
            return v;
		}

		public static Matrix operator * (Matrix mat1, Matrix mat2)
		{
            var m = mat1;

            for(var r = 0; r < 4; r++)
            {
                for(var y = 0; y < 4; y++)
                {
                    m._data[r, y] = 0;
                    for (var k = 0; k < 4; k++)
                    {
                        m._data[r,y] += (mat1._data[r,k] * mat2._data[k,y]);
                    }
                }
            }
            return m;
		}
	}
}
