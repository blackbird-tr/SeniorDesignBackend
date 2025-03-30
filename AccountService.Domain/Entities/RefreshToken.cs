using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool isExpired=>DateTime.UtcNow>=Expires;
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }
        public bool IsActive => Revoked == null && !isExpired;

        public string? RemoteIpAdress { get; private set; }
        public string UserID
        {
            get; set;
        }

        public User User { get; set; }

        public RefreshToken(string token,DateTime expires,string userId,string remoteIP)
        {
            this.Token = token; 
            this.Expires = expires;
            this.UserID = userId;
            this.RemoteIpAdress = remoteIP;
            
        }
        public RefreshToken()
        {
            
        }


    }


}
