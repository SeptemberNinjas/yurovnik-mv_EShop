using Core;
using DAL;
using FluentResults;

namespace Application.SaleItem
{
    public class GetSaleItemHandler
    {
        private readonly RepositoryFactory _repositoryFactory;

        public GetSaleItemHandler(RepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<Result<IEnumerable<SaleItemDto>>> GetItemsAsync(ItemTypes itemTypes, int? count, CancellationToken cancellationToken)
        {
            var repository = _repositoryFactory.CreateSaleItemFactory();

            try
            {
                var items = (await repository.GetAllAsync()).Where(i => i.ItemType == itemTypes);
                var requestedItems = count is null or <= 0 ? items : items.Take(count.Value);

                return Result.Ok(requestedItems
                    .Select(i => new SaleItemDto(i.ItemType, i.Id, i.Name, i.Price, (i as Product)?.Stock)));
            }
            catch (Exception ex) 
            { 
                return Result.Fail("Не удалось получить коллекцию торговых единиц")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);
            }
        }
    }
}
