using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GBank.Customer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Return all Customers
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthConstants.RoleUser)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_customerService.GetAllCustomers());
        }

        /// <summary>
        /// Return specific Customer with Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AuthConstants.RoleUser)]
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Result<CustomerDto> result = _customerService.GetCustomerById(id);
            if(result.IsSuccess)
                return Ok(result);

            return NotFound();
        }

        /// <summary>
        /// Add new customer and account
        /// </summary>
        /// <param name="newCustomer"></param>
        /// <returns></returns>
        [Authorize(AuthConstants.RoleAdmin)]
        [HttpPost]
        public IActionResult AddNewCustomer(AddCustomerDto newCustomer)
        {
            Result result =  _customerService.AddNewCustomer(newCustomer);
            if(result.IsSuccess)
                return Ok(result);

            return NotFound();
        }

    }
}


