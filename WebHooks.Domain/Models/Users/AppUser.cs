using Microsoft.AspNetCore.Identity;

namespace WebHook.Domain.Models.Users;

public class AppUser : IdentityUser
{
    public DateTime CreatedOn { get; set; }
}