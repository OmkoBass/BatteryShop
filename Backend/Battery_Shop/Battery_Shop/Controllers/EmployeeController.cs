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
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Employees = await _unitOfWork.IEmployeeRepo.GetAllEmployees();

            return Ok(Employees);
        }

        [HttpGet(":id")]
        public async Task<IActionResult> Get(int Id)
        {
            var Employee = await _unitOfWork.IEmployeeRepo.GetEmployee(Id);

            if(Employee != null)
            {
                return Ok(Employee);
            }

            return NotFound(new { Message = $"Employee with Id:{Id} not found!" });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddEmployeeDto Employee)
        {
            if(ModelState.IsValid)
            {
                int BatteryShopId = Int32.Parse(User.FindFirst("BatteryShopId").Value);

                var AddedEmployee = _mapper.Map<Employee>(Employee);
                AddedEmployee.BatteryShopId = BatteryShopId;

                await  _unitOfWork.IEmployeeRepo.AddEmployee(AddedEmployee);
                await _unitOfWork.Complete();

                return Ok(AddedEmployee);
            }

            return BadRequest(new { Message = "Invalid info!" });
        }

        [HttpPut(":id")]
        public async Task<IActionResult> Put(int Id, [FromBody] AddEmployeeDto Employee)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid info!" });
            }

            var OldEmployee = await _unitOfWork.IEmployeeRepo.GetEmployee(Id);

            if(OldEmployee == null)
            {
                return NotFound(new { Message = $"Employee with Id:{Id} not found." });
            }

            _mapper.Map<AddEmployeeDto, Employee>(Employee, OldEmployee);

            await _unitOfWork.Complete();

            return Ok(OldEmployee);
        }

        [HttpDelete(":id")]
        public async Task<IActionResult> Delete(int Id)
        {
            var Employee = await _unitOfWork.IEmployeeRepo.GetEmployee(Id);

            if(Employee == null)
            {
                return NotFound(new { Message = $"Employee with Id:{Id} not found." });
            }

            _unitOfWork.IEmployeeRepo.DeleteEmployee(Employee);
            await _unitOfWork.Complete();

            return Ok(Employee);
        }
    }
}
