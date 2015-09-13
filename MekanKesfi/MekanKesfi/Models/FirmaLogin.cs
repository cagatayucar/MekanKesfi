namespace MekanKesfi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FirmaLogin")]
    public partial class FirmaLogin
    {
        [Key]
        [StringLength(50)]
        public string kullanici_adi { get; set; }

        [StringLength(100)]
        public string parola { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int mek_id { get; set; }
    }
}
