using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KlinikApp_WebApplication3.Models
{
    [MetadataType(typeof(MetadataExamtype))]
    public partial class Examtype
    {
    }
    public class MetadataExamtype
    {
        [Display(Name = "Examination")]
        [Required]
        public string Exty_Name { get; set; }
    }
}