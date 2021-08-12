using MyTested.AspNetCore.Mvc;
using Models;

namespace WorkPortal.Tests.Data
{
    public class UserTestData
    {

        public User GetUser()
        {
            var user = new User()
            {
                FirstName = TestUser.Username,
                LastName = TestUser.Username,
                Email = "test@abv.bg",
                PasswordHash = TestUser.AuthenticationType,
                IsApproved = true,
            };
            return user;
        }
    }
}
