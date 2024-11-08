using NatroDomainManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Domain.Entities
{
    public class User:BaseEntity
    {


        [Required]
        [MaxLength(100)] 
        public string UserName { get; set; }

        [Required]
        [EmailAddress] 
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)] 
        public string PasswordHash { get; set; }

    }
}
