using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationPlugin;
using FoodApi.Data;
using FoodApi.Data.Models;
using FoodAppApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FoodAppApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        private FoodAppDbContext _dbContext;
        public AccountsController(FoodAppDbContext dbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _auth = new AuthService(_configuration);
            _dbContext = dbContext;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterApiModel user)
        {
            var userWithSameEmail = _dbContext.Users.SingleOrDefault(u => u.Email == user.Email);
            if (userWithSameEmail != null) return BadRequest("User with this email already exists");
            var userObj = new Users
            {
                Name = user.Name,
                Email = user.Email,
                Password = SecurePasswordHasherHelper.Hash(user.Password),
                Role = "User"
            };
            _dbContext.Users.Add(userObj);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UserLoginApiModel user)
        {
            var userEmail = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            if (userEmail == null) return StatusCode(StatusCodes.Status404NotFound);
            var hashedPassword = userEmail.Password;
            if (!SecurePasswordHasherHelper.Verify(user.Password, hashedPassword)) return Unauthorized();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userEmail.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userEmail.Email),
                new Claim(ClaimTypes.Name,userEmail.Id.ToString() ),
                new Claim(ClaimTypes.Role, userEmail.Role)
            };

            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                token_type = token.TokenType,
                user_Id = userEmail.Id,
                user_name = userEmail.Name,
                expires_in = token.ExpiresIn,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
            });
        }
    }
}
