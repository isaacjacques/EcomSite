using Dapper;
using EcomAPI.Interfaces;
using EcomAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcomAPI.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _IConfiguration;
        private readonly ICustomer _ICustomer;
        private readonly DatabaseContext _context;

        public TokenController(IConfiguration iConfig, ICustomer iCustomer, DatabaseContext context)
        {
            _IConfiguration = iConfig;
            _ICustomer = iCustomer;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string email, string password)
        {
            if (email != null && password != null)
            {
                var customer = await Authenticate(email, password);

                if (customer != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _IConfiguration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("CustomerID", customer.CustomerID.ToString()),
                        new Claim("FirstName", customer.FirstName),
                        new Claim("LastName", customer.LastName),
                        new Claim("Email", customer.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IConfiguration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _IConfiguration["Jwt:Issuer"],
                        _IConfiguration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Customer> Authenticate(string email, string password)
        {
            var query = "SELECT * FROM dbo.Customers WHERE Email = @Email";

            using (var connection = _context.CreateConnection())
            {
                var customer = await connection.QuerySingleOrDefaultAsync<Customer>(query, new { email });

                if (customer != null && customer.ValidatePassword(password))
                {
                    return customer;
                }
            }
            return null;
        }
    }
}