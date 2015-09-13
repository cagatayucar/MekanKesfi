namespace MekanKesfi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
using System.Web;

    [Table("Mekanlar")]
    public partial class Mekanlar
    {
        public Mekanlar()
        {
            Reklam = new HashSet<Reklam>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        public virtual ICollection<Reklam> Reklam { get; set; }
    }
}
