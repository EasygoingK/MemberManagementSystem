using MemberManagementSystem.Models;
using MemberManagementSystem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace MemberManagementSystem.Controllers
{
    [Authorize]
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

                Session["AccountNum"] = data.AccountNum;

                return RedirectToAction("Index","Member");
            }

            return View("Login");
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["AccountNum"] = null;

            return RedirectToAction("Login");
        }

        public ActionResult Index()
        {
            var data = db.Member.ToList();

            return View(data);
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.JobID = db.MemberJob.Select(s => new { s.JobID, s.JobName}).ToList();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(HttpPostedFileBase upFile, Member inputData)
        {
            if (ModelState.IsValid)
            {
                if (upFile != null)
                {
                    string fileName = Path.Combine(Server.MapPath("~/img/"), upFile.FileName);
                    upFile.SaveAs(fileName);

                    string filePath = "/img/";
                    inputData.ImgPath = filePath + upFile.FileName.Trim();
                }

                inputData.CreateDT = DateTime.Now;
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
        public ActionResult Edit(int id, HttpPostedFileBase upFile, EditMember inputData)
        {
            if (ModelState.IsValid)
            {
                var data = db.Member.Find(id);

                if (upFile != null)
                {
                    string fileName = Path.Combine(Server.MapPath("~/img/"), upFile.FileName);
                    upFile.SaveAs(fileName);

                    string filePath = "/img/";
                    inputData.ImgPath = filePath + upFile.FileName.Trim();
                    data.ImgPath = inputData.ImgPath;
                }

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