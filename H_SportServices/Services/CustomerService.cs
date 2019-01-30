using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using H_Sport.Models;
using H_SportServices.Intefaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace H_SportServices.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly H_Plus_SportsContext _h_Plus_SportContext;
        IMemoryCache _MemoryCache;
        public CustomerService(H_Plus_SportsContext context,IMemoryCache memoryCache)
        {
            _MemoryCache = memoryCache;
            _h_Plus_SportContext = context;
        }
        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            _h_Plus_SportContext.Customer.Add(customer);
            return await _h_Plus_SportContext.SaveChangesAsync()>0 ? true : false;
        }
        
        public IEnumerable<Customer> GetAllCustomer()
        {
            return _h_Plus_SportContext.Customer;
        }

        public async Task<Customer> GetCustomerByIDAsync(int id)
        {
            var cachedCustomer=_MemoryCache.Get<Customer>(id);
            if (cachedCustomer != null)
                return cachedCustomer;
            else
            {
                var customer= await _h_Plus_SportContext.Customer.FirstOrDefaultAsync(p => p.CustomerId == id);
                _MemoryCache.Set<Customer>(customer.CustomerId, customer);
                return customer;
            }
        }
    }
}
