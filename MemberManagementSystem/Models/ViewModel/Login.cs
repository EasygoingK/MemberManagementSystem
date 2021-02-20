using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MemberManagementSystem.Models.ViewModel
{
    public class Login
    {
        [Display(Name = "帳號")]
        public string AccountNum { get; set; }

        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}