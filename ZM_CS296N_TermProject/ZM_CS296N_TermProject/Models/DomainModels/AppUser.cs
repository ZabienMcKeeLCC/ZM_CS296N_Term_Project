using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296N_TermProject.Models.DomainModels
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        [StringLength(50)]
        public string Bio { get; set; }

        [NotMapped]
        public IList<string> RoleNames { get; set; }
        public override string ToString()
        {
            return this.UserName;
        }
    }
}
