namespace Core
{
    public class BasketLine
    {
        private readonly Product? _product;

        private readonly Service? _service;

        private int _count;

        public int ItemId => _product?.Id ?? _service!.Id;

        public ItemTypes ItemType => _product is not null ? ItemTypes.Product : ItemTypes.Service;

        string text => $"{_product?.Name ?? _service!.Name} | {_count}";


        public int Count
        {
            get => _count;
            set
            {
                if (_service is not null || value < 1)
                {
                    return;
                }

                _count = value;
            }
        }

        public BasketLine(Product product, int requestedCount)
        {
            _product = product;
            _count = requestedCount;
        }

        public BasketLine(Service service)
        {
            _service = service;
        }
    }
}
