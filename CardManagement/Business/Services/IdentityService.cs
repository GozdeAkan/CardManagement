using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Business.Contracts;
using Business.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Domain.Entities.Auth;
using Business.Utils;

namespace Business.Services
{
    /// <inheritdoc />
    public class IdentityService: IIdentityService
    {
        private readonly UserManager<ApplicationUserEntity> _userManager;
        private readonly IConfiguration _configuration;

        /// <inheritdoc />
        public IdentityService(UserManager<ApplicationUserEntity> userManager,
          RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration)
        {

            _userManager = userManager;
            _configuration = configuration;
        }
        /// <inheritdoc />
        public async Task<TokenDto?> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim("emailaddress", user.Email ?? ""),
                    new Claim("userid", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };


                var token = JwtHelper.CreateToken(authClaims, _configuration);
                var refreshToken = JwtHelper.GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                user.RefreshToken = refreshToken;

                await _userManager.UpdateAsync(user);

                var response = new TokenDto
                {
                    RefreshToken = refreshToken,
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
                };
                return response;
            }
            return null;

        }
        /// <inheritdoc />
        public async Task<TokenDto?> Register(RegisterDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                
                return null;
            }

            ApplicationUserEntity user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var loginModel = new LoginDto
                {
                    Email = model.Email,
                    Password = model.Password
                };


                var response = await Login(loginModel);
                return response;
            }
            return null;

        }

      
    }
}