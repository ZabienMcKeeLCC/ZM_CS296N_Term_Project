using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296N_TermProject.Models.DomainModels
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        public string ReleaseYear { get; set; }
        public string Title { get; set; }
        public string Condition { get; set; }
    }
}
