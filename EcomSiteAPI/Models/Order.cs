using System.Security.Cryptography;
using System.Text;

namespace EcomAPI.Models
{
    public class Order
    {
        public long OrderID { get; set; }
        public string OrderStatus { get; set; }
        public long CustomerID { get; set; }
        public IEnumerable<OrderDetails> Details { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
