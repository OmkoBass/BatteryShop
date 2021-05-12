using AutoMapper;
using Battery_Shop.Data;
using Battery_Shop.Dtos;
using Battery_Shop.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Customers = await _unitOfWork.ICustomerRepo.GetAllCustomers();

            return Ok(Customers);
        }

        [HttpGet(":id")]
        public async Task<IActionResult> Get(int Id)
        {
            var Customer = await _unitOfWork.ICustomerRepo.GetCustomer(Id);

            if (Customer != null)
            {
                return Ok(Customer);
            }

            return NotFound(new { Message = $"Employee with Id:{Id} not found!" });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCustomerDto Customer)
        {
            if (ModelState.IsValid)
            {
                int BatteryShopId = Int32.Parse(User.FindFirst("BatteryShopId").Value);

                var AddedCustomer = _mapper.Map<Customer>(Customer);

                AddedCustomer.BatteryShopId = BatteryShopId;

                await _unitOfWork.ICustomerRepo.AddCustomer(AddedCustomer);
                await _unitOfWork.Complete();

                return Ok(AddedCustomer);
            }

            return BadRequest(new { Message = "Invalid info!" });
        }
    }
}
