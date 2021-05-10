using Battery_Shop.Data.AuthRepo;
using Battery_Shop.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Battery_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;
        public AuthController(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("/authenticate")]
        public async Task<IActionResult> Login([FromBody] AuthEmployee Employee)
        {
            var token = await _authRepo.Authenticate(Employee.Username, Employee.Password);

            if (token != null)
                return Ok(new { Token = $"Bearer {token}" });
            return Unauthorized(new { Message = "Unauthorized!" });
        }
    }
}
