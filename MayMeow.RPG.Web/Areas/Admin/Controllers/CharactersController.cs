using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MayMeow.RPG.Data;
using MayMeow.RPG.Entities.World;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MayMeow.RPG.Entities.Identity;
using MayMeow.RPG.Data.Repositories;

namespace MayMeow.RPG.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICharacterManager _characterManager;

        public CharactersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICharacterManager characterManager)
        {
            _context = context;
            _userManager = userManager;
            _characterManager = characterManager;
        }

        // GET: Admin/Characters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Characters.Include(c => c.Owner).Include(c => c.Race);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Characters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .Include(c => c.Owner)
                .Include(c => c.Race)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Admin/Characters/Create
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["RaceId"] = new SelectList(_context.Races, "Id", "Name");
            return View();
        }

        // POST: Admin/Characters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender,OwnerId,CreatedAt,UpdatedAt,RaceId")] Character character)
        {
            // TODO move to User's Character controller
            // var user = await _userManager.GetUserAsync(User);
            // character.OwnerId = user.Id;

            character = await _characterManager.Prepare(character, true);

            if (ModelState.IsValid)
            {
                _context.Add(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", character.OwnerId);
            ViewData["RaceId"] = new SelectList(_context.Races, "Id", "Name", character.RaceId);
            return View(character);
        }

        // GET: Admin/Characters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", character.OwnerId);
            ViewData["RaceId"] = new SelectList(_context.Races, "Id", "Id", character.RaceId);
            return View(character);
        }

        // POST: Admin/Characters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,Experience,Money,Strength,Agility,Constitution,Intelligence,Charisma,HitPoints,TotalHitPoints,OwnerId,CreatedAt,UpdatedAt,RaceId")] Character character)
        {
            if (id != character.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(character);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", character.OwnerId);
            ViewData["RaceId"] = new SelectList(_context.Races, "Id", "Id", character.RaceId);
            return View(character);
        }

        // GET: Admin/Characters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .Include(c => c.Owner)
                .Include(c => c.Race)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Admin/Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }

        public async Task<IActionResult> SetActive(int id)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(m => m.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            await _characterManager.SetAsActive(character, character.OwnerId);
            return RedirectToAction(nameof(Index));
        }
    }
}
