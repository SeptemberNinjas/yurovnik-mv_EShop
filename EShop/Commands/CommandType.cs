using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Commands
{
    public enum CommandType
    {
        DisplayPayments,
        MakePayment,
        CreatePayment,
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
