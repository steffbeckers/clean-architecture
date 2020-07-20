using Microsoft.AspNetCore.Identity;

namespace Webtrovert.Infrastructure.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
