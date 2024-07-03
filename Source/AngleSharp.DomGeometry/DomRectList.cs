namespace AngleSharp.DomGeometry
{
    public class DomRectList
    {

        private readonly List<DomRect> _list = [];

        internal DomRectList(params DomRect[] rects)
        {
            _list.AddRange(rects);
        }

        public int Length => _list.Count;

        public DomRect? this[int index]
        {
            get
            {
                if (index >= Length)
                {
                    return null;
                }
                return _list[index];
            }
        }

    }
}
