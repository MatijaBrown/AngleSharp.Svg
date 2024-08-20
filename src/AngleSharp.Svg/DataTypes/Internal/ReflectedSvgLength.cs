using AngleSharp.Css.Parser;
using AngleSharp.Css.Values;
using AngleSharp.Css;
using AngleSharp.Dom;
using AngleSharp.Svg.Viewport;
using AngleSharp.Text;
using AngleSharp.Svg.DOM;
using AngleSharp.Css.Dom;

namespace AngleSharp.Svg.DataTypes.Internal
{
    internal class ReflectedSvgLength : SvgReflectedValue, ISvgLength
    {

        private readonly bool _isReadOnly;

        private readonly RenderMode _directionality;
        private readonly IEstablishesViewport? _viewportProvider;

        public SvgLengthType UnitType { get; private set; }

        public float Value
        {
            get => (float)StoredValue.AsPx(RenderDimensions, _directionality);
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                CssLengthValue.Unit targetUnit = ISvgLength.CssUnitMap[UnitType];
                var pixelLength = new CssLengthValue(value, CssLengthValue.Unit.Px);
                double convertedValue = pixelLength.To(targetUnit, RenderDimensions, _directionality);

                UpdateValue(new CssLengthValue(convertedValue, targetUnit));
            }
        }

        public float ValueInSpecifiedUnits
        {
            get => (float)StoredValue.Value;
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                UpdateValue(new CssLengthValue(value, ISvgLength.CssUnitMap[UnitType]));
            }
        }

        public string ValueAsString
        {
            get => StoredValue.CssText;
            set
            {
                if (_isReadOnly)
                {
                    throw new DomException(DomError.NoModificationAllowed);
                }

                UpdateValue(UnitParser.ParseLength(new StringSource(value)) ?? throw new DomException(DomError.TypeMismatch));

                string unitString = StoredValue.UnitString;
                if (!ISvgLength.UnitStringMap.TryGetValue(unitString, out SvgLengthType unit))
                {
                    UnitType = SvgLengthType.Unkown;
                }
                else
                {
                    UnitType = unit;
                }
            }
        }

        private IRenderDimensions RenderDimensions
        {
            get
            {
                IRenderDimensions renderDimensions;
                if (_viewportProvider is null)
                {
                    renderDimensions = new SvgRenderDimensions(100.0, 100.0, CssLengthValue.Medium.Value);
                }
                else
                {
                    renderDimensions = _viewportProvider.RenderDimensions;
                }
                return renderDimensions;
            }
        }

        internal ReflectedSvgLength(ISvgElement associatedElement, string reflectedAttributeName, ICssComputeContext computeContext,
            RenderMode directionality = RenderMode.Undefined, bool isReadOnly = false)
            : base(associatedElement, reflectedAttributeName, computeContext)
        {
            _isReadOnly = isReadOnly;
            _directionality = directionality;

            if (associatedElement is not IEstablishesViewport viewportProvider)
            {
                throw new ArgumentException("ReflectedSvgLength associatedElement must be of type IEstablishesViewport!");
            }
            _viewportProvider = viewportProvider;

            StoredValue = Reflect();
        }

        protected override ICssMetricValue Reflect()
        {
            var cssString = new StringSource(AssociatedElement.GetAttribute(ReflectedAttributeName) ?? throw new DomException(DomError.NotFound));

            ICssValue parsedValue = UnitParser.ParseLengthOrCalc(cssString)
                ?? throw new DomException(DomError.TypeMismatch);

            return (ICssMetricValue)parsedValue.Compute(ComputeContext);
        }

        public void NewValueSpecifiedUnits(SvgLengthType unitType, float valueInSpecifiedUnits)
        {
            if (_isReadOnly)
            {
                throw new DomException(DomError.NoModificationAllowed);
            }

            if (unitType == SvgLengthType.Unkown)
            {
                throw new DomException(DomError.NotSupported);
            }

            UpdateValue(new CssLengthValue(valueInSpecifiedUnits, ISvgLength.CssUnitMap[unitType]));
            UnitType = unitType;
        }

        public void ConvertToSpecifiedUnits(SvgLengthType unitType)
        {
            if (_isReadOnly)
            {
                throw new DomException(DomError.NoModificationAllowed);
            }

            if (unitType == SvgLengthType.Unkown)
            {
                throw new DomException(DomError.NotSupported);
            }

            CssLengthValue.Unit newCssUnit = ISvgLength.CssUnitMap[unitType];

            var pxValue = new CssLengthValue(StoredValue.AsPx(RenderDimensions, _directionality), CssLengthValue.Unit.Px);
            UpdateValue(new CssLengthValue(pxValue.To(newCssUnit, RenderDimensions, _directionality), newCssUnit));

            UnitType = unitType;
        }

    }
}
