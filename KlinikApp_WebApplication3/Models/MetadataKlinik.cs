using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KlinikApp_WebApplication3.Models
{
    [MetadataType(typeof(MetadataKlinik))]
    public partial class Klinik
    {
    }
    public class MetadataKlinik
    {
        [Display(Name = "Klinik")]
        [Required]
        public int K_Address { get; set; }
    }
}