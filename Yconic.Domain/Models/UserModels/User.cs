using Yconic.Domain.Enums;

namespace Yconic.Domain.Models.UserModels;

public class User : BaseClass
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRoles Role { get; set; } = UserRoles.User;
    public string? PasswordResetToken { get; set; }
    public DateTime? PasswordResetTokenExpiry { get; set; }

    public Status IsActive { get; set; } = Status.Active;

    public DateTime? DeletedAt { get; set; }

    public UserAccount? UserAccount { get; set; }
    public UserPersonal? UserPersonal { get; set; }
    public UserPhysical? UserPhysical { get; set; }
    public Persona? UserPersona { get; set; }
    public Garderobe? UserGarderobe { get; set; }

    public ICollection<Suggestion>? Suggestions { get; set; } = new List<Suggestion>();
    public ICollection<SharedLook>? SharedLooks { get; set; } = new List<SharedLook>();
    public ICollection<Follow>? Followers { get; set; } = new List<Follow>();
    public ICollection<Follow>? Following { get; set; } = new List<Follow>();

    public ICollection<FollowRequest>? FollowRequestsReceived { get; set; } = new List<FollowRequest>();
    public ICollection<FollowRequest>? FollowRequestsSent { get; set; } = new List<FollowRequest>();
}