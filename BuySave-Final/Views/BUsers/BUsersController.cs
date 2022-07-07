using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuySave_Final.Areas.Identity.Data;
using BuySave_Final.Models;

namespace BuySave_Final.Views.BUsers
{
    public class BUsersController : Controller
    {
        private readonly BuySave_FinalDbContext _context;

        public BUsersController(BuySave_FinalDbContext context)
        {
            _context = context;
        }

        // GET: BUsers
        public async Task<IActionResult> Index()
        {
            var buySave_FinalDbContext = _context.BUser.Include(b => b.Country);
            return View(await buySave_FinalDbContext.ToListAsync());
        }

        // GET: BUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BUser == null)
            {
                return NotFound();
            }

            var bUser = await _context.BUser
                .Include(b => b.Country)
                .FirstOrDefaultAsync(m => m.BUserID == id);
            if (bUser == null)
            {
                return NotFound();
            }

            return View(bUser);
        }

        // GET: BUsers/Create
        public IActionResult Create()
        {
            ViewData["CountryID"] = new SelectList(_context.Country, "CountryID", "CountryID");
            return View();
        }

        // POST: BUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BUserID,CountryID,UserName,CreatedDate")] BUser bUser)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(bUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryID"] = new SelectList(_context.Country, "CountryID", "CountryID", bUser.CountryID);
            return View(bUser);
        }

        // GET: BUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BUser == null)
            {
                return NotFound();
            }

            var bUser = await _context.BUser.FindAsync(id);
            if (bUser == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(_context.Country, "CountryID", "CountryID", bUser.CountryID);
            return View(bUser);
        }

        // POST: BUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BUserID,CountryID,UserName,CreatedDate")] BUser bUser)
        {
            if (id != bUser.BUserID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(bUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BUserExists(bUser.BUserID))
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
            ViewData["CountryID"] = new SelectList(_context.Country, "CountryID", "CountryID", bUser.CountryID);
            return View(bUser);
        }

        // GET: BUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BUser == null)
            {
                return NotFound();
            }

            var bUser = await _context.BUser
                .Include(b => b.Country)
                .FirstOrDefaultAsync(m => m.BUserID == id);
            if (bUser == null)
            {
                return NotFound();
            }

            return View(bUser);
        }

        // POST: BUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BUser == null)
            {
                return Problem("Entity set 'BuySave_FinalDbContext.BUser'  is null.");
            }
            var bUser = await _context.BUser.FindAsync(id);
            if (bUser != null)
            {
                _context.BUser.Remove(bUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BUserExists(int id)
        {
          return (_context.BUser?.Any(e => e.BUserID == id)).GetValueOrDefault();
        }
    }
}
