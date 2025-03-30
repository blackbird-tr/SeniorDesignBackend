using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Common.DTOs.User
{
    public class GetUserByIdResponse
    { 
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
