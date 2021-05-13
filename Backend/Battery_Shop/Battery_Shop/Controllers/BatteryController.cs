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
        public async Task<IActionResult> GetAll()
        {
            var AllBatteries = await _unitOfWork.IBatteryRepo.GetAllBatteries();

            return Ok(AllBatteries);
        }

        [HttpGet("replace/:id")]
        public async Task<IActionResult> ReplaceBattery(int Id)
        {
            var Battery = await _unitOfWork.IBatteryRepo.GetBattery(Id);

            if(Battery == null)
            {
                return NotFound(new { Message = $"Battery with Id:{Id} not found!" });
            }

            Battery.Replacement = false;

            return Ok(new { Message = "Battery replaced" });
        }

        [HttpGet("replacement")]
        public async Task<IActionResult> GetReplacementBatteriesByBatteryShop()
        {
            int BatteryShopId = int.Parse(User.FindFirst("BatteryShopId").Value);

            var AllBatteries = await _unitOfWork.IBatteryRepo.GetBatteriesByBatteryShop(BatteryShopId, true, true);

            return Ok(AllBatteries);
        }

        [HttpGet("sold")]
        public async Task<IActionResult> GetSoldBatteriesByBatteryShop()
        {
            int BatteryShopId = int.Parse(User.FindFirst("BatteryShopId").Value);

            var AllBatteries = await _unitOfWork.IBatteryRepo.GetBatteriesByBatteryShop(BatteryShopId, true, false);

            return Ok(AllBatteries);
        }

        [HttpGet("batteryShop")]
        public async Task<IActionResult> GetBatteriesByBatteryShop()
        {
            int BatteryShopId = int.Parse(User.FindFirst("BatteryShopId").Value);

            var AllBatteries = await _unitOfWork.IBatteryRepo.GetBatteriesByBatteryShop(BatteryShopId, false, false);

            return Ok(AllBatteries);
        }

        [HttpGet(":id")]
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

            Random rand = new Random();

            int BatteryLifeRandom = rand.Next(1, 100);

            if (BatteryLifeRandom >= 50)
            {
                return Ok(new { Message = "Recalibrated everything is okay!" });
            }

            if (DateTime.Now > Battery.Warrant)
            {
                return Ok(new { Message = "Your warranty ran out!" });
            }

            Battery.Replacement = true;
            await _unitOfWork.Complete();

            return Ok(new { Message = "Choose a new battery!" });
        }

        [HttpPost("sell/:batteryId")]
        public async Task<IActionResult> BuyBattery(int BatteryId, [FromBody]AddCustomerDto Customer)
        {
            //string JobId = User.FindFirst("Job").Value;

            //if (int.Parse(JobId) != 1)
            //{
            //    return Unauthorized(new { Message = "Only Employees can do this!" });
            //}

            int BatteryShopId = int.Parse(User.FindFirst("BatteryShopId").Value);

            var Battery = await _unitOfWork.IBatteryRepo.GetBattery(BatteryId);

            if(Battery == null)
            {
                return BadRequest(new { Message = "Invalid Battery Id!" });
            }

            if(Battery.Sold)
            {
                return BadRequest(new { Message = "Battery already sold!" });
            }

            var FoundCustomer = await _unitOfWork.ICustomerRepo.GetCustomerByInfo(Customer.Name, Customer.LastName, Customer.Address);

            if(FoundCustomer != null)
            {
                Battery.CustomerId = FoundCustomer.Id;
                Battery.Sold = true;
                Battery.Warrant = DateTime.Now.AddYears(2);
            } else
            {
                var AddCustomer = _mapper.Map<Customer>(Customer);

                AddCustomer.BatteryShopId = BatteryShopId;

                await _unitOfWork.ICustomerRepo.AddCustomer(AddCustomer);

                await _unitOfWork.Complete();

                Battery.CustomerId = AddCustomer.Id;
                Battery.Sold = true;
                Battery.Warrant = DateTime.Now.AddYears(2);
            }

            await _unitOfWork.Complete();

            return Ok(_mapper.Map<BatteryDto>(Battery));
        }
    }
}
