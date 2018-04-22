using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelMVC.Models;
using HotelMVC.DataContext;
using System.Security.Cryptography;
using System.Text;

namespace HotelMVC.Services
{
    public class UserService
    {
        public static UserModel SignIn(UserModel userModel)
        {
            using (HotelContext context = new HotelContext())
            {
                var user = context.Users.FirstOrDefault(x => x.UserName == userModel.UserName);
                if (user != null && user.Password == Hash(userModel.Password))
                {
                    var userRoles = context.UserRoles.Where(x => x.IdUser == user.Id).FirstOrDefault(); //1 to 1
                    return new UserModel() { UserName = user.UserName, Role = userRoles.Role.Name, Id = user.Id };
                }
                return null;
            }

        }

        public static bool Register(UserModel userModel)
        {
            using (HotelContext context = new HotelContext())
            {
                if (context.Users.Any(x => x.UserName == userModel.UserName || x.Email == userModel.Email))
                {
                    return false;
                }

                var hash = Hash(userModel.Password);
                var userToAdd = context.Users.Add(new User
                {
                    UserName = userModel.UserName,
                    Email = userModel.Email,
                    Password = hash,

                });
                userToAdd.UserRoles.Add(new UserRole() { IdRole = 2, IdUser = userToAdd.Id });

                context.SaveChanges();
                return true;
            }

        }

        private static string Hash(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }
    }
}