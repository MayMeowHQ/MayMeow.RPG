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

namespace MayMeow.RPG.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ConnectedLocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConnectedLocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ConnectedLocations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ConnectedLocations.Include(c => c.Child).Include(c => c.Parent);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/ConnectedLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connectedLocation = await _context.ConnectedLocations
                .Include(c => c.Child)
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (connectedLocation == null)
            {
                return NotFound();
            }

            return View(connectedLocation);
        }

        // GET: Admin/ConnectedLocations/Create
        public IActionResult Create()
        {
            ViewData["ChildId"] = new SelectList(_context.Locations, "Id", "Id");
            ViewData["ParentId"] = new SelectList(_context.Locations, "Id", "Id");
            return View();
        }

        // POST: Admin/ConnectedLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentId,ChildId")] ConnectedLocation connectedLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(connectedLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChildId"] = new SelectList(_context.Locations, "Id", "Id", connectedLocation.ChildId);
            ViewData["ParentId"] = new SelectList(_context.Locations, "Id", "Id", connectedLocation.ParentId);
            return View(connectedLocation);
        }

        // GET: Admin/ConnectedLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connectedLocation = await _context.ConnectedLocations.FindAsync(id);
            if (connectedLocation == null)
            {
                return NotFound();
            }
            ViewData["ChildId"] = new SelectList(_context.Locations, "Id", "Id", connectedLocation.ChildId);
            ViewData["ParentId"] = new SelectList(_context.Locations, "Id", "Id", connectedLocation.ParentId);
            return View(connectedLocation);
        }

        // POST: Admin/ConnectedLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentId,ChildId")] ConnectedLocation connectedLocation)
        {
            if (id != connectedLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(connectedLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConnectedLocationExists(connectedLocation.Id))
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
            ViewData["ChildId"] = new SelectList(_context.Locations, "Id", "Id", connectedLocation.ChildId);
            ViewData["ParentId"] = new SelectList(_context.Locations, "Id", "Id", connectedLocation.ParentId);
            return View(connectedLocation);
        }

        // GET: Admin/ConnectedLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connectedLocation = await _context.ConnectedLocations
                .Include(c => c.Child)
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (connectedLocation == null)
            {
                return NotFound();
            }

            return View(connectedLocation);
        }

        // POST: Admin/ConnectedLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var connectedLocation = await _context.ConnectedLocations.FindAsync(id);
            _context.ConnectedLocations.Remove(connectedLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConnectedLocationExists(int id)
        {
            return _context.ConnectedLocations.Any(e => e.Id == id);
        }
    }
}
