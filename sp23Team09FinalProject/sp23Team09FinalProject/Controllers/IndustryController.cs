using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sp23Team09FinalProject.DAL;
using sp23Team09FinalProject.Models;

namespace sp23Team09FinalProject
{
    [Authorize(Roles = "CSO")]
    public class IndustryController : Controller
    {
        private readonly AppDbContext _context;

        public IndustryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Industry
        public async Task<IActionResult> Index()
        {
              return View(await _context.Industrys.ToListAsync());
        }

        // GET: Industry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Industrys == null)
            {
                return NotFound();
            }

            var industry = await _context.Industrys
                .FirstOrDefaultAsync(m => m.IndustryID == id);
            if (industry == null)
            {
                return NotFound();
            }

            return View(industry);
        }

        // GET: Industry/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Industry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IndustryID,IndustryName")] Industry industry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(industry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(industry);
        }

        // GET: Industry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Industrys == null)
            {
                return NotFound();
            }

            var industry = await _context.Industrys.FindAsync(id);
            if (industry == null)
            {
                return NotFound();
            }
            return View(industry);
        }

        // POST: Industry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IndustryID,IndustryName")] Industry industry)
        {
            if (id != industry.IndustryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(industry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndustryExists(industry.IndustryID))
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
            return View(industry);
        }

        // GET: Industry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Industrys == null)
            {
                return NotFound();
            }

            var industry = await _context.Industrys
                .FirstOrDefaultAsync(m => m.IndustryID == id);
            if (industry == null)
            {
                return NotFound();
            }

            return View(industry);
        }

        // POST: Industry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Industrys == null)
            {
                return Problem("Entity set 'AppDbContext.Industrys'  is null.");
            }
            var industry = await _context.Industrys.FindAsync(id);
            if (industry != null)
            {
                _context.Industrys.Remove(industry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndustryExists(int id)
        {
          return _context.Industrys.Any(e => e.IndustryID == id);
        }
    }
}
