using EcomAPI.Models;

namespace EcomAPI.Dto
{
    public class InventoryForUpdateDto
    {
        public long InventoryID { get; set; }
        public string InventoryStatus { get; set; }
        public long ProductID { get; set; }
        public int ProductQty { get; set; }
        public string LPN { get; set; }
        public InventoryHistory History { get; set; }
    }
}
