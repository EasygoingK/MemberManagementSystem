using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MemberManagementSystem.Models
{
    [MetadataType(typeof(Partial_MemberJobMedaData))]
    public partial class MemberJob
    {
        public class Partial_MemberJobMedaData
        {
            public int JobID { get; set; }

            [Display(Name = "工作產業")]
            public string JobName { get; set; }

            public string JobAddress { get; set; }
        }
    }
}