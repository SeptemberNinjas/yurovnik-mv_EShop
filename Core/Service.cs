namespace Core
{
    public class Service    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Service(int id, string name, decimal price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }
        public override string? ToString()
        {
            return
                $"""
                ID:{Id} {Name};
                Описание:{Description};
                Цена:{Price};
                """;
        }
    }
}
