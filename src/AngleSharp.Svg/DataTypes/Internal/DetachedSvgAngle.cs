using AngleSharp.Css.Values;
using AngleSharp.Dom;
using AngleSharp.Css.Parser;
using AngleSharp.Text;

namespace AngleSharp.Svg.DataTypes
{
    internal sealed class DetachedSvgAngle : ISvgAngle
    {

        private readonly bool _isReadOnly;

        private CssAngleValue _storedValue;

        public SvgAngleType UnitType { get; private set; }

        public float Value
        {
            get => (float)_storedValue.ToDegree();
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                var degValue = new CssAngleValue(value, CssAngleValue.Unit.Deg);
                double convertedValue = ISvgAngle.DegConversionMap[UnitType](degValue.Value);
                CssAngleValue.Unit targetUnit = ISvgAngle.CssUnitMap[UnitType];

                _storedValue = new CssAngleValue(convertedValue, targetUnit);
            }
        }

        public float ValueInSpecifiedUnits
        {
            get => (float)_storedValue.Value;
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                _storedValue = new CssAngleValue(value, _storedValue.Type);
            }
        }

        public string ValueAsString
        {
            get => _storedValue.CssText;
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                _storedValue = UnitParser.ParseAngle(new StringSource(value)) ?? throw new DomException(DomError.NotSupported);
            }
        }

        internal DetachedSvgAngle(float initialValue = default, SvgAngleType unitType = SvgAngleType.Unkown, bool isReadOnly = false)
        {
            _isReadOnly = isReadOnly;
            _storedValue = new CssAngleValue(initialValue, ISvgAngle.CssUnitMap[unitType]);
            UnitType = unitType;
        }

        public void NewValueSpecifiedUnits(SvgAngleType unitType, float valueInSpecifiedUnits)
        {
            if (_isReadOnly)
            {
                throw new DomException(DomError.NoModificationAllowed);
            }

            _storedValue = new CssAngleValue(valueInSpecifiedUnits, ISvgAngle.CssUnitMap[unitType]);
            UnitType = unitType;
        }

        public void ConvertToSpecifiedUnits(SvgAngleType unitType)
        {
            if (_isReadOnly)
            {
                throw new DomException(DomError.NoModificationAllowed);
            }

            if (unitType == SvgAngleType.Unkown)
            {
                throw new DomException(DomError.NotSupported);
            }

            double degrees = Value;

            _storedValue = unitType switch
            {
                SvgAngleType.Unspecified or SvgAngleType.Deg => new CssAngleValue(degrees, CssAngleValue.Unit.Deg),
                SvgAngleType.Rad => new CssAngleValue(double.DegreesToRadians(degrees), CssAngleValue.Unit.Rad),
                SvgAngleType.Grad => new CssAngleValue(degrees / 90.0 * 100.0, CssAngleValue.Unit.Grad),
                _ => throw new DomException(DomError.NotSupported),
            };

            UnitType = unitType;
        }

    }
}
