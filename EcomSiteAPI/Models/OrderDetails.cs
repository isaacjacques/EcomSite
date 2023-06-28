
namespace EcomAPI.Models
{
    public class OrderDetails
    {
        public long DetailID { get; set; }
        public long OrderID { get; set; }
        public long ProductID { get; set; }
        public int ProductQty { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
