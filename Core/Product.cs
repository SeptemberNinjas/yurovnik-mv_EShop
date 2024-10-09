namespace Core
{
    public class Product
    {
        public int Id;

        public string Name;

        public decimal Price;

        public int Stock;

        public Product(int id, string name, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }
    }
}
