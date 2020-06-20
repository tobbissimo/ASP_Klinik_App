using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KlinikApp_WebApplication3.Models
{
    [MetadataType(typeof(MetadataBundesland))]
    public partial class Bundesland
    {
    }

    public class MetadataBundesland
    {
        [Display(Name = "Bundesland")]
        [Required]
        public string B_Name { get; set; }
    }
}