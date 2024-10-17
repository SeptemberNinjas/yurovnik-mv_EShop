using System.Security.Principal;
using System.Text;

namespace Core
{
    public class Order
    {
        public int Id { get; init; }

        public int OrderSum { get; init; }

        public bool IsPaid { get; set; }

        public Product[]? ProductList { get; set; }

        public Service[]? ServiceList { get; set; }

        public Order(Cart cart)
        {            
        }


        public override string? ToString()
        {
            var sb = new StringBuilder();

            if (ProductList != null)
                sb.AppendLine(string.Join("\n", ProductList!.Select(item => item.ToString()).ToArray()));

            if (ServiceList != null)
                sb.AppendLine(string.Join("\n", ServiceList.Select(item => item.ToString()).ToArray()));

            return sb.ToString();
        }
    }
}
