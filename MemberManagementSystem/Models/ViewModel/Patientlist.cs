using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MemberManagementSystem.Models.ViewModel
{
    public class Patientlist
    {
        [Display(Name = "病歷號")]
        public string PatientID { get; set; }
        [Display(Name = "姓名")]
        public string PatientName { get; set; }
        [Display(Name = "檢查流水號")]
        public string AccessionNum { get; set; }
        [Display(Name = "醫師帳號")]
        public string DoctorID { get; set; }
        [Display(Name = "醫師姓名")]
        public string DoctorName { get; set; }

    }
}