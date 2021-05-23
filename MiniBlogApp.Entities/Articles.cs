using System;

namespace MiniBlogApp.Entities
{
    public class Articles
    {
        public Articles()
        {

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }


    }
}
