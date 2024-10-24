namespace Core
{
    public class Service : SaleItem
    {
        /// <summary>
        /// Тип элемента
        /// </summary>
        public override ItemTypes ItemType => ItemTypes.Service;

        public override bool OnlyOneItem => true;

        public Service(int id, string name, decimal price) : base(id, name, price) { }

        /// <summary>
        /// Текстовое представление услуги
        /// </summary>
        /// <returns></returns>
        public override string GetDisplayText()
        {
            return
                $"""
                ID:{Id} {Name};
                Цена:{Price};
                """;
        }
    }
}
