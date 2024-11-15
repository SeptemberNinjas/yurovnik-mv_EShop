namespace Core
{
    public class CartLine
    {
        private readonly SaleItem _saleItem;

        private int _count;

        /// <summary>
        /// Идентификатор позиции в корзине
        /// </summary>
        public int ItemId => _saleItem.Id;

        /// <summary>
        /// Тип позиции в корзине
        /// </summary>
        public ItemTypes ItemType => _saleItem.ItemType;

        /// <summary>
        /// Количество
        /// </summary>
        public int Count
        {
            get => _count;
            set
            {
                if (_saleItem.OnlyOneItem || value < 1)
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
                return _saleItem.OnlyOneItem ? _saleItem.Price : _saleItem.Price * Count;
            }
        }

        public CartLine(SaleItem item, int requestedCount)
        {
            _saleItem = item;
            _count = requestedCount;
        }

        /// <summary>
        /// Текстовое представление позиции в корзине
        /// </summary>
        /// <returns></returns>
        public override string? ToString() => $"ID: {_saleItem!.Id} Наименование: {_saleItem!.Name} Количество: {Count} Сумма: {Price}";
    }
}
