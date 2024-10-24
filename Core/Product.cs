namespace Core
{
    public class Product : SaleItem
    {
        /// <summary>
        /// Остатки
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Тип элемента
        /// </summary>
        public override ItemTypes ItemType => ItemTypes.Product;

        public Product(int id, string name, decimal price, int stock) : base(id, name, price)
        {
            Stock = stock;
        }

        /// <summary>
        /// Текстовое представление товара
        /// </summary>
        /// <returns></returns>
        public override string GetDisplayText()
        {
            return
                $"""
                ID:{Id} {Name};
                Цена:{Price} Остаток:{(Stock <= 0 ? "Нет на складе" : Stock)};
                """;
        }
    }
}
