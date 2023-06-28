using EcomAPI.Dto;
using EcomAPI.Models;

namespace EcomAPI.Interfaces
{
    public interface ICustomer
    {
        public Task<IEnumerable<Customer>> GetCustomers();
        public Task<Customer> GetCustomer(long customerID);
        public Task<Customer> CreateCustomer(CustomerForCreationDto customer);
        public Task UpdateCustomer(CustomerForUpdateDto customer);
        public Task DeleteCustomer(long customerID);
    }
}
