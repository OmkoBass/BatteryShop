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
    public class BatteryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BatteryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var AllBatteries = await _unitOfWork.IBatteryRepo.GetAllBatteries();

            return Ok(AllBatteries);
        }

        [HttpGet(":id")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int Id)
        {
            var Battery = await _unitOfWork.IBatteryRepo.GetBattery(Id);

            if(Battery != null)
            {
                return Ok(Battery);
            }

            return NotFound(new { Message = $"Battery with Id:{Id} not found!" });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddBatteryDto Battery)
        {
            if (ModelState.IsValid)
            {
                var AddedBattery = _mapper.Map<Battery>(Battery);
                AddedBattery.Life = 100;
                AddedBattery.Sold = false;

                await _unitOfWork.IBatteryRepo.AddBattery(AddedBattery);
                await _unitOfWork.Complete();

                return Ok(AddedBattery);
            }

            return BadRequest(new { Message = "Invalid info!" });
        }

        [HttpPut(":id")]
        public async Task<IActionResult> Put(int Id, [FromBody] AddBatteryDto Battery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid info!" });
            }

            var OldBattery = await _unitOfWork.IBatteryRepo.GetBattery(Id);

            if (OldBattery == null)
            {
                return NotFound(new { Message = $"Battery with Id:{Id} not found." });
            }

            _mapper.Map<AddBatteryDto, Battery>(Battery, OldBattery);

            await _unitOfWork.Complete();

            return Ok(OldBattery);
        }

        [HttpDelete(":id")]
        public async Task<IActionResult> Delete(int Id)
        {
            var Battery = await _unitOfWork.IBatteryRepo.GetBattery(Id);

            if (Battery == null)
            {
                return NotFound(new { Message = $"Battery with Id:{Id} not found." });
            }

            _unitOfWork.IBatteryRepo.DeleteBattery(Battery);
            await _unitOfWork.Complete();

            return Ok(Battery);
        }

        [HttpGet("check/:id")]
        public async Task<IActionResult> CheckBattery(int Id)
        {
            var Battery = await _unitOfWork.IBatteryRepo.GetBattery(Id);

            if (Battery.Life >= 60)
            {
                Battery.Life = 100;

                return Ok(Battery);
            }

            if (Battery.Warrant < DateTime.Now)
            {
                return Ok(new { Message = "Your warranty ran out!" });
            }

            return Ok(new { Message = "Choose a new battery!" });
        }

        [HttpGet(":batteryId/:customerId")]
        public async Task<IActionResult> BuyBattery(int BatteryId, int CustomerId)
        {
            int JobId = Int32.Parse(User.FindFirst("Job").Value);

            if (JobId != 1)
            {
                return Unauthorized(new { Message = "Only Employees can do this!" });
            }

            var Battery = await _unitOfWork.IBatteryRepo.GetBattery(BatteryId);

            if(Battery == null)
            {
                return BadRequest(new { Message = "Invalid Battery Id!" });
            }

            var Customer = await _unitOfWork.ICustomerRepo.GetCustomer(CustomerId);

            if(Customer == null)
            {
                return BadRequest(new { Message = "Invalid Customer Id!" });
            }

            Battery.CustomerId = Customer.Id;
            Battery.Sold = true;
            Battery.Warrant = DateTime.Now.AddYears(2);

            return Ok(Battery);
        }
    }
}
