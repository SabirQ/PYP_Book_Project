using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PYP_Book.Application.Common.Dtos;
using PYP_Book.Application.Common.Exceptions;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PYP_Book.Infrastructure.Services;
public class IdentityService : IIdentityService
{

    #region Constructor
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public IdentityService(
        UserManager<AppUser> userManager
        ,RoleManager<IdentityRole> roleManager
        ,IConfiguration config
        ,IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
        _mapper = mapper;
    }
    #endregion
    public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
    {
        AppUser user = new AppUser
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            Name = registerDto.Name,
            Surname = registerDto.Surname
        };

        IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
        return result;
    }
    public async Task<IdentityResult> AddRoleAsync(string username,string role)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == username);
        if (user == null) throw new NotFoundException($"User with this {username} was not found");
        IdentityResult result = await _userManager.AddToRoleAsync(user, role);
        return result;
    }
    public async Task CreateRoles(string role)
    {
        await _roleManager.CreateAsync(new IdentityRole(role));
    }
    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        AppUser existedUser = await _userManager.FindByNameAsync(loginDto.UserName);

        if (existedUser is null) throw new LoginException("Username or password is incorrect");

        bool result = await _userManager.CheckPasswordAsync(existedUser, loginDto.Password);

        if (!result) throw new LoginException("Username or password is incorrect");

        //_userManager.GetClaims(existedUser)

        List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, existedUser.Id),
                new Claim(ClaimTypes.UserData, existedUser.UserName),
                new Claim("Name", existedUser.Name),
                new Claim("Surname", existedUser.Surname),
                new Claim(ClaimTypes.Email, existedUser.Email)
            };

        IList<string> roles = await _userManager.GetRolesAsync(existedUser);


        claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

        string keyStr = _config["Jwt:Key"];

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));

        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            audience: _config["Jwt:Audience"],
            issuer: _config["Jwt:Issue"],
            expires: DateTime.Now.AddSeconds(15),
            signingCredentials: credentials,
            claims: claims
            );

        string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenStr;
    }
    public async Task<IdentityResult> DeleteUserAsync(string username)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == username);
        if (user == null) throw new NotFoundException($"User with this {username} was not found");
        IdentityResult result=await _userManager.DeleteAsync(user);
        return result;
    }
    public async Task<IEnumerable<UserDto>> GetAll()
    {
        List<UserDto> users =_mapper.Map<List<UserDto>>(await _userManager.Users.ToListAsync());
       return users;
    }
    public async Task<bool> IsInRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
        return user != null && await _userManager.IsInRoleAsync(user, roleName);
    }


}
