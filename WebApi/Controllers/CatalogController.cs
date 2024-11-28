using Application.SaleItem;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly GetSaleItemHandler _saleItemHandler;

        public CatalogController(GetSaleItemHandler getSaleItemHandler)
        {
            _saleItemHandler = getSaleItemHandler;
        }

        /// <summary>
        /// Получить список продуктов
        /// </summary>
        /// <param name="count"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<SaleItemDto>>> GetProductsAsync([FromQuery] int? count, CancellationToken cancellationToken)
        {
            var result = await _saleItemHandler.GetItemsAsync(ItemTypes.Product, count, cancellationToken);
            if (result.IsFailed)
                return BadRequest(result.ToString());

            if (!result.Value.Any()) 
                return NotFound();
                    
            return Ok(result.Value);
        }

        [HttpGet("services")]
        public async Task<ActionResult<IEnumerable<SaleItemDto>>> GetServicesAsync([FromQuery] int? count, CancellationToken cancellationToken)
        {
            var result = await _saleItemHandler.GetItemsAsync(ItemTypes.Service, count, cancellationToken);
            if (result.IsFailed)
                return BadRequest(result.ToString());

            if (!result.Value.Any())
                return NotFound();

            return Ok(result.Value);
        }
    }
}
