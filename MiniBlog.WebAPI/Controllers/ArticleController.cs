using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.Entities;
using MiniBlogApp.Repositories.Interfaces;
using MiniBlogApp.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private IRepository<Articles> _repo;
        public ArticleController(IRepository<Articles> repository)
        {
            _repo = repository;
        }


        [HttpPost]
        public IActionResult CreateArticle([FromBody] ArticleModel model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                Articles articles = new()
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    Url = model.Url


                };
                _repo.Add(articles);
                _repo.SaveChange();
            }

            return StatusCode(StatusCodes.Status200OK);
        }


        [HttpGet]
        public IActionResult GetArticle()
        {
            try
            {
                IEnumerable<Articles> articles = _repo.GetAll().ToList();

                return StatusCode(StatusCodes.Status200OK, articles);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

    }
}
