using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using Yconic.Domain.Dtos.User;
using Yconic.Domain.Dtos.UserDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.UserRepositories;

namespace Yconic.Application.Services.UserServices
{
    public class UserService:IUserService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly ILogger<UserService> _logger;
        protected readonly IMapper _mapper;
        public UserService(IUserRepository userRepository , ILogger<UserService> logger,IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var listUser = _mapper.Map<List<UserDto>>(users);
            return ApiResult<List<UserDto>>.Success(listUser);
        }
        public async Task<ApiResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            var mappedUser = _mapper.Map<UserDto>(user);    
            return ApiResult<UserDto>.Success(mappedUser);
        }
        public async Task<ApiResult<UserDto>> CreateUser(User user)
        {
            var newUser = await _userRepository.Add(user);
            var mappedUser = _mapper.Map<UserDto>(newUser);
            return ApiResult<UserDto>.Success(mappedUser);
        }
        public async Task<ApiResult<UserDto>> UpdateUser(User user)
        {
            var updatedUser = await _userRepository.Update(user);
            var mappedUser = _mapper.Map<UserDto>(updatedUser);

            return ApiResult<UserDto>.Success(mappedUser);
        }

        public async Task<ApiResult<UserDto>> AddProfilePhoto(Guid id, AddProfilePhotoDto profilePhotoDto)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return ApiResult<UserDto>.Fail("User not found");
            }
            var photoUrl = await SavePhoto(profilePhotoDto.photo);
            user.ProfilePhoto = photoUrl;
            var updatedUser = await _userRepository.Update(user);
            var mappedUser = _mapper.Map<UserDto>(updatedUser);
            return ApiResult<UserDto>.Success(mappedUser);
        }

        public async Task<ApiResult<bool>> UpdatePrivacy(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            
            if (user == null)
            {
                return ApiResult<bool>.Fail("User not found");
            }

            user.IsPrivate = !user.IsPrivate;
            
            await _userRepository.Update(user);
            
            return ApiResult<bool>.Success(user.IsPrivate);
        }
        public async Task DeleteUser(Guid id)
        {
            await _userRepository.Delete(id);
        }

        private string NormalizeFileName(string fileName)
        {
            var normalized = fileName
                .ToLowerInvariant()
                .Replace("ı", "i")
                .Replace("ğ", "g")
                .Replace("ü", "u")
                .Replace("ş", "s")
                .Replace("ö", "o")
                .Replace("ç", "c")
                .Replace(" ", "_");

            return Regex.Replace(normalized, @"[^a-zA-Z0-9_\.\-]", "");
        }



        private async Task<string> SavePhoto(IFormFile photo)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            Directory.CreateDirectory(uploadsFolder);

            var originalName = Path.GetFileNameWithoutExtension(photo.FileName);
            var extension = Path.GetExtension(photo.FileName);
            var normalized = NormalizeFileName(originalName);
            var uniqueFileName = $"{Guid.NewGuid()}_{normalized}{extension}";

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }

            return $"/uploads/{uniqueFileName}";
        }
    }
}
