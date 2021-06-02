using Microsoft.AspNetCore.Mvc;
using MiniBlogApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlogApp.WebUI.Components
{
    public class CommentViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(Comments modele)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {



            return View(modele);
        }
    }
}
