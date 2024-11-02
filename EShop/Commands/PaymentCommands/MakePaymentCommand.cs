using Core.Payments;
using EShop.Pages;

namespace EShop.Commands.PaymentCommands
{
    public class MakePaymentCommand : ICommandExecutable, IDisplayable
    {
        private List<Payment> _paymentList;

        /// <summary>
        /// Результат
        /// </summary>
        public string? Result { get; private set; }

        public MakePaymentCommand(List<Payment> payments)
        {
            _paymentList = payments;
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

            if (!Guid.TryParse(args[0], out Guid orderId))
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

            //if (payment.PaymentType.Equals(PaymentType.cashless))
            //{
            //    var cashlessPayment = (CashlessPayment)payment;
            //    cashlessPayment.Pay(out string response);
            //    Result = response;
            //}
            //else 
            //{
            //    var cashPayment = (CashPayment)payment;
            //    cashPayment.Pay(out string response);
            //    Result = response;
            //}

            payment.Pay(out string response);
            Result = response;
            _paymentList.Remove(payment);
        }
    }
}
