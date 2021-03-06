using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.Entities;
using MiniBlogApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlogApp.WebUI.Component
{


    public class ArticleViewComponent : ViewComponent
    {
        private IRepository<Comments> _repo;
        public ArticleViewComponent(IRepository<Comments> repository)
        {
            _repo = repository;

        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IViewComponentResult> InvokeAsync(int id)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {

            
            IEnumerable<Comments> comments = (from c in _repo.GetAll().ToList().Where(x => x.ArticleId == id)
                                              select c
                                              );

           


            return View(comments);
        }


    }
}
