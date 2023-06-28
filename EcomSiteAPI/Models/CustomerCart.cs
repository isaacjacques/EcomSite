
namespace EcomAPI.Models
{
    public class CustomerCart
    {
        public long CartID { get; set; }
        public long CustomerID { get; set; }
        public long ProductID { get; set; }
        public int ProductQty { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
