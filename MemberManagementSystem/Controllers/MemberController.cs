using MemberManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberManagementSystem.Controllers
{
    public class MemberController : Controller
    {
        MemberManagementSystemEntities db = new MemberManagementSystemEntities();

        public ActionResult Index()
        {
            var data = db.Member.ToList();

            return View(data);
        }
    }
}