using MemberManagementSystem.Models;
using MemberManagementSystem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MemberManagementSystem.Controllers
{
    public class MemberController : Controller
    {
        private MemberManagementSystemEntities db = new MemberManagementSystemEntities();

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Login inpuData)
        {
            var data = db.Member.Where(w => w.AccountNum == inpuData.AccountNum && w.Password == inpuData.Password).FirstOrDefault();

            if (data != null)
            {
                FormsAuthentication.SetAuthCookie(data.AccountNum, false);
                return RedirectToAction("Index","Member");
            }

            return View("Login");
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }

        public ActionResult Index()
        {
            var data = db.Member.ToList();

            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.JobID = db.MemberJob.Select(s => new { s.JobID, s.JobName}).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Member inputData)
        {
            if (ModelState.IsValid)
            {
                db.Member.Add(inputData);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.JobID = db.MemberJob.Select(s => new { s.JobID, s.JobName }).ToList();

            return View(inputData);
        }

        public ActionResult Edit(int id)
        {
            var data = db.Member.Find(id);

            ViewBag.JobID = db.MemberJob.Select(s => new { s.JobID, s.JobName }).ToList();

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(int id, EditMember inputData)
        {
            if (ModelState.IsValid)
            {
                var data = db.Member.Find(id);

                if (data != null)
                {
                    data.Name = inputData.Name;
                    data.PhoneNum = inputData.PhoneNum;
                    data.Address = inputData.Address;
                    data.Sex = inputData.Sex;
                    data.Email = inputData.Email;
                    data.AccountNum = inputData.AccountNum;
                    data.Password = inputData.Password;
                    data.Birth = inputData.Birth;
                    data.JobID = inputData.JobID;
                    data.IdCard = inputData.IdCard;
                    data.CreateDT = DateTime.Now;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            var dataCheck = db.Member.Find(id);

            return View(dataCheck);
        }

        public ActionResult Delete(int id)
        {
            var data = db.Member.Find(id);

            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(int? id, FormCollection form)
        {
            var data = db.Member.Find(id);

            if (id != null)
            {
                db.Member.Remove(data);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = db.Member.Find(id);



            return View(data);
        }
    }
}