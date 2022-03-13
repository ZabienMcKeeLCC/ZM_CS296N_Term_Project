using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296N_TermProject.Models.DomainModels
{
    public class Collection
    {
        public AppUser User { get; set; }
        public List<Game> GameList { get; set; }
    }
}
