using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZM_CS296N_TermProject.Models.DomainModels;

namespace ZM_CS296N_TermProject.Models.ViewModels
{
    public class CommentVM
    {
        public int ReviewId { get; set; }
        public AppUser User { get; set; }
        public string Message { get; set; }

    }
}
