using EcomAPI.Models;

namespace EcomAPI.Dto
{
    public class OrderForCreationDto
    {
        public string OrderStatus { get; set; }
        public long CustomerID { get; set; }
    }
}
