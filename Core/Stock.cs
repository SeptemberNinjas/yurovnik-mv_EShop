using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Stock
    {
        /// <summary>
        /// Идентфикатор
        /// </summary>
        public required int ItemId { get; init; }

        /// <summary>
        /// Остатки
        /// </summary>
        public required int Amount { get; set; }
    }
}
