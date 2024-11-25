using System.Text;

namespace Core
{
    public class Cart
    {
        private List<CartLine> _lines = new();

        public IReadOnlyCollection<CartLine> Lines => _lines;
        /// <summary>
        /// Количество позиций в корзине
        /// </summary>
        public int Count {  get => _lines.Count; }

        public Cart() {}

        public Cart(IEnumerable<CartLine> lines)
        {
            _lines = (List<CartLine>)lines;
        }
        /// <summary>
        /// Добавить продукт в корзину
        /// </summary>
        /// <param name="product"></param>
        /// <param name="requestedCount"></param>
        /// <returns></returns>
        public string AddProduct(Product product, int requestedCount)
        {
            if (requestedCount < 1)
                return "Запрашиваемое количество товара должно быть больше 0";

            if (product.Stock < requestedCount)
                return $"Нельзя добавить товар в корзину, недостаточно остатков. Имеется {product.Stock}, Требуется {requestedCount}";

            product.Stock -= requestedCount;
            if (TryGetLine(product, ItemTypes.Product, out var line))
                line.Count += requestedCount;
            else
                _lines.Add(new CartLine(product, requestedCount));


            return $"В корзину добавлено {requestedCount} единиц товара \'{product.Name}\'";
        }

        /// <summary>
        /// Добавить Услугу в корзину
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public string AddService(Service service)
        {
            if (TryGetLine(service, ItemTypes.Service, out var line))
                return $"Ошибка при добавлении услуги. Услуга \'{service.Name}\' уже добавлена в корзину";

            _lines.Add(new CartLine(service, 1));
            return $"В корзину добавлена услуга \'{service.Name}\'";
        }

        private bool TryGetLine(SaleItem itemLine, ItemTypes itemType, out CartLine line)
        {
            foreach (var item in _lines)
            {
                if (item.ItemType != itemType || item.ItemId != itemLine.Id)
                    continue;

                line = item;
                return true;
            }

            line = null;
            return false;
        }

        /// <summary>
        /// Получить строковое представление корзины
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var item in _lines) builder.AppendLine(item.ToString());
            builder.AppendLine($"Итого:{GetSum()}");

            return builder.ToString();
        }

        private decimal GetSum()
        {
            return _lines.Sum(item => item.Price);
        }

        /// <summary>
        /// Создать новый заказ из корзины
        /// </summary>
        /// <returns></returns>
        public Order CreateOrderFromCart()
        {
            var newOrder = new Order(_lines.ToList());
            _lines.Clear();
            return newOrder;
        }
    }
}
