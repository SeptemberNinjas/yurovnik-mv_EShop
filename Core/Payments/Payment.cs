namespace Core.Payments
{
    public abstract class Payment
    {
        /// <summary>
        /// Id оплаты
        /// </summary>
        public Guid OrderId { get; init; }
        /// <summary>
        /// Общая сумма оплаты
        /// </summary>
        public decimal Total {  get; init; }
        /// <summary>
        /// Количество полученных средств
        /// </summary>
        public decimal AmountReceived { get; init; }

        /// <summary>
        /// Тип оплаты
        /// </summary>
        public virtual PaymentType PaymentType { get; }

        public Payment(Guid orderId, decimal total, decimal amountReceived)
        {
            OrderId = orderId;
            Total = total;
            AmountReceived = amountReceived;
        }

        /// <summary>
        /// Валидация оплаты
        /// </summary>
        /// <param name="alert"></param>
        /// <returns></returns>
        public virtual bool IsValid(out string alert)
        {
            alert = "";            

            if (Total > AmountReceived)
            {
                alert = $"Недостаточно средств для оплаты. Необходимо: {Total}, введено: {AmountReceived}. Транзакция отменена";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Процедура оплаты
        /// </summary>
        /// <param name="message"></param>
        public virtual void Pay(out string message)
        {
            if (!IsValid(out message))
            {
                return;
            }

            message = "Заказ оплачен! Спасибо за покупку";
        }

        public override string? ToString()
        {
            return $"Оплата {(PaymentType == PaymentType.cash ? "наличными" : "безналичная")} для заказа {OrderId} на сумму {Total}, количество полученных средств {AmountReceived}";
        }
    }
}
