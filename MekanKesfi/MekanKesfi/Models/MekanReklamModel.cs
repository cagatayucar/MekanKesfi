using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MekanKesfi.Models
{
    public class MekanReklamModel
    {
        public IEnumerable<MekanKesfi.Models.Mekanlar> mekanlar { get; set; }
        public IEnumerable<MekanKesfi.Models.Reklam> reklam { get; set; }
   
    }



    





    }
