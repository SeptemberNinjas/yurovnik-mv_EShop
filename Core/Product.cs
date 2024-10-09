namespace Core
{
    public class Product
    {
        public int Id { get; }

        public string Name { get; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string Description { get; set; }

        public Product(int id, string name, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }
    }
}
