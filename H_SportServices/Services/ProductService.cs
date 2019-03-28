using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using H_Sport.Models;
using H_SportServices.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace H_SportServices.Services
{
   public class ProductService:IProductService
    {
        readonly H_Plus_SportsContext _h_Plus_SportsContext;
        public ProductService(H_Plus_SportsContext context)
        {
            _h_Plus_SportsContext = context;
        }

        public async Task<IEnumerable<Product>> GetCheapProducts()
        {
            return await _h_Plus_SportsContext.Product.FromSql("GetCheapProducts").ToListAsync();
             
        }

        public async Task<IEnumerable<Product>> RangePriceProducts(int minPrice,int maxPrice)
        {

            try
            {
                var a = await _h_Plus_SportsContext.Product.FromSql<Product>($"GetRangeProducts {minPrice},{maxPrice}").ToListAsync<Product>();
                return a;
            }
            catch (Exception exp)
            {

                throw;
            }
        }

    }
}
