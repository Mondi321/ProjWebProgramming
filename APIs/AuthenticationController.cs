using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjWebProgramming.Configurations;
using ProjWebProgramming.Models;
using ProjWebProgramming.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjWebProgramming.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        //private readonly JwtConfig _jwtConfig;
        public AuthenticationController(UserManager<User> userManager, IConfiguration configuration, SignInManager<User> signInManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _roleManager = roleManager;
            //_jwtConfig = jwtConfig;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        {
            if(ModelState.IsValid)
            {
                var user_exist = await _userManager.FindByEmailAsync(requestDto.Email);
                if (user_exist != null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Email already exist"
                        }
                    });
                }
                var newUser = new User()
                {
                    FirstName= requestDto.FirstName,
                    UserName = requestDto.UserName,
                    Email = requestDto.Email
                };

                var isCreated = await _userManager.CreateAsync(newUser, requestDto.Password);

                if (isCreated.Succeeded)
                {
                    var token = await GenerateJwtToken(newUser);

                    await _userManager.AddToRoleAsync(newUser, "User");

                    return Ok(new AuthResult()
                    {
                        FirstName = newUser.FirstName,
                        UserName = newUser.UserName,
                        Roli = await _userManager.GetRolesAsync(newUser),
                        Result = true,
                        Token = token
                    });
                }
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Server Error"
                    },
                    Result = false
                });
            }

            return BadRequest();
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto loginRequestDto)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(loginRequestDto.Email);

                if (existingUser == null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "Invalid Email"
                        },
                        Result = false
                    });
                }
                var isCorrect = await _signInManager.PasswordSignInAsync(existingUser, loginRequestDto.Password, false, false);

                if (!isCorrect.Succeeded)
                {
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "Invalid Password"
                        },
                        Result = false
                    });
                }

                var jwtToken = await GenerateJwtToken(existingUser);

                return Ok(new AuthResult()
                {
                    FirstName = existingUser.FirstName,
                    UserName = existingUser.UserName,
                    Roli = await _userManager.GetRolesAsync(existingUser),
                    Token = jwtToken,
                    Result = true
                });
            }

            return BadRequest(new AuthResult()
            {
                Result = false,
                Errors = new List<string>()
                {
                    "Invalid email or password"
                }
            });
        }

        [Authorize(AuthenticationSchemes ="Api")]
        [HttpGet]
        public async Task<IActionResult> CurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if(user == null)
            {
                return NotFound();
            }

            var jwtToken = await GenerateJwtToken(user);
            var rolet = await _userManager.GetRolesAsync(user);

            return Ok(new AuthResult()
            {
                Result = true,
                FirstName= user.FirstName,
                UserName = user.UserName,
                Token = jwtToken,
                Roli = rolet
            });
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            var claims = await GetAllValidClaims(user);
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
        private async Task<List<Claim>> GetAllValidClaims(User user)
        {
            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, value:user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {

                var role = await _roleManager.FindByNameAsync(userRole);

                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));

                    var roleClaims = await _roleManager.GetClaimsAsync(role);

                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            return claims;
        }
    }
}
