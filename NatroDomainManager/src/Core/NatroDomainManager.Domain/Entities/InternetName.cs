using NatroDomainManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Domain.Entities
{
    public class InternetName : BaseEntity
    {
        [MaxLength(255, ErrorMessage = "Domain adı en fazla 255 karakter olabilir.")]
        public string DomainName { get; set; } // Alan adının maksimum uzunluğu 255 karakter.

        [Required]
        public bool IsAvailable { get; set; } // Müsaitlik durumu zorunlu.

        [Required]
        public DateTime LastCheckedDate { get; set; } // Son kontrol tarihi zorunlu.

        public DateTime? ExpiryDate { get; set; } // Süresi dolma tarihi, opsiyonel.
    }
}
