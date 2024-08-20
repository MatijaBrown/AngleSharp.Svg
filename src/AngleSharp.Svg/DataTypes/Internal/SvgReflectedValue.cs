using AngleSharp.Css.Values;
using AngleSharp.Dom;
using AngleSharp.Svg.DOM;

namespace AngleSharp.Svg.DataTypes.Internal
{
    internal abstract class SvgReflectedValue : IDisposable
    {

        protected readonly ISvgElement AssociatedElement;
        protected readonly string ReflectedAttributeName;
        protected readonly ICssComputeContext ComputeContext;

        protected readonly MutationObserver AttributeObserver;

        protected ICssMetricValue StoredValue;

        protected SvgReflectedValue(ISvgElement associatedElement, string reflectedAttributeName, ICssComputeContext computeContext)
        {
            StoredValue = new CssNumberValue(0);

            AssociatedElement = associatedElement;
            ReflectedAttributeName = reflectedAttributeName;
            ComputeContext = computeContext;

            AttributeObserver = new MutationObserver((_, _) => StoredValue = Reflect());
            ConnectObserver();
        }

        protected abstract ICssMetricValue Reflect();

        protected void UpdateValue(ICssMetricValue newValue)
        {
            DisconnectObserver();
            AssociatedElement.SetAttribute(ReflectedAttributeName, newValue.CssText);
            ConnectObserver();
            StoredValue = newValue;
        }

        protected void ConnectObserver()
        {
            AttributeObserver.Connect(AssociatedElement, attributes: true, attributeFilter: [ReflectedAttributeName]);
        }

        protected void DisconnectObserver()
        {
            AttributeObserver.Disconnect();
        }

        public void Dispose()
        {
            DisconnectObserver();
        }

    }
}
