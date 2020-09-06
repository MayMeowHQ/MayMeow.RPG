using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MayMeow.RPG.Data;
using MayMeow.RPG.Data.Repositories;
using MayMeow.RPG.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MayMeow.RPG.Web.Controllers
{
    [Authorize]
    public class PlayGroundController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICharacterManager characterManager;
        private readonly ApplicationDbContext dbContext;

        public PlayGroundController(UserManager<ApplicationUser> userManager, ICharacterManager characterManager, ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.characterManager = characterManager;
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(User);
            var character = await this.characterManager.GetActiveCharacter(user);
            var location = await this.characterManager.GetCurrentLocation(character);

            return View(location);
        }

        public async Task<IActionResult> MoveTo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await this.userManager.GetUserAsync(User);
            var character = await this.characterManager.GetActiveCharacter(user);

            var location = await this.dbContext.Locations
                .FirstOrDefaultAsync(c => c.Id == id);

            await this.characterManager.MoveTo(character, location.Id);

            return RedirectToAction(nameof(Index));
        }
    }
}
