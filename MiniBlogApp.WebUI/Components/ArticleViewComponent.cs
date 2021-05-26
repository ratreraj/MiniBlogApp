using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlogApp.WebUI.Component
{

    public class RichTextEditorModel
    {
        [Required(ErrorMessage = "Value is required")]
        public string Value { get; set; }
    }
    public class ArticleViewComponent :ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {

            RichTextEditorModel rteModel = new RichTextEditorModel();
            rteModel.Value = "<p>Type or edit the content to post the <b>Rich Text Editor</b> value.</p>";

            return View(rteModel); 
        }
    }
}
