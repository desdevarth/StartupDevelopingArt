using H_Sport.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace H_SportServices.Intefaces
{
   public interface ICustomerService
    {
        Task<bool> AddCustomerAsync(Customer customer);
        IEnumerable<Customer> GetAllCustomer();
        Task<Customer> GetCustomerByIDAsync(int id);
        
    }
}
