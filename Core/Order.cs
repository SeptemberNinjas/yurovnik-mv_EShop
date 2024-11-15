using Core.Payments;
using System.Text;

namespace Core
{
    public class Order
    {
        private List<CartLine> _cartLines;

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Сумма заказа
        /// </summary>
        public decimal OrderSum { get; init; }

        public OrderStatus Status { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        /// <param name="cartLines"></param>
        public Order(List<CartLine> cartLines)
        {
            Id = Guid.NewGuid();
            _cartLines = cartLines;
            Status = OrderStatus.New;
            OrderSum = cartLines.Sum(cl => cl.Price);
        }

        /// <summary>
        /// Текстовое представление заказа
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Создает оплату из заказа
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Payment createPaymentFromOrder(decimal amount, PaymentType type)
        {
            if (type.Equals(PaymentType.cash))
            {
                return new CashPayment(Id, OrderSum, amount);
            }
            
            return new CashlessPayment(Id, OrderSum, amount);
        }
    }
}
