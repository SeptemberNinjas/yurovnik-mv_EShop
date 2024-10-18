﻿using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Commands
{
    public class DisplayCartCommand
    {
        private Cart _cart;
        public const string Name = "DisplayCart";

        public static string GetInfo()
        {
            return "Отобразить корзину покупок";
        }

        public DisplayCartCommand(Cart cart)
        {
            _cart = cart;
        }

        public string Execute()
        {
            return _cart.ToString();
        }
    }
}