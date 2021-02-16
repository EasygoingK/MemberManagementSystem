using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MemberManagementSystem.Models.ViewModel
{
    public class EditMember
    {
        public int Id { get; set; }

        [Display(Name="姓名")]
        [Required]
        public string Name { get; set; }

        [Display(Name="電話")]
        [Required]
        public string PhoneNum { get; set; }

        [Display(Name="地址")]
        public string Address { get; set; }

        [Display(Name="性別")]
        public string Sex { get; set; }

        [Display(Name="電子郵件")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name="帳號")]
        [Required]
        public string AccountNum { get; set; }

        [Display(Name="密碼")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="生日")]
        [DataType(DataType.Date)]
        public string Birth { get; set; }

        public Nullable<int> JobID { get; set; }

        [Display(Name="工作產業")]
        public string JobName { get; set; }

        [Display(Name="身分證字號")]
        [Required]
        public string IdCard { get; set; }
    }
}