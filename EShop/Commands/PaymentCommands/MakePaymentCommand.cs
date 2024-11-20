using Core;
using Core.Payments;
using EShop.Pages;

namespace EShop.Commands.PaymentCommands
{
    public class MakePaymentCommand : ICommandExecutable, IDisplayable
    {
        private List<Payment> _paymentList;
        private List<Order> _orders;

        /// <summary>
        /// Результат
        /// </summary>
        public string? Result { get; private set; }

        public MakePaymentCommand(List<Payment> payments, List<Order> orders)
        {
            _paymentList = payments;
            _orders = orders;
        }

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Провести оплату";
        }

        /// <summary>
        /// Вывести на экран
        /// </summary>
        public void Display()
        {
            Console.WriteLine(GetInfo());
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="args"></param>
        public void Execute(string[]? args)
        {
            if (args is null || args.Length == 0)
            {
                Result = "Не передано ни одно аргумента";
                return;
            }

            if (!int.TryParse(args[0], out int orderId))
            {
                Result = "Некорректно указан id заказа";
                return;
            }

            var payment = _paymentList.FirstOrDefault(p => p.OrderId == orderId);
            if (payment is null)
            {
                Result = "Оплата не найдена";
                return;
            }

            payment.Pay(out string response);
            Result = response;
            _paymentList.Remove(payment);
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order is null)
            {
                Result = "Не удалось найти заказ";
                return;
            }
            order.Status = OrderStatus.Paid;
        }
    }
}
