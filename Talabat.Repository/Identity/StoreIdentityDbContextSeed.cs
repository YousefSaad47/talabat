using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity;

public static class StoreIdentityDbContextSeed
{
    public static async Task SeedAppUserAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser()
        {
            Email = "Yousef123@gmail.com",
            DisplayName = "Yousef Saadallah",
            UserName = "Yousef_Saadallah",
            PhoneNumber = "0000000000",
            Address = new Address()
            {
                FName = "Yousef",
                LName = "Saadallah",
                Street = "Street 1",
                City = "Cairo",
                Country = "Egypt",
            }
        };
        await userManager.CreateAsync(user, "P@sswOrd");
        }
    }
}