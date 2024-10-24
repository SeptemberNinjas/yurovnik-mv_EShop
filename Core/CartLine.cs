namespace Core
{
    public class CartLine
    {
        private readonly Product? _product;

        private readonly Service? _service;

        private int _count;

        /// <summary>
        /// Идентификатор позиции в корзине
        /// </summary>
        public int ItemId => _product?.Id ?? _service!.Id;

        /// <summary>
        /// Тип позиции в корзине
        /// </summary>
        public ItemTypes ItemType => _product is not null ? ItemTypes.Product : ItemTypes.Service;

        /// <summary>
        /// Количество
        /// </summary>
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

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price
        {
            get
            {
                return _service is not null ? _service.Price : _product!.Price * Count;
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
            _count = 1;
        }

        /// <summary>
        /// Текстовое представление позиции в корзине
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            if (_service == null)
            {
                return $"ID: {_product!.Id} Наименование: {_product!.Name} Количество: {Count} Сумма: {Price}";
            }

            return $"ID: {_service!.Id} Наименование: {_service!.Name} Сумма: {Price}";
        }
    }
}
