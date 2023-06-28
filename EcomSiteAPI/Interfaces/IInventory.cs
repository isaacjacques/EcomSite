using EcomAPI.Dto;
using EcomAPI.Models;

namespace EcomAPI.Interfaces
{
    public interface IInventory
    {
        public Task<IEnumerable<Inventory>> GetInventory();
        public Task<Inventory> GetInventory(long inventoryID);
        public Task<Inventory> CreateInventory(InventoryForCreationDto inventory);
        public Task UpdateInventory(InventoryForUpdateDto inventory);
        public Task DeleteInventory(long inventoryID);
    }
}
