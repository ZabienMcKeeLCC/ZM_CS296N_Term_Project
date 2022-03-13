using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZM_CS296N_TermProject.Models.DomainModels;

namespace ZM_CS296N_TermProject.Models.DomainModels
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public AppUser User { get; set; }
        [StringLength (120, MinimumLength = 3)]
        public string Title { get; set; }
        public string Message { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
        public string Date { get; set; }
        public List<Comment> Comments { get; set; }

        public List<Comment> GetComments()
        {
            if (Comments != null)
            {
                return Comments;
            }
            return new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }
    }
}
