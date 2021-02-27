using Dapper;
using MemberManagementSystem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ReportAssign.Controllers
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
            List<Patientlist> patList = new List<Patientlist>();

            if (accession_num != null)
            {
                using (IDbConnection db = new SqlConnection(constr))
                {
                    string sql = "select * from patientlist where AccessionNum = @AccessionNum";

                    patList = db.Query<Patientlist>(sql, new { AccessionNum = accession_num }).ToList();

                    if (patList.Count == 1)
                    {
                        ViewBag.checkdata = "ok";
                    }
                    else
                    {
                        TempData["Result"] = "查無此報告，請重新輸入檢查流水號!";
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

        //下拉選單取得醫師資料
        [HttpPost]
        public ActionResult GetDocList()
        {
            List<Doclist> docList = new List<Doclist>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                string sql = "select * from doclist";

                docList = db.Query<Doclist>(sql).ToList();
            }

            return Json(docList);
        }

        //更新醫師資料
        [HttpPost]
        public ActionResult UpdateDoc(string doclist, Patientlist item)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(doclist))
                {
                    var user_name = doclist.Trim().Split('-');

                    using (IDbConnection db = new SqlConnection(constr))
                    {
                        string docCheck = "select * from doclist where DocID=@DocID ";

                        var result = db.Query(docCheck, new { DocID = user_name[0] }).ToList();

                        if (result.Count == 1)
                        {
                            var sql = "update patientlist set DoctorID = @DoctorID, DoctorName = @DoctorName " +
                                      "where AccessionNum = @AccessionNum";

                            var data = db.Execute(sql, new { DoctorID = user_name[0], DoctorName= user_name[1], AccessionNum = item.AccessionNum });

                            if (data == 1)
                            {
                                TempData["Result"] = "報告分派成功!";
                            }
                        }
                        else
                        {
                            TempData["Result"] = "查無此醫師帳號，請重新輸入!";

                        }
                    }
                }
                else
                {
                    TempData["Result"] = "未指定分派醫師，請重新輸入!";
                }

                return RedirectToAction("Index");
            }

            return View(item);

        }
    }
}