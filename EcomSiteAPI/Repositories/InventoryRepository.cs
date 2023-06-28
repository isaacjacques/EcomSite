using Dapper;
using EcomAPI.Models;
using EcomAPI.Dto;
using EcomAPI.Interfaces;
using System.Data;
using Microsoft.AspNetCore.Http.Connections;

namespace EcomAPI.Repositories
{
    public class InventoryRepository : IInventory
    {
        private readonly DatabaseContext _context;

        public InventoryRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Inventory>> GetInventory()
        {
            var query = "SELECT * FROM dbo.Inventory";

            using (var connection = _context.CreateConnection())
            {
                var inventory = await connection.QueryAsync<Inventory>(query);
                return inventory.ToList();
            }
        }
        public async Task<Inventory> GetInventory(long inventoryID)
        {
            var query = "SELECT * FROM dbo.Inventory WHERE InventoryID = @InventoryID"
            + " SELECT * FROM dbo.InventoryHistory WHERE InventoryID = @InventoryID";

            using (var connection = _context.CreateConnection())
            {
                var results = await connection.QueryMultipleAsync(query, new { inventoryID });
                var inventory = results.ReadFirstOrDefault<Inventory>();

                if (inventory != null)
                {
                    inventory.History = results.Read<InventoryHistory>();
                }
                return inventory;
            }
        }
        public async Task<Inventory> CreateInventory(InventoryForCreationDto inventory)
        {
            var query = "INSERT INTO dbo.Inventory (InventoryStatus, ProductID, ProductQty, LPN, CreationTime) OUTPUT (inserted.InventoryID) VALUES (@InventoryStatus, @ProductID, @ProductQty, @LPN, @CreationTime)";

            var creationTime = DateTime.Now;
            var createdInventory = new Inventory
            {
                InventoryID = 0,
                InventoryStatus = inventory.InventoryStatus,
                ProductID = inventory.ProductID,
                ProductQty = inventory.ProductQty,
                LPN = inventory.LPN,
                CreationTime = creationTime
            };

            var parameters = new DynamicParameters();
            parameters.Add("InventoryStatus", createdInventory.InventoryStatus, DbType.String);
            parameters.Add("ProductID", createdInventory.ProductID, DbType.Int64);
            parameters.Add("ProductQty", createdInventory.ProductQty, DbType.Int16);
            parameters.Add("LPN", createdInventory.LPN, DbType.String);
            parameters.Add("CreationTime", creationTime, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                createdInventory.InventoryID = await connection.QuerySingleAsync<long>(query, parameters);
                return createdInventory;
            }
        }
        public async Task UpdateInventory(InventoryForUpdateDto inventory)
        {
            var query = "UPDATE dbo.Inventory SET InventoryStatus = @InventoryStatus WHERE InventoryID = @InventoryID";

            var parameters = new DynamicParameters();
            parameters.Add("InventoryID", inventory.InventoryID, DbType.Int64);
            parameters.Add("InventoryStatus", inventory.InventoryStatus, DbType.String);
    
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteInventory(long inventoryID)
        {
            var query = "DELETE FROM dbo.Inventory WHERE InventoryID = @InventoryID";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { inventoryID });
            }
        }
    }
}
