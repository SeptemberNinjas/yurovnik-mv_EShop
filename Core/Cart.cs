
using System.Dynamic;
using System.Text;

namespace Core
{
    public class Cart
    {
        private readonly List<CartLine> _lines = new();

        public int Count {  get => _lines.Count; }

        public string AddLine(Product product, int requestedCount)
        {
            if (requestedCount < 1)
                return "Запрашиваемое количество товара должно быть больше 0";

            if (product.Stock < requestedCount)
                return $"Нельзя добавить товар в корзину, недостаточно остатков. Имеется {product.Stock}, Требуется {requestedCount}";

            product.Stock -= requestedCount;
            if (IsLineExists(product, out var line))
                line.Count += requestedCount;
            else
                _lines.Add(new CartLine(product, requestedCount));


            return $"В корзину добавлено {requestedCount} единиц товара \'{product.Name}\'";
        }

        public string AddLine(Service service)
        {
            if (IsLineExists(service, out var line))
                return $"Ошибка при добавлении услуги. Услуга \'{service.Name}\' уже добавлена в корзину";

            _lines.Add(new CartLine(service));
            return $"В корзину добавлена услуга \'{service.Name}\'";
        }

        private bool IsLineExists(Product product, out CartLine line)
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

        private bool IsLineExists(Service service, out CartLine line)
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

        public override string ToString()
        {

            return new StringBuilder().AppendLine(string.Join(Environment.NewLine, _lines.Select(item => item.ToString()).ToArray())).AppendLine($"Итого: {GetSum().ToString()}").ToString();
        }

        private decimal GetSum()
        {
            return _lines.Aggregate(0m, (acc, item) => acc += item.Price);
        }

        public Order CreateOrderFromCart()
        {
            var newOrder = new Order(_lines.ToList());
            _lines.Clear();
            return newOrder;
        }
    }
}
