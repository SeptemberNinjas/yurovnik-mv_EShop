using Core.Payments;
using System.Text;

namespace Core
{
    public class Order
    {
        public IReadOnlyCollection<CartLine> Lines { get; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Сумма заказа
        /// </summary>
        public decimal OrderSum { get; init; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public OrderStatus Status { get; set; }

        public Order(IEnumerable<CartLine> lines)
        {
            Lines = lines.ToArray();
            Status = OrderStatus.New;

        }

        public Order(int id, OrderStatus orderStatus, IEnumerable<CartLine> cartLines)
        {
            Id = id;
            Lines = cartLines.ToArray();
            Status = orderStatus;
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
            foreach (var item in Lines)
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
