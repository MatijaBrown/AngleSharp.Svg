namespace AngleSharp.Svg.DataTypes
{
    public interface ISvgLength
    {

        SvgLengthType UnitType { get; }

        float Value { get; set; }

        float ValueInSpecifiedUnits { get; set; }

        string ValueAsString { get; set; }

    }
}
