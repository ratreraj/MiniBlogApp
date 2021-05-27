using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlogApp.WebUI.Controllers
{
    public class ArticleController : Controller
    {

        private IHostingEnvironment Environment;

        public ArticleController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(ArticleModel model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {


            }
            return Json(model);
        }
        [AcceptVerbs("Post")]
        [HttpPost]
        public JsonResult UploadFile(List<IFormFile> files)
        {
            // long size = files.Sum(f => f.Length);

            // full path to file in temp location
            //var filePath = Path.GetTempFileName();

            //foreach (var formFile in files)
            //{
            //    if (formFile.Length > 0)
            //    {
            //        using (var stream = new FileStream(filePath, FileMode.Create))
            //        {
            //            //await formFile.CopyToAsync(stream);
            //        }
            //    }
            //}
            string filepath = null;

            string stringbase64 = null;

            string extention = null;
            string ext = null;

            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine("D:\\Dot Net Tricks\\Imgae");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in files)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                    //  ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                    filepath = Path.Combine(path, fileName);
                    ext = Path.GetExtension(postedFile.FileName);
                    // filepath = "https://images.ctfassets.net/hrltx12pl8hq/3MbF54EhWUhsXunc5Keueb/60774fbbff86e6bf6776f1e17a8016b4/04-nature_721703848.jpg?fit=fill&w=480&h=270";
                }
            }


            if (ext == ".jpg")
                extention = "jpg";
            Byte[] bytes = System.IO.File.ReadAllBytes(filepath);
            filepath = "data:image/" + ext + ";base64," + Convert.ToBase64String(bytes);

            return Json(filepath);
        }
    }
}
