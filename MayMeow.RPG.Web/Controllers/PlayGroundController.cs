using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MayMeow.RPG.Data.Repositories;
using MayMeow.RPG.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MayMeow.RPG.Web.Controllers
{
    [Authorize]
    public class PlayGroundController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICharacterManager characterManager;

        public PlayGroundController(UserManager<ApplicationUser> userManager, ICharacterManager characterManager)
        {
            this.userManager = userManager;
            this.characterManager = characterManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(User);
            var character = this.characterManager.GetActiveCharacter(user);

            return View();
        }
    }
}
