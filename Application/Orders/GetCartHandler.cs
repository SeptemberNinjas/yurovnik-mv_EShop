using DAL;
using FluentResults;
using Core;

namespace Application.Orders
{
    public class GetCartHandler
    {
        private readonly RepositoryFactory _repositoryFactory;

        public GetCartHandler(RepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<Result<Cart>> GetCartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var repository = _repositoryFactory.CreateCartFactory();
                var cart = (await repository.GetAllAsync(cancellationToken)).FirstOrDefault();
                if (cart is null)
                {
                    return Result.Fail("Корзина не найдена");
                }
                return Result.Ok(cart);
            }
            catch (Exception ex) 
            {
                return Result.Fail("Не удалось получить корзину")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);  
            }
            
        }
    }
}
