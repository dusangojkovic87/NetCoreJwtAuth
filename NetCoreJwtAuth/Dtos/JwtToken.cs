using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreJwtAuth.Models
{
    public class JwtToken
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
        [Required]
        public bool isAuthenticated { get; set; }
    }
}