using Dapper;
using EcomAPI.Models;
using EcomAPI.Dto;
using EcomAPI.Interfaces;
using System.Data;

namespace EcomAPI.Repositories
{
    public class CustomerRepository : ICustomer
    {
        private readonly DatabaseContext _context;

        public CustomerRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var query = "SELECT * FROM dbo.Customers";

            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Customer>(query);
                return companies.ToList();
            }
        }
        public async Task<Customer> GetCustomer(long customerID)
        {
            var query = "SELECT * FROM dbo.Customers WHERE CustomerID = @CustomerID" +
                " SELECT * FROM dbo.CustomerCart WHERE CustomerID = @CustomerID";

            using (var connection = _context.CreateConnection())
            {
                var results = await connection.QueryMultipleAsync(query, new { customerID });
                var customer = results.ReadFirstOrDefault<Customer>();

                if (customer != null)
                {
                    customer.Cart = results.Read<CustomerCart>();
                }
                return customer;
            }
        }
        public async Task<Customer> CreateCustomer(CustomerForCreationDto customer)
        {
            var query = "INSERT INTO dbo.Customers (FirstName, LastName, Email, PasswordHash, PasswordSalt, CreationTime) OUTPUT (inserted.CustomerID) VALUES (@FirstName, @LastName, @Email, @PasswordHash, @PasswordSalt, @CreationTime)";

            var creationTime = DateTime.Now;
            var createdCustomer = new Customer
            {
                CustomerID = 0,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                CreationTime = creationTime
            };
            createdCustomer.SetPassword(customer.Password);

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", createdCustomer.FirstName, DbType.String);
            parameters.Add("LastName", createdCustomer.LastName, DbType.String);
            parameters.Add("Email", createdCustomer.Email, DbType.String);
            parameters.Add("PasswordHash", createdCustomer.PasswordHash, DbType.String);
            parameters.Add("PasswordSalt", createdCustomer.PasswordSalt, DbType.String);
            parameters.Add("CreationTime", creationTime, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                createdCustomer.CustomerID = await connection.QuerySingleAsync<long>(query, parameters);
                return createdCustomer;
            }
        }
        public async Task UpdateCustomer(CustomerForUpdateDto customer)
        {
            var query = "UPDATE dbo.Customers SET FirstName = @FirstName, LastName = @LastName, Email = @Email WHERE CustomerID = @CustomerID";

            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customer.CustomerID, DbType.Int64);
            parameters.Add("FirstName", customer.FirstName, DbType.String);
            parameters.Add("LastName", customer.LastName, DbType.String);
            parameters.Add("Email", customer.Email, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteCustomer(long customerID)
        {
            var query = "DELETE FROM dbo.Customers WHERE CustomerID = @CustomerID";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { customerID });
            }
        }
    }
}
