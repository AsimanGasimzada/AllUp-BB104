using Microsoft.AspNetCore.Identity;

namespace AllUp_BB104.Models;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; }
}