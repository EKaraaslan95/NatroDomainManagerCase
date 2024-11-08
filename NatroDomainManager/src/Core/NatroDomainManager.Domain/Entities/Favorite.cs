using NatroDomainManager.Domain.Common;
using NatroDomainManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Domain.Entities
{
    public class Favorite:BaseEntity
    {


        [Required]
        public int InternetNameId { get; set; } // InternetName tablosuna yabancı anahtar.

        [Required]
        public int UserId { get; set; } // Kullanıcı kimliği.

        [Required]
        public DateTime CreatedDate { get; set; }  // Favoriye eklenme tarihi zorunlu.

        [Required]
        public bool IsActive { get; set; } 

        public InternetName InternetName { get; set; } // Foreign key ilişkilendirme
        public User User { get; set; } // Foreign key ilişkilendirme
    }
}
