using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MekanKesfi.Models
{
    public class FirmaKayit
    {
        [Key]
        [StringLength(50)]
        public string kullanici_adi { get; set; }

        [StringLength(100)]
        public string parola { get; set; }

        [StringLength(100)]
        public string parolaTekrar { get; set; }
    }
}