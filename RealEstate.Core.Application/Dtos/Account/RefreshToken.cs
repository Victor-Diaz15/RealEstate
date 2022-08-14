using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Dtos.Account
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expire { get; set; }
        public bool IsExpire => DateTime.Now >= Expire;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public string ReplaceToken { get; set; }
        public bool IsActive => Revoked == null && IsExpire == false ? true : false; 


    }
}
