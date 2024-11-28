using Core;
using DAL;
using FluentResults;

namespace Application.Orders
{
    public class CreateOrderHandler
    {
        private readonly RepositoryFactory _repositoryFactory;

        public CreateOrderHandler(RepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<Result> CreateOrderAsync(CancellationToken cancellationToken)
        {           
            try
            {
                var cartRepo = _repositoryFactory.CreateCartFactory();
                var cart = (await cartRepo.GetAllAsync(cancellationToken)).FirstOrDefault();
                if (cart is null)
                    return Result.Fail("Корзина не найдена");

                var order = cart.CreateOrderFromCart();
                if (order is null)
                    return Result.Fail("Не удалось создать заказ");

                var stockRepo = _repositoryFactory.CreateStockFactory();
                var orderProductWihtCount = order.Lines
                    .Where(l => l.ItemType is ItemTypes.Product)
                    .Join(await stockRepo.GetAllAsync(cancellationToken),
                        orderLine => orderLine.ItemId,
                        stock => stock.ItemId,
                        (orderLine, stock) => (stock, orderLine.Count));

                var ordersRepository = _repositoryFactory.CreateOrderFactory();
                var id = await ordersRepository.InsertAsync(order, cancellationToken);
                await cartRepo.UpdateAsync(cart, cancellationToken);
                foreach (var (stock, count) in orderProductWihtCount)
                {
                    if (stock.Amount - count < 0)
                        return Result.Fail("Недостаточно товара на остатках");

                    stock.Amount -= count;
                    await stockRepo.UpdateAsync(stock, cancellationToken);
                }

                return Result.Ok()
                    .WithSuccess($"Создан заказ {id}");
            }
            catch (Exception ex) 
            {
                return Result.Fail("Не удалось создать заказ")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);
            }
        }
    }
}
