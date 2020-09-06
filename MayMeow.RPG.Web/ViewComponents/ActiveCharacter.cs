using MayMeow.RPG.Data;
using MayMeow.RPG.Data.Repositories;
using MayMeow.RPG.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayMeow.RPG.Web.ViewComponents
{
    public class ActiveCharacter : ViewComponent
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICharacterManager characterManager;

        public ActiveCharacter(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ICharacterManager characterManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            this.characterManager = characterManager;
        }

        /// <summary>
        /// Return active character for current user
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // This is how you can get Logged-in user in view components
            var user = await _userManager.GetUserAsync(UserClaimsPrincipal);
            var result = await this.characterManager.GetActiveCharacter(user);

            return View(result);
        }
    }
}
