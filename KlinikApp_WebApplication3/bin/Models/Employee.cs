//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KlinikApp_WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Examinations = new HashSet<Examination>();
        }
    
        public int Emp_Id { get; set; }
        public string Emp_Lastname { get; set; }
        public string Emp_Firstname { get; set; }
        public System.DateTime Emp_Birthday { get; set; }
        public string Emp_Address { get; set; }
        public int Emp_Plz { get; set; }
        public int Emp_Salary { get; set; }
        public string Emp_Bundesland { get; set; }
        public string Emp_Klinik { get; set; }
        public string Emp_Pwd { get; set; }
        public virtual Bundesland Bundesland { get; set; }
        public virtual Klinik Klinik { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Examination> Examinations { get; set; }
    }
}
