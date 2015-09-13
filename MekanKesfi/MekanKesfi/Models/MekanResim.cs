using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MekanKesfi.Models
{
    public class MekanResim
    {
        public int id { get; set; }

        [StringLength(60)]
        public string mekan_adi { get; set; }

        public string mekan_adresi { get; set; }

        [StringLength(11)]
        public string telefon { get; set; }

        [StringLength(60)]
        public string email { get; set; }

        public byte[] fotograf { get; set; }

        public decimal? latitude { get; set; }

        public decimal? longitude { get; set; }
        public HttpPostedFileBase photo { get; set; }
    }
}