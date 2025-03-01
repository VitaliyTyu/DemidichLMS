using Microsoft.AspNetCore.Identity;

namespace AuthService.Models;

public class User : IdentityUser
{
    public string FullName { get; set; }
    public string Role { get; set; } // Admin, Manager, Employee
}