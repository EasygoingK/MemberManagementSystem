using Dapper;
using MemberManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MemberManagementSystem.Controllers
{
    public class AssignController : Controller
    {
        private static readonly string constr = ConfigurationManager.ConnectionStrings["ReportDB"].ConnectionString;

        public ActionResult Index()
        {
            return View();
        }

        //查詢資料
        [HttpPost]
        public ActionResult QueryReport(string accession_num)
        {
            List<PatientList> patList = new List<PatientList>();

            if (accession_num != null)
            {
                using (IDbConnection db = new SqlConnection(constr))
                {
                    string sql = "SELECT PatientID, PatientName, AccessionNum, DoctorID, DoctorName FROM patientlist Where AccessionNum = @AccessionNum";

                    patList = db.Query<PatientList>(sql, new { AccessionNum = accession_num }).ToList();

                    if (patList.Count >= 1)
                    {
                        ViewBag.checkdata = "ok";
                    }

                    ViewBag.Test = new SelectList(patList, "AccessionNum", "PatientName");

                    return View("Index", patList);
                }
            }
            else
            {
                return View("Index");
            }

        }

        //新增資料
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PatientList data)
        {
            if (ModelState.IsValid)
            {
                using (IDbConnection db = new SqlConnection(constr))
                {
                    string sql = "Insert into patientlist (PatientID, PatientName, AccessionNum) Values (@PatientID, @PatientName, @AccessionNum)";

                    var result = db.Execute(sql, new { data.PatientID, data.PatientName, data.AccessionNum });

                    if (result >= 1)
                    {
                        TempData["Result"] = "資料新增成功!";
                    }
                }
                return RedirectToAction("Index");
            }

            return View(data);
        }

        //編輯資料
        public ActionResult Edit(string accession_num)
        {
            var data = new PatientList();

            using (IDbConnection db = new SqlConnection(constr))
            {
                var sql = "SELECT PatientID, PatientName, AccessionNum, DoctorID, DoctorName " +
                                   "FROM patientlist Where AccessionNum = @AccessionNum";

                data = db.Query<PatientList>(sql, new { AccessionNum = accession_num }).ToList().FirstOrDefault();
            }

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(PatientList data)
        {
            if (ModelState.IsValid)
            {
                using (IDbConnection db = new SqlConnection(constr))
                {
                    string sql = "update patientlist  set PatientID = @PatientID, PatientName = @PatientName where AccessionNum = @AccessionNum";

                    var result = db.Execute(sql, new { data.PatientID, data.PatientName, data.AccessionNum });

                    if (result >= 1)
                    {
                        TempData["Result"] = "資料更新成功!";
                    }
                }

                return RedirectToAction("Index");
            }

            return View(data);
        }

        //刪除資料
        public ActionResult Delete(string accession_num)
        {
            var data = new PatientList();

            using (IDbConnection db = new SqlConnection(constr))
            {
                string sql = "SELECT PatientID, PatientName, AccessionNum, DoctorID, DoctorName " +
                "FROM patientlist Where AccessionNum = @AccessionNum";

                data = db.Query<PatientList>(sql, new { AccessionNum = accession_num }).ToList().FirstOrDefault();
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PatientList data)
        {
            if (ModelState.IsValid)
            {
                using (IDbConnection db = new SqlConnection(constr))
                {
                    string sql = "delete patientlist where AccessionNum = @AccessionNum";

                    var result = db.Execute(sql, new { data.AccessionNum });

                    if (result >= 1)
                    {
                        TempData["Result"] = "資料刪除成功!";
                    }
                }

                return RedirectToAction("Index");
            }

            return View(data);
        }

        //下拉選單取得醫師資料
        [HttpPost]
        public ActionResult GetDocList()
        {
            List<DocoterList> docList = new List<DocoterList>();

            using (IDbConnection db = new SqlConnection(constr))
            {

                var sql = "select DocID,DocName from doclist";

                docList = db.Query<DocoterList>(sql).ToList();
            }

            return Json(docList);
        }

        [HttpPost]
        public ActionResult UpdateDoc(string doclist, PatientList item)
        {
            if (ModelState.IsValid)
            {
                var dicId = doclist.Split('-');

                using (IDbConnection db = new SqlConnection(constr))
                {
                    var sql = "  update patientlist set DoctorID = @DoctorID, DoctorName = @DoctorName where AccessionNum = @AccessionNum";

                    var data = db.Execute(sql, new { DoctorID = dicId[0], DoctorName = dicId[1], AccessionNum = item.AccessionNum });

                    if (data >= 1)
                    {
                        TempData["Result"] = "資料更新成功!";
                    }
                }

                return RedirectToAction("Index");
            }

            return View(item);
        }
    }
}