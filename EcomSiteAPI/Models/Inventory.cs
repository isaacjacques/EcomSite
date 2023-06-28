using System.Security.Cryptography;
using System.Text;

namespace EcomAPI.Models
{
    public class Inventory
    {
        public long InventoryID { get; set; }
        public string InventoryStatus { get; set; }
        public long ProductID { get; set; }
        public int ProductQty { get; set; }
        public string LPN { get; set; }
        public IEnumerable<InventoryHistory> History { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
