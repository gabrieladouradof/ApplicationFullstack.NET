using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Bcpg;

namespace Dimadev.Api.Models
{
    public class User : IdentityUser<long>
    {
        public List<IdentityRole<long>>? Roles {  get; set; } 
    }
}
