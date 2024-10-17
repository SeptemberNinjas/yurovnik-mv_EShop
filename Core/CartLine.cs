namespace Core
{
    public class CartLine
    {
        private readonly Product? _product;

        private readonly Service? _service;

        private int _count;

        public int ItemId => _product?.Id ?? _service!.Id;

        public ItemTypes ItemType => _product is not null ? ItemTypes.Product : ItemTypes.Service;

        public string Text => $"{_product?.Name ?? _service!.Name} | {_count}";


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

        public decimal Price
        {
            get
            {
                return _service is not null ? _service.Price : _product!.Price;
            }
        }

        public CartLine(Product product, int requestedCount)
        {
            _product = product;
            _count = requestedCount;
        }

        public CartLine(Service service)
        {
            _service = service;
        }
    }
}
