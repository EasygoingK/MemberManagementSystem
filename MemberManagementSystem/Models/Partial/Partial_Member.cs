﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MemberManagementSystem.Models
{
    [MetadataType(typeof(Partial_MemberMedaData))]
    public partial class Member
    {
        public class Partial_MemberMedaData
        {
            [Display(Name = "姓名")]
            public string Name { get; set; }

            [Display(Name = "電話")]
            public string PhoneNum { get; set; }

            [Display(Name = "地址")]
            public string Address { get; set; }

            [Display(Name = "性別")]
            public string Sex { get; set; }

            [Display(Name = "電子郵件")]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "帳號")]
            public string AccountNum { get; set; }

            [Display(Name = "密碼")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "生日")]
            [DataType(DataType.Date)]
            public string Birth { get; set; }

            [Display(Name = "身分證字號")]
            public string IdCard { get; set; }
        }
    }
}