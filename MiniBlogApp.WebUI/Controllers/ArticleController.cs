using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiniBlogApp.Entities;
using MiniBlogApp.Repositories.Interfaces;
using MiniBlogApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniBlogApp.WebUI.Controllers
{
    public class ArticleController : Controller
    {

        private IRepository<Articles> _repo;
        private IWebHostEnvironment Environment;

        private readonly IConfiguration _config;
        private readonly HttpClient client;
        public ArticleController(IWebHostEnvironment _environment, IRepository<Articles> repository, IConfiguration configuration)
        {
            Environment = _environment;
            _repo = repository;

            _config = configuration;
            client = new HttpClient();
            Uri uri = new Uri(_config["apiAddress"]);
            client.BaseAddress = uri;

        }
        public IActionResult Index()
        {


            // IEnumerable<Articles> articles = _repo.GetAll().ToList();
            IEnumerable<Articles> articles = new List<Articles>();
            var response = client.GetAsync(client.BaseAddress + "/Article/GetArticle").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                articles = JsonSerializer.Deserialize<IEnumerable<Articles>>(result);



            }
            return View(articles);

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

                string GetPtag = GetFirstParagraph(model.Description);
                string GetImageTag = GetImgTag(model.Description);
                string GetLinkTag = GetAnchorTag(model.Description);

                model.Description = GetPtag;
                model.ImageUrl = GetImageTag;
                model.Url = GetLinkTag;

                string strData = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");

                var response = client.PostAsync(client.BaseAddress + "/Article/CreateArticle", content).Result;
                if (response.IsSuccessStatusCode)
                {

                    return Redirect("Index");

                }
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

        [HttpPost]
        public IActionResult Commnet(Comments model)
        {

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reply(Reply model)
        {

            return RedirectToAction("Index");
        }
        private string GetFirstParagraph(string htmltext)
        {
            Match m = Regex.Match(htmltext, @"<p>\s*(.+?)\s*</p>");
            if (m.Success)
            {
                Match mm = Regex.Match(htmltext, @"</?img((\s+\w+(\s*=\s*(?:"".*?""|\'.*?\'|[^\'"">\s]+))?)+\s*|\s*)/?>");
                if (!mm.Success)
                {
                    return m.Value;
                }
                else
                {
                    Regex reg = new Regex(@"</?img((\s+\w+(\s*=\s*(?:"".*?""|\'.*?\'|[^\'"">\s]+))?)+\s*|\s*)/?>");
                    string result = reg.Replace(htmltext, string.Empty);
                    return result;
                }

            }
            else
            {
                return null;
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
                return null;
            }
        }

        private string GetAnchorTag(string htmltext)
        {
            Match m = Regex.Match(htmltext, @"<a [^>]*?>(?<text>.*?)</a>");
            if (m.Success)
            {
                Match mm = Regex.Match(htmltext, @"</?img((\s+\w+(\s*=\s*(?:"".*?""|\'.*?\'|[^\'"">\s]+))?)+\s*|\s*)/?>");
                if (!mm.Success)
                {
                    return m.Value;
                }
                else
                {
                    Regex reg = new Regex(@"</?img((\s+\w+(\s*=\s*(?:"".*?""|\'.*?\'|[^\'"">\s]+))?)+\s*|\s*)/?>");
                    string result = reg.Replace(htmltext, string.Empty);
                    return result;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
