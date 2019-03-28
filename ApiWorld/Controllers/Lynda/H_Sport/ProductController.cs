using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using H_SportServices.Intefaces;
using H_SportServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWorld.Controllers.Lynda.H_Sport
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCheapProducts()
        {
            return Ok(await _productService.GetCheapProducts());

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRangePriceProducts(int minPrice,int maxPrice)
        {
            return Ok(_productService.RangePriceProducts(minPrice, maxPrice));
        }
    }
}