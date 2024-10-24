
using System.Dynamic;
using System.Text;

namespace Core
{
    public class Cart
    {
        private readonly List<CartLine> _lines = new();

        /// <summary>
        /// Количество позиций в корзине
        /// </summary>
        public int Count {  get => _lines.Count; }

        /// <summary>
        /// Добавить продукт в корзину
        /// </summary>
        /// <param name="product"></param>
        /// <param name="requestedCount"></param>
        /// <returns></returns>
        public string AddLine(Product product, int requestedCount)
        {
            if (requestedCount < 1)
                return "Запрашиваемое количество товара должно быть больше 0";

            if (product.Stock < requestedCount)
                return $"Нельзя добавить товар в корзину, недостаточно остатков. Имеется {product.Stock}, Требуется {requestedCount}";

            product.Stock -= requestedCount;
            if (TryGetLine(product, out var line))
                line.Count += requestedCount;
            else
                _lines.Add(new CartLine(product, requestedCount));


            return $"В корзину добавлено {requestedCount} единиц товара \'{product.Name}\'";
        }

        /// <summary>
        /// Добавить услугу в корзину
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public string AddLine(Service service)
        {
            if (TryGetLine(service, out var line))
                return $"Ошибка при добавлении услуги. Услуга \'{service.Name}\' уже добавлена в корзину";

            _lines.Add(new CartLine(service));
            return $"В корзину добавлена услуга \'{service.Name}\'";
        }

        private bool TryGetLine(Product product, out CartLine line)
        {
            foreach (var item in _lines)
            {
                if (item.ItemType != ItemTypes.Product || item.ItemId != product.Id)
                    continue;

                line = item;
                return true;
            }

            line = null;
            return false;
        }

        private bool TryGetLine(Service service, out CartLine line)
        {
            foreach (var item in _lines)
            {
                if (item.ItemType != ItemTypes.Service || item.ItemId != service.Id)
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

        public Order CreateOrderFromCart()
        {
            var newOrder = new Order(_lines.ToList());
            _lines.Clear();
            return newOrder;
        }
    }
}
