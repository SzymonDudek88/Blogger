using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructute.Identity
{
    public class UserRoles
    {

        public const string Admin = "Admin";
        public const string User = "User";
        public const string SuperUser = "SuperUser";
    
        public const string AdminOrUser = Admin + "," + User;

        public const string AdminOrUserOrSuperUser = Admin + "," + User + "," + SuperUser;  // wszyscy 3 
        public const string UserOrSuperUser = User + "," + SuperUser;                   
        public const string AdminOrSuperUser = Admin + "," + SuperUser;
    }
}
