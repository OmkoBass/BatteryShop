using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Battery_Shop.Data.AuthRepo
{
    public class MockAuthRepo : IAuthRepo
    {
        private readonly DatabaseContext _context;

        public MockAuthRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            var Employee = await _context.Employees.FirstOrDefaultAsync(e => e.Username == username && e.Password == password);

            if (Employee != null)
            {
                // Yes i know, i'm not gonna deploy this, don't worry
                var key = "Testing12 qwe qwkej qiowejio qjweioj ioqjweoi 3";

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Id", Employee.Id.ToString()),
                        new Claim("Username", Employee.Username),
                        new Claim("Job", Employee.Job.ToString()),
                        new Claim("BatteryShopId", Employee.BatteryShopId.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }

            return null;
        }
    }
}
