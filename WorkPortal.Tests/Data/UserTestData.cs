using System;
using MyTested.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace WorkPortal.Tests.Data
{
    public static class UserTestData
    {

        public static IEnumerable<User> ApprovedUsers
         => Enumerable.Range(0, 2).Select(i => new User
         {
            IsApproved = true,

         });
        //public static User ApprovedUser()
        //{
        //    var user = new User()
        //    {
        //        FirstName = TestUser.Username,
        //        LastName = TestUser.Username,
        //        Email = "test@abv.bg",
        //        PasswordHash = TestUser.AuthenticationType,
        //    };

        //    return user;
        //}
    }
}
