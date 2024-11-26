using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SaleItem
{
    public record SaleItemDto(ItemTypes ItemType, int Id, string Name, decimal Price, decimal? Stock = null);
}
