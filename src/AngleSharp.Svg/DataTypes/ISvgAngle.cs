using AngleSharp.Css.Values;

namespace AngleSharp.Svg.DataTypes
{
    public interface ISvgAngle
    {

        private protected static readonly Dictionary<SvgAngleType, Func<double, double>> DegConversionMap = new()
        {
            { SvgAngleType.Unkown, v => v },
            { SvgAngleType.Unspecified, v => v },
            { SvgAngleType.Deg, v => v },
            { SvgAngleType.Rad, double.DegreesToRadians },
            { SvgAngleType.Grad, v => v / 90.0 * 100.0 }
        };

        private protected static readonly Dictionary<SvgAngleType, CssAngleValue.Unit> CssUnitMap = new()
        {
            { SvgAngleType.Unkown, CssAngleValue.Unit.None },
            { SvgAngleType.Unspecified, CssAngleValue.Unit.Deg },
            { SvgAngleType.Deg, CssAngleValue.Unit.Deg },
            { SvgAngleType.Rad, CssAngleValue.Unit.Rad },
            { SvgAngleType.Grad, CssAngleValue.Unit.Grad }
        };

        SvgAngleType UnitType { get; }

        float Value { get; set; }

        float ValueInSpecifiedUnits { get; set; }

        string ValueAsString { get; set; }

        void NewValueSpecifiedUnits(SvgAngleType unitType, float valueInSpecifiedUnits);

        void ConvertToSpecifiedUnits(SvgAngleType unitType);

    }
}
