using AngleSharp.Css.Values;
using AngleSharp.Css;

namespace AngleSharp.Svg.DataTypes
{
    public interface ISvgLength
    {

        private protected static readonly Dictionary<string, SvgLengthType> UnitStringMap = new()
        {
            { string.Empty, SvgLengthType.Number },
            { UnitNames.Percent, SvgLengthType.Percentage },
            { UnitNames.Em, SvgLengthType.EMs },
            { UnitNames.Ex, SvgLengthType.EXs },
            { UnitNames.Px, SvgLengthType.Px },
            { UnitNames.Cm, SvgLengthType.Cm },
            { UnitNames.Mm, SvgLengthType.Mm },
            { UnitNames.In, SvgLengthType.In },
            { UnitNames.Pt, SvgLengthType.Pt },
            { UnitNames.Pc, SvgLengthType.Pc }
        };

        private protected static readonly Dictionary<SvgLengthType, CssLengthValue.Unit> CssUnitMap = new()
        {
            { SvgLengthType.Unkown, CssLengthValue.Unit.None },
            { SvgLengthType.Number, CssLengthValue.Unit.None },
            { SvgLengthType.Percentage, CssLengthValue.Unit.Percent },
            { SvgLengthType.EMs, CssLengthValue.Unit.Em },
            { SvgLengthType.EXs, CssLengthValue.Unit.Ex },
            { SvgLengthType.Px, CssLengthValue.Unit.Px },
            { SvgLengthType.Cm, CssLengthValue.Unit.Cm },
            { SvgLengthType.Mm, CssLengthValue.Unit.Mm },
            { SvgLengthType.In, CssLengthValue.Unit.In },
            { SvgLengthType.Pt, CssLengthValue.Unit.Pt },
            { SvgLengthType.Pc, CssLengthValue.Unit.Pc }
        };

        SvgLengthType UnitType { get; }

        float Value { get; set; }

        float ValueInSpecifiedUnits { get; set; }

        string ValueAsString { get; set; }

        void NewValueSpecifiedUnits(SvgLengthType unitType, float valueInSpecifiedUnits);

        void ConvertToSpecifiedUnits(SvgLengthType unitType);

    }
}
