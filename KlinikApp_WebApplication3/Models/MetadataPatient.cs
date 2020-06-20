using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KlinikApp_WebApplication3.Models
{
    [MetadataType(typeof(MetadataPatient))]
    public partial class Patient
    {
        public int? NrOfExams
        {
            get { return this.Examinations.Count; }
        }
    }

    public class MetadataPatient
    {
        [Display(Name = "Exams")]
        public int NrOfExams { get; }

        [Display(Name = "Patient nr.")]
        public string Pat_Id { get; set; }

        [Display(Name = "Lastname")]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Lastname to short")]
        public string  P_Lastname{ get; set; }

        [Display(Name = "Firstname")]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Firstname to short")]
        public string P_Firstname { get; set; }

        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> P_Birthday { get; set; }

        [Display(Name = "Address")]
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Address to short")]
        public string P_Address { get; set; }

        [Display(Name = "PLZ")]
        [Required]
        public int P_Plz { get; set; }

        [Display(Name = "Bundesland")]
        [Required]
        public string P_Bundesland { get; set; }

    }
}