using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace MVC5.Controllers
{
    public class FileUploadController : Controller
    {
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                var baseFolderPath = GetDirectoryPath();
                file.SaveAs(baseFolderPath + file.FileName);
                return Json(new { Message = "Success" });
            }
            catch (Exception)
            {
                return Json(new { Message = "Failed" });
            } 
        }

        [HttpGet]
        public JsonResult GetExistingFiles()
        {
            var directoryPath = GetDirectoryPath();
            var files = Directory.GetFiles(directoryPath);

            return Json(files, JsonRequestBehavior.AllowGet);
        }

        private string GetDirectoryPath()
        {
            var directoryPath = Server.MapPath("~/FileStorage\\");
            return directoryPath;
        }

        [HttpPost]
        public JsonResult DeleteFile(FileInput file)
        {
            try
            {
                var fileToDelete = file.fileName;
                if ((System.IO.File.Exists(file.fileName)))
                {
                    System.IO.File.Delete(file.fileName);
                }
                
                return Json(new { Message = "Success" });
            }
            catch (Exception)
            {
                return Json(new { Message = "Failed" });
            }
        }
    }

    public class FileInput
    {
        public string fileName { get; set; }
    }


}