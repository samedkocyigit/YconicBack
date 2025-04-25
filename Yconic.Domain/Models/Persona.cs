using Yconic.Domain.Enums;
using Yconic.Domain.Models.UserModels;

namespace Yconic.Domain.Models;

public class Persona : BaseClass
{
    public Guid PersonaTypeId { get; set; }
    public PersonaType? PersonaType { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
}