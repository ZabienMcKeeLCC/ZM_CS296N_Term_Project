using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296N_TermProject.Models.DomainModels
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ReviewId { get; set; }
        public AppUser User { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }

    }
}
