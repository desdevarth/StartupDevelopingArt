using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using H_Sport.Models;
using H_SportServices.Intefaces;
using H_SportServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWorld.Controllers.Lynda.H_Sport
{
    [Authorize]

    [Route("api/[controller]/[action]")]
     [ApiController]
  
    public class CustomerController : ControllerBase
    {
        readonly H_Plus_SportsContext _h_Plus_SportsContext;
        readonly ICustomerService _customerService;
        public CustomerController(H_Plus_SportsContext h_Plus_SportsContext,ICustomerService customerService)
        {
            _h_Plus_SportsContext = h_Plus_SportsContext;
            _customerService = customerService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetToken()
        {
            var jwt = new JwtSecurityToken();

            return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _h_Plus_SportsContext.Customer.FindAsync(id);
                if (customer == null) return NotFound();

                _h_Plus_SportsContext.Customer.Remove(customer);
                return (await _h_Plus_SportsContext.SaveChangesAsync()) > 0 ? Ok() : StatusCode(500);
            }
            catch (DbUpdateConcurrencyException)
            {


                throw;
            }
            catch(Exception exp)
            {
                throw;
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustome([FromRoute] int id, Customer customer)
        {
            if (!ModelState.IsValid || id!=customer.CustomerId)
            {
                return BadRequest(customer);
            }
            int a = 0;
            _h_Plus_SportsContext.Entry(customer).State = EntityState.Modified;
           int resualt= await _h_Plus_SportsContext.SaveChangesAsync();

            return Ok(customer);
        }
        

        [HttpGet]
        [ResponseCache(Duration =60)]
        
        public IActionResult GetAllCustomer()
            {
            // return new ObjectResult(_h_Plus_SportsContext.Customer);
            // return Ok(_h_Plus_SportsContext.Customer);

            Request.HttpContext.Response.Headers.Add("X-Total-Custumer", _h_Plus_SportsContext.Customer.Count().ToString());
            //Request.HttpContext.Response.Headers.Add("X-Admin-Name", "Hassan");
            return new ObjectResult(_customerService.GetAllCustomer())
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        //[Route("[controller]/[action]/{id}")]
        //[HttpGet("[controller]/[action]/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByID([FromRoute] int id)
        {
            //var custoemr = _h_Plus_SportsContext.Customer.FirstOrDefault(p => p.CustomerId == id);
            var custoemr = await _customerService.GetCustomerByIDAsync(id);

            if (custoemr == null)
                return NotFound();

            // return custoemr == null ? Ok(custoemr):NotFound();
             return Ok(custoemr);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromForm] Customer customer)
        {
            var a = HttpContext.Request;
            var b = Request.Form["customer"];
            if (ModelState.IsValid)
            {

                return await _customerService.AddCustomerAsync(customer) ? Ok() : StatusCode(500);
               
            }
            return BadRequest();
        }
    }
}