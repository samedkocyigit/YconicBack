using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using Yconic.Application.Services.FollowServices;
using Yconic.Application.Services.PersonasServices;
using Yconic.Domain.Dtos.User;
using Yconic.Domain.Dtos.UserDtos;
using Yconic.Domain.Enums;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.FollowRepositories;
using Yconic.Infrastructure.Repositories.FollowRequestRepositories;
using Yconic.Infrastructure.Repositories.PersonaRepositories;
using Yconic.Infrastructure.Repositories.UserRepositories;

namespace Yconic.Application.Services.UserServices
{
    public class UserService:IUserService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IFollowRequestRepository _followRequestRepository;
        protected readonly IFollowService _followService;
        protected readonly IPersonaRepository _personasRepository;
        protected readonly ILogger<UserService> _logger;
        protected readonly IMapper _mapper;
        public UserService(IUserRepository userRepository , IFollowRequestRepository followRequestRepository,IPersonaRepository personaRepository, IFollowService followService, ILogger<UserService> logger,IMapper mapper)
        {
            _userRepository = userRepository;
            _followRequestRepository = followRequestRepository;
            _personasRepository = personaRepository;
            _followService = followService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var listUser = _mapper.Map<List<UserDto>>(users);
            return ApiResult<List<UserDto>>.Success(listUser);
        }

