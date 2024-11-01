namespace Core.Payment
{
    public class CashlessPayment : Payment, IPayment
    {
        public CashlessPayment(Order order) : base(order)
        {

        }

        public void Pay()
        {
            throw new NotImplementedException();
        }
    }
}