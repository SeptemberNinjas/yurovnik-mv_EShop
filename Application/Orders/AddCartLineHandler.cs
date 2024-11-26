using DAL;
using FluentResults;
using Core;

namespace Application.Orders
{
    public class AddCartLineHandler
    {
        private readonly RepositoryFactory _repositoryFactory;

        public AddCartLineHandler(RepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<Result> AddLineAsync(int itemId, int count, CancellationToken cancellationToken)
        {
            try
            {
                var cartRepo = _repositoryFactory.CreateCartFactory();
                var cart = (await cartRepo.GetAllAsync(cancellationToken)).FirstOrDefault();
                if (cart == null)
                {
                    await cartRepo.InsertAsync(new Cart(), cancellationToken);
                    cart = (await cartRepo.GetAllAsync(cancellationToken)).FirstOrDefault();
                }
                if (cart is null)
                    return Result.Fail("Корзина не найдена");

                var saleItemRepo = _repositoryFactory.CreateSaleItemFactory();
                var item = await saleItemRepo.GetByIdAsync(itemId, cancellationToken);
                if (item is null)
                    return Result.Fail($"{itemId} товар не найден");

                var result = item switch
                {
                    Product product => await AddProductLineAsync(product, count, cart, cartRepo, cancellationToken),
                    Service service => AddServiceLine(service, cart),
                    _ => Result.Fail("Неизвестный тип товарной единицы")
                };

                if (result.IsSuccess)
                {
                    await cartRepo.UpdateAsync(cart, cancellationToken);
                }

                return result.ToResult();
            }
            catch (Exception ex) 
            {
                return Result.Fail("Не удалось получить корзину")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);
            }
        }

        public async Task<Result<Cart>> AddProductLineAsync(Product product, int requiredCount, Cart cart, IRepository<Cart> cartRepo, CancellationToken cancellationToken)
        {
            if (requiredCount < 1)
                return Result.Fail("Запрашиваемое количество товара должно быть больше 0");

            // Вычисляем доступные остатки с учетом всех корзин
            var productsInCarts = (await cartRepo.GetAllAsync())
                .SelectMany(b => b.Lines)
                .Where(p => p.ItemType is ItemTypes.Product && p.ItemId == product.Id)
                .Sum(p => p.Count);
            var remainsWithCarts = product.Stock - productsInCarts;

            if (remainsWithCarts < requiredCount)
                return Result.Fail($"Нельзя добавить товар в корзину, недостаточно остатков.{Environment.NewLine}" +
                       $"Имеется {product.Stock} из них в корзине {productsInCarts}, требуется {requiredCount}");


            if (IsLineExists(product, cart.Lines, out var line))
                line.Count += requiredCount;
            else
                cart.AddProduct(product, requiredCount);

            return Result.Ok(cart)
                .WithSuccess($"В корзину добавлено {requiredCount} единиц товара \'{product.Name}\'");
        }

        public Result<Cart> AddServiceLine(Service service, Cart cart)
        {
            if (IsLineExists(service, cart.Lines, out _) && service.OnlyOneItem)
                return Result.Fail($"Ошибка при добавлении услуги. Услуга \'{service.Name}\' уже добавлена в корзину");

            cart.AddService(service);

            return Result.Ok(cart)
                .WithSuccess($"В корзину добавлена услуга \'{service.Name}\'");
        }

        public bool IsLineExists(Core.SaleItem saleItem, IEnumerable<CartLine> lines, out CartLine line)
        {
            foreach (var ln in lines)
            {
                if (ln.ItemType != saleItem.ItemType || ln.ItemId != saleItem.Id)
                    continue;
                line = ln;
                return true;
            }

            line = null!;
            return false;
        }
    }
}
