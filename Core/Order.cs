using System.Security.Principal;
using System.Text;

namespace Core
{
    public class Order
    {
        private List<CartLine> _cartLines;

        public Guid Id { get; init; }

        public int OrderSum { get; init; }

        public OrderStatus Status { get; set; }

        public Order(List<CartLine> cartLines)
        {
            Id = Guid.NewGuid();
            _cartLines = cartLines;
            Status = OrderStatus.New;
        }

        public override string? ToString()
        {
            decimal sum = 0;
            var result = new StringBuilder($"Заказ: {Id}");
            result.AppendLine($"{Environment.NewLine}Статус заказа: {Status}");
            result.AppendLine("Состав заказ: ");
            result.AppendLine("------------------------------------------");
            foreach (var item in _cartLines)
            {
                result.AppendLine(item.ToString());
                sum += item.Price;
            }
            result.AppendLine("------------------------------------------");
            result.AppendLine($"Итого: {sum.ToString()}");

            return result.ToString();
        }
    }
}
