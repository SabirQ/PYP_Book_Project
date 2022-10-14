using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PYP_Book.Application.Common.Dtos;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountsController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Description = "Password and Confirm Password is not same"
                });
            }
            IdentityResult result=await _identityService.RegisterAsync(registerDto);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Errors = result.Errors
                });
            }

            IdentityResult roleResult = await _identityService.AddRoleAsync(registerDto.UserName, "Member");
            if (!roleResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Errors = roleResult.Errors
                });
            }

            return Ok($"User {registerDto.UserName} was succesfully registered");
        }
        //[HttpGet("roles")]
        //private async Task CreateRoles()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    await _roleManager.CreateAsync(new IdentityRole("Moderator"));
        //    await _roleManager.CreateAsync(new IdentityRole("Member"));
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody]LoginDto userDto)
        //{
        //    AppUser existedUser = await _userManager.FindByNameAsync(userDto.UserName);

        //    if (existedUser is null) return NotFound();

        //    //await _signInManager.PasswordSignInAsync(,) for MVC
        //    bool result = await _userManager.CheckPasswordAsync(existedUser, userDto.Password);

        //    if (!result) return NotFound(new
        //    {
        //        Description = "Username or Password is incorrect"
        //    });

        //    // _usermanager.getclaims(existeduser)



        //    List<Claim> claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, existedUser.Id),
        //        new Claim(ClaimTypes.UserData, existedUser.UserName),
        //        new Claim("Fullname", existedUser.Fullname),
        //        new Claim(ClaimTypes.Email, existedUser.Email)
        //    };

        //    IList<string> roles = await _userManager.GetRolesAsync(existedUser);


        //    claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        //    string keyStr = _config["Jwt:Key"];

        //    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));

        //    SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    JwtSecurityToken token = new JwtSecurityToken(
        //        audience: _config["Jwt:Audience"],
        //        issuer: _config["Jwt:Issue"],
        //        expires: DateTime.Now.AddSeconds(15),
        //        signingCredentials: credentials,
        //        claims: claims
        //        );

        //    string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

        //    return Ok(tokenStr);

        //}

    }


}
