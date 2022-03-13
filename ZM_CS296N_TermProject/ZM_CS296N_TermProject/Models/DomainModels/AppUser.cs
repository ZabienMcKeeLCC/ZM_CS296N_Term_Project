using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296N_TermProject.Models.DomainModels
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
