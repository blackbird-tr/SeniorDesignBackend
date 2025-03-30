using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Common.DTOs.User
{
    public class LoginUserResponse
    {
        public string UserId { get; set; }
        public string JwToken { get; set; }
        public string RefreshToken { get; set; }
        public bool emailValid { get; set; }
    }
}
