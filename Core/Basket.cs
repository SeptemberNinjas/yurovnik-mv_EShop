
namespace Core
{
    public class Basket
    {
        private readonly List<BasketLine> _lines = new();

        public string AddLine(Product product, int requestedCount)
        {
            if (requestedCount < 1)
            {
                return "Запрашиваемое количество товара должно быть больше 0";
            }

            if (product.Stock < requestedCount)
            {
                return $"Нельзя добавить товар в корзину, недостаточно остатков. Имеется {product.Stock}, Требуется {requestedCount}";

            }

            product.Stock -= requestedCount;
            if(IsLineExists(product, out var line))
            {
                line.Count += requestedCount;
            } else
            {
                _lines.Add(new BasketLine(product, requestedCount));
            }

            return $"В корзину добавлено {requestedCount} единиц товара \'{product.Name}\'";
        }

        public string AddLine(Service service)
        {
            if (IsLineExists(service, out var line))
                return $"Ошибка при добавлении услуги. Услуга \'{service.Name}\' уже добавлена в корзину";

            _lines.Add(new BasketLine(service));
            return $"В корзину добавлена услуга \'{service.Name}\'";
        }

        private bool IsLineExists(Product product, out BasketLine line)
        {
            foreach (var item in _lines)
            {
                if (item.ItemType != ItemTypes.Product || item.ItemId != product.Id)
                {
                    continue;
                }
                line = item;
                return true;
            }

            line = null;
            return false;
        }

        private bool IsLineExists(Service service, out BasketLine line)
        {
            foreach (var item in _lines)
            {
                if (item.ItemType != ItemTypes.Service || item.ItemId != service.Id)
                {
                    continue;
                }
                line = item;
                return true;
            }

            line = null;
            return false;
        }
    }
}
