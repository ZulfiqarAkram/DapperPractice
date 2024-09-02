using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace DapperPractice.Services
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }
    }
    public class CustomerService
    {
        private readonly IConfiguration _config;
        public CustomerService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("AdventureWorks")))
            {
                await connection.OpenAsync();
                var customers = await connection.QueryAsync<Customer>("SELECT * FROM SalesLT.Customer");
                return customers;
            }

        }

        public async Task DeleteCustomerByID(int customerID)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("AdventureWorks")))
            {
                await connection.OpenAsync();
                await connection.QueryAsync("Delete FROM SalesLT.Customer where CustomerID = @CustomerID", new { CustomerID = customerID });
            }
        }

        public async Task<Customer> GetCustomerByID(int customerID)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("AdventureWorks")))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM SalesLT.Customer where CustomerID = @CustomerID";
                return await connection.QuerySingleAsync<Customer>(query, new { CustomerID = customerID });
            }
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("AdventureWorks")))
            {
                await connection.OpenAsync();
                var query = "Update SalesLT.Customer set FirstName=@FirstName, EmailAddress=@EmailAddress where CustomerID = @CustomerID";
                await connection.QueryAsync<Customer>(query, new { CustomerID = customer.CustomerID, FirstName = customer.FirstName, EmailAddress = customer.EmailAddress });
                return customer;
            }
        }
    }
}
