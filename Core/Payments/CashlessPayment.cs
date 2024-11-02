namespace Core.Payments
{
    public class CashlessPayment : Payment
    {
        /// <summary>
        /// Тип
        /// </summary>
        public override PaymentType PaymentType => PaymentType.cashless;

        public CashlessPayment(Guid orderId, decimal total, decimal amountReceived) : base(orderId, total, amountReceived) {}

        /// <summary>
        /// Валидация оплаты
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override bool IsValid(out string message)
        {
            if (!base.IsValid(out message))
            {
                return false;
            }

            if (Total < AmountReceived)
            {
                message = $"Внесите ровно {Total}, при безналичном платеже переплата не допускается. Транзакция отменена";
                return false;
            }

            return true;
        }
        

    }
}
