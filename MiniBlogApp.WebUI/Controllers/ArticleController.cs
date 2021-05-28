using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniBlogApp.WebUI.Controllers
{
    public class ArticleController : Controller
    {

        private IWebHostEnvironment Environment;


        public ArticleController(IWebHostEnvironment _environment)
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
        public IActionResult Create(ArticleModel model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {

                //string text = "No image <img alt='sss' src='something' /> asdasd";
                //Regex reg = new Regex(@"</?img((\s+\w+(\s*=\s*(?:"".*?""|\'.*?\'|[^\'"">\s]+))?)+\s*|\s*)/?>");
                //string result = reg.Replace(model.Description, string.Empty);

                //var regex = new Regex("<a [^>]*href=(?:'(?<href>.*?)')|(?:\"(?<href>.*?)\")", RegexOptions.IgnoreCase);
                //var urls = regex.Matches(model.Description).OfType<Match>().Select(m => m.Groups["href"].Value).SingleOrDefault();

                string GetPtag = GetFirstParagraph(model.Description);
                string GetImageTag = GetImgTag(model.Description);
                string GetLinkTag = GetAnchorTag(model.Description);

                //Regex regex = new Regex("<a [^>]*href=(?:'(?<href>.*?)')|(?:\"(?<href>.*?)\")", RegexOptions.IgnoreCase);
                //Match match;
                //for (match = regex.Match(model.Description); match.Success; match = match.NextMatch())
                //{

                //    foreach (Group group in match.Groups)
                //    {
                //        var url = group;
                //    }
                //}

            }
            return View();
        }
        [AcceptVerbs("Post")]
        [HttpPost]
        public JsonResult UploadFile(List<IFormFile> files)
        {

            string filepath = null;

            //string stringbase64 = null;


            string ext = null;

            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.ContentRootPath, "Upload");
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
            //string extention = string.Empty;
            //if (ext == ".jpg")
            //    extention = "jpg";
            Byte[] bytes = System.IO.File.ReadAllBytes(filepath);
            filepath = "data:image/" + ext + ";base64," + Convert.ToBase64String(bytes);



            return Json(filepath);
        }


        private string GetFirstParagraph(string htmltext)
        {
            Match m = Regex.Match(htmltext, @"<p>\s*(.+?)\s*</p>");
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                return htmltext;
            }
        }


        private string GetImgTag(string htmltext)
        {
            Match m = Regex.Match(htmltext, @"</?img((\s+\w+(\s*=\s*(?:"".*?""|\'.*?\'|[^\'"">\s]+))?)+\s*|\s*)/?>");
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                return htmltext;
            }
        }

        private string GetAnchorTag(string htmltext)
        {
            Match m = Regex.Match(htmltext, @"<a [^>]*?>(?<text>.*?)</a>");
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                return htmltext;
            }
        }
    }
}
