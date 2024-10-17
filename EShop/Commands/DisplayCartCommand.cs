using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Commands
{
    public class DisplayCartCommand
    {
        public const string Name = "DisplayCart";

        public static string GetInfo()
        {
            return "Отобразить корзину покупок";
        }

        public static string Execute(Cart cart)
        {
            return cart.ToString();
        }
    }
}
