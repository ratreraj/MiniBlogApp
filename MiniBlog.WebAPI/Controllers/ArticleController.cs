using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {


        [HttpPost]
        public IActionResult CreateArticle([FromBody] ArticleModel model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {


            }

            return StatusCode(StatusCodes.Status200OK, model);
        }

    }
}
