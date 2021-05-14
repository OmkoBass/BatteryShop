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
    public class InterventionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InterventionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInterventionsByBatteryShop()
        {
            int BatteryShopId = int.Parse(User.FindFirst("BatteryShopId").Value);

            var AllInterventions = await _unitOfWork.IInterventionRepo.GetAllInterventionsByBatteryShop(BatteryShopId);

            return Ok(AllInterventions);
        }

        [HttpGet(":id")]
        public async Task<IActionResult> Get(int Id)
        {
            var Intervention = await _unitOfWork.IInterventionRepo.GetIntervention(Id);

            if (Intervention != null)
            {
                return Ok(Intervention);
            }

            return NotFound(new { Message = $"Intervention with Id:{Id} not found!" });
        }

        [HttpPost("sell/:batteryId")]
        public async Task<IActionResult> Post(int BatteryId, AddInterventionDto Intervention)
        {
            var Battery = await _unitOfWork.IBatteryRepo.GetBattery(BatteryId);

            int BatteryShopId = int.Parse(User.FindFirst("BatteryShopId").Value);

            if (Battery == null)
            {
                return NotFound(new { Message = $"Battery with Id:{BatteryId} not found!" });
            }

            var AddIntervention = _mapper.Map<Intervention>(Intervention);
            AddIntervention.Price = Battery.Price + 5000;
            AddIntervention.BatteryId = Battery.Id;
            AddIntervention.BatteryShopId = BatteryShopId;

            await _unitOfWork.IInterventionRepo.AddIntervention(AddIntervention);
            Battery.Sold = true;
            await _unitOfWork.Complete();

            return Ok(new { Message = "Intervention created!" });
        }

        [HttpPost("resolve/:interventionId/:batteryId")]
        public async Task<IActionResult> ResolveIntervention(int InterventionId, int BatteryId, [FromBody] AddInterventionWithCustomer Customer)
        {
            if (ModelState.IsValid)
            {
                int BatteryShopId = int.Parse(User.FindFirst("BatteryShopId").Value);

                var Intervention = await _unitOfWork.IInterventionRepo.GetIntervention(InterventionId);

                if (Intervention == null)
                {
                    return NotFound(new { Message = $"Intervention with Id:{InterventionId} not found!" });
                }

                var Battery = await _unitOfWork.IBatteryRepo.GetBattery(BatteryId);

                if (Intervention == null)
                {
                    return NotFound(new { Message = $"Battery with Id:{BatteryId} not found!" });
                }

                var FoundCustomer = await _unitOfWork.ICustomerRepo.GetCustomerByInfo(Customer.Name, Customer.LastName, Customer.Address);

                if (FoundCustomer != null)
                {
                    Battery.CustomerId = FoundCustomer.Id;
                    Battery.Sold = true;
                    Battery.Warrant = DateTime.Now.AddYears(2);
                }
                else
                {
                    var AddCustomerDto = new AddCustomerDto()
                    {
                        Name = Customer.Name,
                        LastName = Customer.LastName,
                        Address = Customer.Address
                    };

                    var AddCustomer = _mapper.Map<Customer>(AddCustomerDto);

                    AddCustomer.BatteryShopId = BatteryShopId;

                    await _unitOfWork.ICustomerRepo.AddCustomer(AddCustomer);
                    await _unitOfWork.Complete();

                    Battery.CustomerId = AddCustomer.Id;
                    Battery.Sold = true;
                    Battery.Warrant = DateTime.Now.AddYears(2);

                    await _unitOfWork.Complete();
                }

                Intervention.Resolved = true;
                Intervention.Description = Customer.Description;

                await _unitOfWork.Complete();

                return Ok(Intervention);
            }

            return BadRequest(new { Message = "Invalid info!" });
        }
    }
}
