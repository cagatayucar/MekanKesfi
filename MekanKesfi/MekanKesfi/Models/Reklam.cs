namespace MekanKesfi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reklam")]
    public partial class Reklam
    {
        [Key]
        public int reklam_id { get; set; }

        public int? mekan_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? verilis_zamani { get; set; }

        public string aciklama { get; set; }

        public virtual Mekanlar Mekanlar { get; set; }
    }
}
