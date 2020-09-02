using MayMeow.RPG.Data;
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

        public ActiveCharacter(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        /// <summary>
        /// Return active character for current user
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // This is how you can get Logged-in user in view components
            var user = await _userManager.GetUserAsync(UserClaimsPrincipal);

            var result = _dbContext.Characters
                //.Where(m => m.IsActive == true)
                .FirstOrDefault(m => m.IsActive == true && m.OwnerId == user.Id);

            return View(result);
        }
    }
}
