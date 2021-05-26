using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlogApp.WebUI.Controllers
{
    public class ArticleController : Controller
    {
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


        
    }
}
