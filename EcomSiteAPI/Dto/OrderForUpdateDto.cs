using EcomAPI.Models;

namespace EcomAPI.Dto
{
    public class OrderForUpdateDto
    {
        public long OrderID { get; set; }
        public string OrderStatus { get; set; }
        public long CustomerID { get; set; }
        public IEnumerable<OrderDetails> Details { get; set; }
    }
}
