using EcomAPI.Dto;
using EcomAPI.Models;

namespace EcomAPI.Interfaces
{
    public interface IOrder
    {
        public Task<IEnumerable<Order>> GetOrders();
        public Task<Order> GetOrder(long orderID);
        public Task<Order> CreateOrder(OrderForCreationDto order);
        public Task UpdateOrder(OrderForUpdateDto order);
        public Task DeleteOrder(long orderID);
    }
}
