using System;
using System.Collections.Generic;
using System.Text;

namespace DapperDemoAPI.DAL
{
    /// <summary>
    /// Disclaimer: This is not the best design, and I certainly wouldn’t use this for a production application, 
    /// but it does serve as a good demonstration of how to use Dapper. by Jeremy Morgan.
    /// </summary>
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers(int amount,string sort);
        Customer GetSingleCustomer(int customerId);
        bool InsertCustomer(Customer ourCustomer);

        bool DeleteCustomer(int customerId);
        bool UpdateCustomer(Customer ourCustomer);
    }
}
