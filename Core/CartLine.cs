namespace Core
{
    public class CartLine
    {
        /// <summary>
        /// Продажная единица
        /// </summary>
        public SaleItem SaleItem { get; }     

        private int _count;

        /// <summary>
        /// Идентификатор позиции в корзине
        /// </summary>
        public int ItemId => SaleItem.Id;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name => SaleItem.Name;

        /// <summary>
        /// Тип позиции в корзине
        /// </summary>
        public ItemTypes ItemType => SaleItem.ItemType;

        /// <summary>
        /// Количество
        /// </summary>
        public int Count
        {
            get => _count;
            set
            {
                if (SaleItem.OnlyOneItem || value < 1)
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
                return SaleItem.OnlyOneItem ? SaleItem.Price : SaleItem.Price * Count;
            }
        }

        public CartLine(SaleItem item, int requestedCount)
        {
            SaleItem = item;
            _count = requestedCount;
        }

        /// <summary>
        /// Текстовое представление позиции в корзине
        /// </summary>
        /// <returns></returns>
        public override string? ToString() => $"ID: {SaleItem!.Id} Наименование: {SaleItem!.Name} Количество: {Count} Сумма: {Price}";
    }
}
