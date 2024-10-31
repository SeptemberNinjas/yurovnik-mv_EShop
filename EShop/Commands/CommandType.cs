using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Commands
{
    public enum CommandType
    {
        DisplaySaleItems,
        DisplayProducts,
        DisplayServices,
        DisplayCart,
        AddProductToCart,
        AddServiceToCart,
        CreateOrder,
        DisplayOrders,
        GoToRoot,
        Back,
        Exit
    }
}
