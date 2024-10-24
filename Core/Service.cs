namespace Core
{
    public class Service    {

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
        /// Описание
        /// </summary>
        public string Description { get; set; }

        public Service(int id, string name, decimal price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }

        /// <summary>
        /// Текстовое представление услуги
        /// </summary>
        /// <returns></returns>
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
