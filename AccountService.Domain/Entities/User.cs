using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities
{
    public class User:IdentityUser
    {
        public User()
        {
            RefreshTokens=new HashSet<RefreshToken>();  
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set;}
         

    }
}
