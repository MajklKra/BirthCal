using Microsoft.AspNetCore.Identity;

namespace BirthCal.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }

        public IEnumerable<AppUser> RoleMembers { get; set; }

        public IEnumerable<AppUser> RoleNonMembers { get; set; }
    }
}
