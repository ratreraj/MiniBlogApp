using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.Entities;
using MiniBlogApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlogApp.WebUI.Components
{
    public class ReplyViewComponent : ViewComponent
    {

        private IRepository<Reply> _repo;
        public ReplyViewComponent(IRepository<Reply> repository)
        {
            _repo = repository;

        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IViewComponentResult> InvokeAsync(int id)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {

            IEnumerable<Reply> replies = (from c in _repo.GetAll().ToList().Where(x => x.CommentId == id)
                                          select c
                                              );

            return View(replies);
        }
    }
}
