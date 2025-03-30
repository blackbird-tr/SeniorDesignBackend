using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Common.DTOs.User
{
    public class LoginUserRequest
    {
        public string email { get; set; }
        public string Password { get; set; }
    }
}
