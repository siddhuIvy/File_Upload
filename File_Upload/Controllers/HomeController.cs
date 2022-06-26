using File_Upload.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace File_Upload.Controllers
{
    public class HomeController : Controller
    {
        FileUploadEntities db = new FileUploadEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult addnewrecord()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult addnewrecord(tbl_data d,HttpPostedFileBase anyfile)
        {
            tbl_data di = new tbl_data();
            string path = uploadimage(anyfile);
            if (path.Equals("-1"))
            {

            }
            else
            {
                di.Name = d.Name;
                di.Description = d.Description;
                di.Files = path;
                db.tbl_data.Add(di);
                db.SaveChanges();
                ViewBag.msg = "Upload Successfull";

            }

            return View();
        }

        public string uploadimage(HttpPostedFileBase file)
        {
            Random r = new Random();

            string path = "-1";

            int random = r.Next();

            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".mp3") || extension.ToLower().Equals(".pdf") || extension.ToLower().Equals(".txt")
                    || extension.ToLower().Equals(".mp4") || extension.ToLower().Equals(".xls") || extension.ToLower().Equals(".png") || extension.ToLower().Equals(".xml")
                )
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);
                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg,txt,docx,pdf or png formats are acceptable....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }
            return path;
        }
        public ActionResult view(int id)
        {
            tbl_data anyfile = new tbl_data();
            using (FileUploadEntities db = new FileUploadEntities())
            {
                anyfile = db.tbl_data.Where(X => X.Id == id).FirstOrDefault();
            }
            return View(anyfile);
        }

        public ActionResult List()
        {
            return View(db.tbl_data.ToList());
        }
    }
}