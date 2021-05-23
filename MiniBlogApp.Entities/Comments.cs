using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlogApp.Entities
{
    public class Comments
    {
        public Comments()
        {

        }
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
    }
}
