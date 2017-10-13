using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager
{
    public enum UserType : int
    {
        None,
        User,
        Moderator,
        Admin
    }
    public class User
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }

        public static User[] GetUsers()
        {
            return new User[]
            {
                new User() { Name = "Ivan", Phone = "256884", Type = UserType.Moderator},
                new User() { Name = "Bhamri", Phone = "272435", Type = UserType.User},
                new User() { Name = "Vlad", Phone = "5436373", Type = UserType.User},
                new User() { Name = "Nasty", Phone = "4363467", Type = UserType.Admin}
            };
        }
    }
}
