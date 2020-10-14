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
using MayMeow.RPG.Data.Repositories;

namespace MayMeow.RPG.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class RacesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRacesManager _racesManager;

        public RacesController(ApplicationDbContext context, IRacesManager racesManager)
        {
            _context = context;
            _racesManager = racesManager;
        }

        // GET: Admin/Races
        public async Task<IActionResult> Index()
        {
            var allRaces = await _racesManager.All();
            return View(allRaces);
        }

        // GET: Admin/Races/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var race = await _racesManager.Details(id); 
            if (race == null)
            {
                return NotFound();
            }

            return View(race);
        }

        // GET: Admin/Races/Create
        public IActionResult Create()
        {
            ViewData["StartingLocationId"] = new SelectList(_context.Locations, "Id", "Name");
            return View();
        }

        // POST: Admin/Races/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Strength,Agility,Constitution,Intelligence,Charisma,StartingLocationId,Playable")] Race race)
        {
            if (ModelState.IsValid)
            {
                _context.Add(race);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StartingLocationId"] = new SelectList(_context.Locations, "Id", "Name", race.StartingLocationId);
            return View(race);
        }

        // GET: Admin/Races/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var race = await _context.Races.FindAsync(id);
            if (race == null)
            {
                return NotFound();
            }
            ViewData["StartingLocationId"] = new SelectList(_context.Locations, "Id", "Name", race.StartingLocationId);
            return View(race);
        }

        // POST: Admin/Races/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Strength,Agility,Constitution,Intelligence,Charisma,StartingLocationId,Playable")] Race race)
        {
            if (id != race.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(race);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaceExists(race.Id))
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
            ViewData["StartingLocationId"] = new SelectList(_context.Locations, "Id", "Name", race.StartingLocationId);
            return View(race);
        }

        // GET: Admin/Races/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var race = await _context.Races
                .Include(r => r.StartingLocation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (race == null)
            {
                return NotFound();
            }

            return View(race);
        }

        // POST: Admin/Races/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var race = await _context.Races.FindAsync(id);
            _context.Races.Remove(race);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaceExists(int id)
        {
            return _context.Races.Any(e => e.Id == id);
        }
    }
}
