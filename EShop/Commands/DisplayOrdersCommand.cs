using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Commands
{
    public class DisplayOrdersCommand
    {
        private readonly List<Order> _orders;
        public const string Name = "DisplayOrders";

        public static string GetInfo()
        {
            return "Показать все заказы";
        }
        public DisplayOrdersCommand(List<Order> orders)
        {
            _orders = orders;
        }

        public string Execute()
        {
            if (_orders is null || _orders.Count == 0)
            {
                return "Заказов пока нет:(";
            }

            return string.Join(Environment.NewLine, _orders.Select(item => item.ToString()).ToArray());
        }
    }
}
