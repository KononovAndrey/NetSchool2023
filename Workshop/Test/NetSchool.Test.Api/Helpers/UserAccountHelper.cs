namespace NetSchool.Test.Api;

using Microsoft.AspNetCore.Identity;
using NetSchool.Context.Entities;
using System.Threading.Tasks;

public static class UserAccountHelper
{
    public static async Task CreateUserAccount(UserManager<User> userManager, string userName, string password, string email)
    {
        var user = new User()
        {
            Status = UserStatus.Active,
            UserName = userName,  
            FullName = userName,
            Email = email,
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false
        };

        await userManager.CreateAsync(user, password);
    }
}