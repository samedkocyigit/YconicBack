using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models.UserModels;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.UserRepositories
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<ICollection<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(Guid id);
    }
}
