using System.Security.Cryptography;
using System.Text;

namespace EcomAPI.Models
{
    public class InventoryHistory
    {
        public long InventoryHistoryID { get; set; }
        public long InventoryID { get; set; }
        public long OrderID { get; set; }
        public int OrderQty { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
