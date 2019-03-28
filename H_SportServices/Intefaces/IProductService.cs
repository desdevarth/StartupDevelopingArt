using H_Sport.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace H_SportServices.Intefaces
{
   public interface IProductService
    {
        Task<IEnumerable<Product>> GetCheapProducts();
        Task<IEnumerable<Product>> RangePriceProducts(int minPrice, int maxPrice);
    }
}