        public async Task<ApiResult<List<UserMiniDto>>> GetAllSimpleUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var listUser = _mapper.Map<List<UserMiniDto>>(users);
            return ApiResult<List<UserMiniDto>>.Success(listUser);
        }
        public async Task<ApiResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            var mappedUser = _mapper.Map<UserDto>(user);    
            return ApiResult<UserDto>.Success(mappedUser);
        }
        public async Task<ApiResult<UserPublicDto>> GetUserPublicProfile(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return ApiResult<UserPublicDto>.Fail("User not found");
            }
            var mappedUser = _mapper.Map<UserPublicDto>(user);
            return ApiResult<UserPublicDto>.Success(mappedUser);
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
            if(user.ProfilePhoto != null)
            {
                DeletePhotoFile(user.ProfilePhoto);
            }
            
            var photoUrl = await SavePhoto(profilePhotoDto.photo);
            user.ProfilePhoto = photoUrl;
            var updatedUser = await _userRepository.Update(user);
            var mappedUser = _mapper.Map<UserDto>(updatedUser);
            return ApiResult<UserDto>.Success(mappedUser);
        }

        public async Task<ApiResult<UserDto>> UpdateUserPersonal(Guid id,UserPersonalPatchDto personalPatchDto)
        {
            var user = await _userRepository.GetUserById(id);
            if (personalPatchDto.username != null)
            {
                user.Username = personalPatchDto.username;
            }
            if (personalPatchDto.bio != null)
            {
                user.Bio = personalPatchDto.bio;
            }
            if (personalPatchDto.name != null)
            {
                user.Name= personalPatchDto.name;
            }
            if (personalPatchDto.surname != null)
            {
                user.Surname = personalPatchDto.surname;
            }
            var updatetUser = await _userRepository.Update(user);
            var mappedUser = _mapper.Map<UserDto>(updatetUser);
            return ApiResult<UserDto>.Success(mappedUser);
        }
        public async Task<ApiResult<UserDto>> UpdateUserAccount(Guid id, UserAccountPatchDto userAccountPatchDto)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return ApiResult<UserDto>.Fail("User not found");
            }
            if (string.IsNullOrEmpty(userAccountPatchDto.phoneNumber))
            {
                user.PhoneNumber = userAccountPatchDto.phoneNumber;
            }
            if(userAccountPatchDto.weight != null)
            {
                user.Weight = userAccountPatchDto.weight;
            }
            if (userAccountPatchDto.height != null)
            {
                user.Height = userAccountPatchDto.height;
            }
            if (userAccountPatchDto.birthday != null)
            {
                user.Birthday = userAccountPatchDto.birthday;

                DateTime today = DateTime.Today;

                DateTime birthDate = userAccountPatchDto.birthday.Value;

                int age = today.Year - birthDate.Year;

                if (birthDate > today.AddYears(-age))
                {
                    age--;
                }

                user.Age = age;
            }
            if(userAccountPatchDto.personaType != null)
            {
                if(user.UserPersonaId == null)
                {
                    var newPersona = new Persona()
                    {
                        UserId = user.Id,
                        Usertype = GetUserPersonaType((int)userAccountPatchDto.personaType)
                    };
                    var addedPersona =  await _personasRepository.Add(newPersona);
                    user.UserPersonaId = addedPersona.Id;
                }
                else
                {
                    var persona = await _personasRepository.GetById(user.UserPersonaId.Value);
                    persona.Usertype = GetUserPersonaType((int)userAccountPatchDto.personaType);
                    await _personasRepository.Update(persona);
                }
            }

            var updatedUser = await _userRepository.Update(user);
            var mappedUser = _mapper.Map<UserDto>(updatedUser);
            return ApiResult<UserDto>.Success(mappedUser);
        }

        public async Task<ApiResult<bool>> UpdateUserPassword(Guid id, ChangePasswordDto passwordDto)
        {
            var userToUpdate = await _userRepository.GetUserById(id);
            if (userToUpdate == null)
            {
                return ApiResult<bool>.Fail("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(passwordDto.oldPassword, userToUpdate.Password))
            {
                return ApiResult<bool>.Fail("Old password is incorrect");
            }

            userToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(passwordDto.newPassword);

            await _userRepository.Update(userToUpdate);

            return ApiResult<bool>.Success(true);
        }


        public async Task<ApiResult<UserDto>> UpdatePrivacy(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            
            if (user == null)
            {
                return ApiResult<UserDto>.Fail("User not found");
            }

            user.IsPrivate = !user.IsPrivate;

            if (!user.IsPrivate)
            {
                var pendingRequests = user.FollowRequestsReceived
                            .Where(fr => fr.RequestStatus == RequestStatus.Pending)
                            .ToList();
                foreach (var request in pendingRequests)
                {
                    await _followService.FollowUser(request.RequesterId,user.Id);

                    request.RequestStatus = RequestStatus.Accepted;
                    await _followRequestRepository.Update(request);
                }

            }
            
            var updatedUser = await _userRepository.Update(user);
            var mappedUser = _mapper.Map<UserDto>(updatedUser);
            return ApiResult<UserDto>.Success(mappedUser);
        }
        public async Task DeleteUser(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if(user.ProfilePhoto != null)
            {
                DeletePhotoFile(user.ProfilePhoto);
            }
            foreach(var category in user.UserGarderobe.ClothesCategory)
            {
                foreach (var clothe in category.Clothes)
                {
                    if (clothe.Photos.Count > 0)
                    {
                        foreach (var photo in clothe.Photos)
                        {
                            DeletePhotoFile(photo.Url);
                        }
                    }
                }
            }
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


        private Personas GetUserPersonaType(int id)
        {
            switch (id)
            {
                case 0: return Personas.OldMoney;
                case 1: return Personas.SmartCasual;
                case 2: return Personas.BusinessCasual;
                case 3: return Personas.Gothic;
                case 4: return Personas.Boho;
                case 5: return Personas.Preppy;
                case 6: return Personas.Hipster;
                case 7: return Personas.Minimalist;
                case 8: return Personas.Streetwear;
                case 9: return Personas.Rocker;
                default: throw new Exception("Invalid persona type");
            }
        }

        private async Task<string> SavePhoto(IFormFile photo)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/profile-photos");
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

            return $"/uploads/profile-photos/{uniqueFileName}";
        }

        private void DeletePhotoFile(string photoUrl)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photoUrl.TrimStart('/'));
            if (File.Exists(filePath)) { File.Delete(filePath); }
        }
    }
}
