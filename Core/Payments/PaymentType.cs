using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Payments
{
    public enum PaymentType
    {
        /// <summary>
        /// Наличный расчет
        /// </summary>
        cash,
        /// <summary>
        /// Безналичный расчет
        /// </summary>
        cashless
    }
}
