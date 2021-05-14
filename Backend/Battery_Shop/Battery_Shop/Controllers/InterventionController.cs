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
            AddIntervention.BatteryShopId = BatteryShopId;

            await _unitOfWork.IInterventionRepo.AddIntervention(AddIntervention);
            Battery.Sold = true;
            await _unitOfWork.Complete();

            return Ok(new { Message = "Intervention created!" });
        }
    }
}
