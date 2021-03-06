﻿using System.Collections.Generic;
using System.Text;
using DapperDemoAPI.DAL;
using DapperDemoAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DapperDemoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[TypeFilter(typeof(CustomerExceptionFilterAttribute))]
    //[ServiceFilter(typeof(CustomerExceptionFilterAttribute))]
    public class CustomerController : Controller
    {
        //private IConfiguration _configuration;
        private CustomerRepository _ourCustomerRepository;

        public CustomerController(IConfiguration configuration)
        {
            //_configuration = configuration;
            _ourCustomerRepository = new CustomerRepository(configuration.GetConnectionString("DefaultConnection"));
        }
       
        // GET: api/Customer
        [Route("Customers/{amount}/{sort}")]
        [HttpGet]
        public IEnumerable<Customer> Get(int amount, string sort)
        {
            return _ourCustomerRepository.GetCustomers(amount, sort);
        }

        // GET: api/Customer/5
        [HttpGet("Customers/{id}")]
        public Customer Get(int id)
        {
            return _ourCustomerRepository.GetSingleCustomer(id);
        }

        // POST: api/Customer/Customers
        [Route("Customers")]
        [HttpPost]
        public bool Post([FromBody] Customer ourCustomer)
        {
            if(ModelState.IsValid)
                return _ourCustomerRepository.InsertCustomer(ourCustomer);
            return false;
        }

        // PUT: api/Customer/5
        [HttpPut("Customers")]
        public bool Put([FromBody] Customer ourCustomer)
        {
            return _ourCustomerRepository.UpdateCustomer(ourCustomer);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("Customers/{id}")]
        public bool Delete(int id)
        {
            return _ourCustomerRepository.DeleteCustomer(id);
        }
    }
}
