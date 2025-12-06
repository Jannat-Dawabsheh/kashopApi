using kashop.dal.DTO.Request;
using kashop.dal.DTO.Response;
using kashop.dal.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace kashop.bll.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<ApplicationUser>userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        
        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var user=await _userManager.FindByEmailAsync(loginRequest.Email);
                if(user is null)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "invalid email",
                        
                    };
                }
                var result=await _userManager.CheckPasswordAsync(user, loginRequest.Password);
                if (!result)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "invalid password",

                    };
                }

                return new LoginResponse()
                {
                    Success = true,
                    Message = "login successfully",
                   AccessToken=await GenerateAccessToken(user)

                };

            }
            catch (Exception ex) {
                return new LoginResponse()
                {
                    Success = false,
                    Message = "Exception error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            try
            {


                var user = registerRequest.Adapt<ApplicationUser>();
                var result = await _userManager.CreateAsync(user, registerRequest.Password);
                //foreach(var e in result.Errors)
                //{
                //    Console.WriteLine(e.Description);
                //}
                if (!result.Succeeded)
                {
                    return new RegisterResponse()
                    {
                        Success = false,
                        Message = "User creation failed",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }

                await _userManager.AddToRoleAsync(user, "User");
                return new RegisterResponse()
                {
                    Success = true,
                    Message = "Success"
                };
            }
            catch (Exception ex) {
                return new RegisterResponse()
                {
                    Success = false,
                    Message = "Exception error",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
        public async Task<string>GenerateAccessToken(ApplicationUser user)
        {
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audiennce"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    
    
    }
}
