using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Data
{
    public class IdentityApiContext : IdentityDbContext
    {
        public IdentityApiContext(DbContextOptions<IdentityApiContext> options) : base(options)
        {

        }
    }
}
