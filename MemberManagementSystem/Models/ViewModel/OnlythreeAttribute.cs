using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace MemberManagementSystem.Models.ViewModel
{
    public class OnlythreeAttribute : DataTypeAttribute
    {
        private MemberManagementSystemEntities db = new MemberManagementSystemEntities();

        public OnlythreeAttribute() : base(DataType.Text)
        {
            ErrorMessage = "一組身分證字號僅能申請三組帳號!";
        }

        public override bool IsValid(object value)
        {
            string data = Convert.ToString(value);

            var result = db.Member.Where(w => w.IdCard == data).ToList();

            if (result.Count == 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}