using Yconic.Domain.Enums;
using Yconic.Domain.Models.UserModels;

namespace Yconic.Domain.Models;

public class Persona : BaseClass
{
    public Personas Usertype { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
}