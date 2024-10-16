using System.Security.Principal;

namespace Core
{
    public class Order
    {
        public int Id { get; init; }

        public bool IsPaid { get; set; }

        public Product[]? ProductList {  get; set; } 

        public Service[]? ServiceList { get; set; }

        public Order(Product[] products)
        {
            ProductList = products;
        }

        public Order(Service[] services)
        {
            ServiceList = services;
        }

        public override string? ToString()
        {
            if (ProductList == null)
            {
                return string.Join("\n", ServiceList!.Select(item => item.ToString()).ToArray());
            }
            else
            {
                return string.Join("\n", ProductList.Select(item => item.ToString()).ToArray());
            }
        }
    }
}
