using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngleSharp.Svg.DataTypes
{
    public class SvgLength(bool isReadonly = false) : ISvgLength
    {

        public SvgLengthType UnitType => throw new NotImplementedException();

        public float Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public float ValueInSpecifiedUnits { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ValueAsString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    }
}
