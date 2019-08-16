using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WhatsNewAttributes;

[assembly:SupportsWhatsNew]
namespace VectorClass
{
    [LastModified("2017-7-19", "更新C#7，.NET Core 2")]
    [LastModified("2015-6-6", "更新C#6,.NET Core")]
    [LastModified("2010-2-14", "修改第一步")]
    public class Vector : IFormattable, IEnumerable<double>
    {
        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector(Vector vector) : this(vector.X, vector.Y, vector.Z)
        {

        }
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public IEnumerator<double> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        [LastModified("2017-7-19", "将ijk格式从StringBuilder更改为格式字符串")]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return ToString();
            }

            switch (format.ToUpper())
            {
                case "N":
                    return "|| " + Norm().ToString() + " ||";
                case "VE":
                    return $"( {X:E}, {Y:E}, {Z:E} )";
                case "IJK":
                    return $"{X} i + {Y} j + {Z} k";
                default:
                    return ToString();
            }
        }
        

        public double Norm() => X * X + Y * Y + Z * Z;

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        [LastModified("2015-6-6", "修改")]
        [LastModified("2010-2-14", " 类创建")]
        public class VectorEnumerator : IEnumerator<double>
        {
            public double Current => throw new NotImplementedException();

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
