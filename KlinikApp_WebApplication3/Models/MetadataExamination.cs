using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KlinikApp_WebApplication3.Models
{
    [MetadataType(typeof(MetadataExamination))]
    public partial class Examination
    {
        public string PatientName
        {
            get { return this.Patient.P_Firstname + " " + this.Patient.P_Lastname; }
        }

        public string EmployeeName
        {
            get { return this.Employee.Emp_Firstname + " " + this.Employee.Emp_Lastname; }
        }
    }
    public class MetadataExamination
    {
        [Display(Name = "Exam ID")]
        [Required]
        public int Ex_Id { get; set; }

        [Display(Name = "Exam")]
        [Required]
        public string Ex_Exam { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Ex_Date { get; set; }

        [Display(Name = "Patient")]
        [Required]
        public string Ex_Patient { get; set; }

        [Display(Name = "Employee")]
        [Required]
        public string Ex_Employee { get; set; }

        [Display(Name = "Klinik")]
        [Required]
        public string Ex_Klinik { get; set; }
    }

}