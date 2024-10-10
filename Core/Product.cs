namespace Core
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }

        public Product(int id, string name, decimal price, int stock, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
        }

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
