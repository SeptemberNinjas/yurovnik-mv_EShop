namespace Core.Payments
{
    internal class CashPayment : Payment
    {
        /// <summary>
        /// Тип
        /// </summary>
        public override PaymentType PaymentType => PaymentType.cash;

        public CashPayment(Guid orderId, decimal total, decimal amountReceived) : base(orderId, total, amountReceived) {}

        /// <summary>
        /// Процедура оплаты
        /// </summary>
        /// <param name="message"></param>
        public override void Pay(out string message)
        {
            if (!IsValid(out message))
            {
                return;
            }

            var change = AmountReceived - Total;

            message = change > 0 ? $"Заказ оплачен! Спасибо за покупку. Ваша сдача: {change}" : "Заказ оплачен! Спасибо за покупку";
        }
    }
}
