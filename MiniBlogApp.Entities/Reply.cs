using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlogApp.Entities
{
    public class Reply
    {
        public Reply()
        {

        }
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
    }
}
