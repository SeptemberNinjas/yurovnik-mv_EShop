using EShop.Commands;
using EShop.Pages;
using System.Text;

namespace Core.Payments
{
    public class DisplayPaymentsCommand : ICommandExecutable, IDisplayable
    {
        private readonly List<Payment> _paymentsList;
        /// <summary>
        /// Результат
        /// </summary>
        public string? Result { get; private set; }

        public DisplayPaymentsCommand(List<Payment> payments)
        {
            _paymentsList = payments;
        }

        /// <summary>
        /// Получить описание команды
        /// </summary>
        /// <returns></returns>
        public static string GetInfo()
        {
            return "Показать все оплаты";
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
        /// <returns></returns>
        public void Execute(string[]? args)
        {
            if (_paymentsList is null || _paymentsList.Count == 0)
            {
                Result = "Созданных оплат нет";
                return;
            }

            var sb = new StringBuilder();
            foreach (var payment in _paymentsList)
            {
                sb.AppendLine(payment.ToString());
            }
            
            Result = sb.ToString();
        }

        public async Task DisplayAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(GetInfo());
            });
        }

        public async Task ExecuteAsync(string[]? args, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (_paymentsList is null || _paymentsList.Count == 0)
                {
                    Result = "Созданных оплат нет";
                    return;
                }

                var sb = new StringBuilder();
                foreach (var payment in _paymentsList)
                {
                    sb.AppendLine(payment.ToString());
                }

                Result = sb.ToString();
            });
        }
    }
}
