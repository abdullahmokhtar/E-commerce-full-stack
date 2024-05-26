using Microsoft.AspNetCore.Identity;

namespace Backend.DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public Address Address { get; set; }
        public UserResetCode UserResetCode { get; set; }
        public Cart Cart { get; set; } = null!;
        public List<Order> Orders { get; set; } = [];
    }
}
