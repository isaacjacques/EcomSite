using Dapper;
using EcomAPI.Models;
using EcomAPI.Dto;
using EcomAPI.Interfaces;
using System.Data;

namespace EcomAPI.Repositories
{
    public class OrderRepository : IOrder
    {
        private readonly DatabaseContext _context;

        public OrderRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            var query = "SELECT * FROM dbo.Orders";

            using (var connection = _context.CreateConnection())
            {
                var orders = await connection.QueryAsync<Order>(query);
                return orders.ToList();
            }
        }
        public async Task<Order> GetOrder(long orderID)
        {
            var query = "SELECT * FROM dbo.Orders WHERE OrderID = @OrderID"
            +" SELECT * FROM dbo.OrderDetails WHERE OrderID = @OrderID";

            using (var connection = _context.CreateConnection())
            {
                var results = await connection.QueryMultipleAsync(query, new { orderID });
                var order = results.ReadFirstOrDefault<Order>();

                if (order != null)
                {
                    order.Details = results.Read<OrderDetails>();
                }
                return order;
            }
        }
        public async Task<Order> CreateOrder(OrderForCreationDto order)
        {
            var query = "INSERT INTO dbo.Orders (OrderStatus, CustomerID, CreationTime) OUTPUT (inserted.OrderID) VALUES (@OrderStatus, @CustomerID, @CreationTime)";

            var creationTime = DateTime.Now;
            var createdOrder = new Order
            {
                OrderID = 0,
                OrderStatus = order.OrderStatus,
                CustomerID = order.CustomerID,
                CreationTime = creationTime
            };

            var parameters = new DynamicParameters();
            parameters.Add("OrderStatus", createdOrder.OrderStatus, DbType.String);
            parameters.Add("CustomerID", createdOrder.CustomerID, DbType.Int64);
            parameters.Add("CreationTime", creationTime, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                createdOrder.OrderID = await connection.QuerySingleAsync<long>(query, parameters);
                return createdOrder;
            }
        }
        public async Task UpdateOrder(OrderForUpdateDto order)
        {
            var query = "UPDATE dbo.Orders SET OrderStatus = @OrderStatus WHERE OrderID = @OrderID";

            var parameters = new DynamicParameters();
            parameters.Add("OrderID", order.OrderID, DbType.Int64);
            parameters.Add("OrderStatus", order.OrderStatus, DbType.String);
            //parameters.Add("CustomerID", order.Customer.CustomerID, DbType.Int64);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteOrder(long orderID)
        {
            var query = "DELETE FROM dbo.Orders WHERE OrderID = @OrderID";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { orderID });
            }
        }
    }
}
