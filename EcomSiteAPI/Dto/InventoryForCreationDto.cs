using EcomAPI.Models;

namespace EcomAPI.Dto
{
    public class InventoryForCreationDto
    {
        public string InventoryStatus { get; set; }
        public long ProductID { get; set; }
        public int ProductQty { get; set; }
        public string LPN { get; set; }
    }
}
