using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Common.DTOs.User
{
    public class UpdateUserRequest
    {
        [Required] public string  UserId { get; set; }
        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }
        [Required] public int BirthYear { get; set; }

         
    }
}
