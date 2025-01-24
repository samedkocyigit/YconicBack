using AutoMapper;
using BCrypt.Net;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Yconic.Application.Services.EmailServices;
using Yconic.Application.Services.TokenServices;
using Yconic.Domain.Dtos.Auth;
using Yconic.Domain.Dtos.User;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.GarderobeRepositories;
using Yconic.Infrastructure.Repositories.UserRepositories;

namespace Yconic.Application.Services.AuthServices
{
    public class AuthService:IAuthService
    {
        protected readonly IUserRepository  _userRepository;
        protected readonly IGarderobeRepository _garderobeRepository;
        protected readonly ILogger<AuthService> _logger;
        protected readonly IEmailService _emailService;
        protected readonly ITokenService _tokenService;
        protected readonly IMapper _mapper;
        public AuthService(IUserRepository userRepository,IGarderobeRepository garderobeRepository, IEmailService emailService,ITokenService tokenService,IMapper mapper)
        {
            _userRepository = userRepository;
            _garderobeRepository = garderobeRepository;
            _emailService = emailService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<ApiResult<LoginResponse>> Login(LoginDto loginModel)
        {
            var user = await _userRepository.GetUserByEmail(loginModel.Email);
            if(user == null)
            {
                _logger.LogError("User not found");
                throw new Exception("User not found");
            }
            else if(!BCrypt.Net.BCrypt.Verify(loginModel.Password,user.Password))
            {
                _logger.LogError("Invalid password");
                throw new Exception("Invalid password");
            }
            else
            {
                var token = _tokenService.CreateToken(user);
                var userDto = _mapper.Map<UserDto>(user);
                var loginResponse =new LoginResponse
                {
                    token = token,
                    user = userDto
                };
                return ApiResult<LoginResponse>.Success(loginResponse);
            }
        }
        public async Task<ApiResult<User>> Register(RegisterDto registerModel)
        {
            var user = new User
            {
                Email = registerModel.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerModel.Password),
                Name = registerModel.Name,
                Surname = registerModel.Surname,
                PhoneNumber = registerModel.PhoneNumber,
                Birthday = registerModel.Birthday.ToUniversalTime()
            };
            var createdUser = await _userRepository.Add(user);
            var garderobe = new Garderobe
            {
                UserId = createdUser.Id
            };
            var newGarderobe = await _garderobeRepository.Add(garderobe);
            createdUser.UserGarderobeId = newGarderobe.Id;
            createdUser.UserGarderobe = newGarderobe;
            createdUser.UpdatedAt = DateTime.UtcNow;
            var lastseen =await _userRepository.Update(createdUser);
            return ApiResult<User>.Success(lastseen);
        }
        public async Task ForgotPassword(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if(user == null)
            {
                _logger.LogError("User not found");
                throw new Exception("User not found");
            }
            else
            {
                var token = GenerateResetToken();
                user.PasswordResetToken = token;
                user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1);
                user.UpdatedAt = DateTime.UtcNow;
                await _userRepository.Update(user);

                var resetLink = $"http://localhost:5169/reset-password?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(email)}";

                await _emailService.SendEmailAsync(user.Email, "Password Reset Request",
                          $"here is token={token} to reset your password. This link is valid for 1 hour.");
            }
        }
        private string GenerateResetToken()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var tokenBytes = new byte[32]; 
                rng.GetBytes(tokenBytes);
                return Convert.ToBase64String(tokenBytes);
            }
        }
        public async Task ResetPassword(string email, string token, string newPassword)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("Invalid email.");
            }

            if (user.PasswordResetToken != token || user.PasswordResetTokenExpiry < DateTime.UtcNow)
            {
                throw new Exception("Invalid or expired token.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;

            user.UpdatedAt = DateTime.UtcNow;
            await _userRepository.Update(user);
        }


        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("supersecretkeyunbeliaveablemysteriouskeyinhooaleeertt");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
