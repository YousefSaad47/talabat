using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity.Context;

public class StoreIdentityDbContext : IdentityDbContext<AppUser>
{
    public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
    {
        
    }
}