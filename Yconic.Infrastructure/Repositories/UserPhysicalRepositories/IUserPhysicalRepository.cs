using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models.UserModels;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.UserPhysicalRepositories
{
    public interface IUserPhysicalRepository : IGenericRepository<UserPhysical>
    {
    }
}