namespace Core
{
    public class Product
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Остатки
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        public Product(int id, string name, decimal price, int stock, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
        }

        /// <summary>
        /// Текстовое представление товара
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            return 
                $"""
                ID:{Id} {Name};
                Описание:{Description};
                Цена:{Price} Остаток:{(Stock <= 0 ? "Нет на складе" : Stock)};
                """;
        }
    }
}
