﻿namespace Core
{
    public class Service    {
        public int Id { get; }
        public string Name { get; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Service(int id, string name, decimal price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }
    }
}
