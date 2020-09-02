using MayMeow.RPG.Entities.World;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MayMeow.RPG.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public List<Character> Characters { get; set; }
    }
}
