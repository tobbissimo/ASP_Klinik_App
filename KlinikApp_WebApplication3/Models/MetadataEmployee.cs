using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KlinikApp_WebApplication3.Models
{
    [MetadataType(typeof(MetadataEmployee))]
    public partial class Employee
    {
    }
    public class MetadataEmployee
    {
        [Display(Name = "Emp nr.")]
        public int Emp_Id { get; set; }

        [Display(Name = "Lastname")]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Lastname to short")]
        public string Emp_Lastname { get; set; }

        [Display(Name = "Firstname")]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Firstname to short")]
        public string Emp_Firstname { get; set; }

        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Emp_Birthday { get; set; }

        [Display(Name = "Address")]
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Address to short")]
        public string Emp_Address { get; set; }

        [Display(Name = "PLZ")]
        [Required]
        public int Emp_Plz { get; set; }

        [Display(Name = "Salary")]
        [Required]
        public int Emp_Salary { get; set; }

        [Display(Name = "Bundesland")]
        [Required]
        public string Emp_Bundesland { get; set; }

        [Display(Name = "Klinik")]
        [Required]
        public string Emp_Klinik { get; set; }
    }
}