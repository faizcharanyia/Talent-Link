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
    public class MajorController : Controller
    {
        private readonly AppDbContext _context;

        public MajorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Major
        public async Task<IActionResult> Index()
        {
              return View(await _context.Majors.ToListAsync());
        }

        // GET: Major/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Majors == null)
            {
                return NotFound();
            }

            Major major = await _context.Majors
                .Include(d => d.Positions)
                .FirstOrDefaultAsync(m => m.MajorID == id);
            if (major == null)
            {
                return NotFound();
            }

            return View(major);
        }

        // GET: Major/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Major/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MajorID,MajorName")] Major major)
        {
            if (ModelState.IsValid)
            {
                _context.Add(major);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(major);
        }

        // GET: Major/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Majors == null)
            {
                return NotFound();
            }

            var major = await _context.Majors.FindAsync(id);
            if (major == null)
            {
                return NotFound();
            }
            return View(major);
        }

        // POST: Major/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MajorID,MajorName")] Major major)
        {
            if (id != major.MajorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(major);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MajorExists(major.MajorID))
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
            return View(major);
        }

        // GET: Major/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Majors == null)
            {
                return NotFound();
            }

            var major = await _context.Majors
                .FirstOrDefaultAsync(m => m.MajorID == id);
            if (major == null)
            {
                return NotFound();
            }

            return View(major);
        }

        // POST: Major/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Majors == null)
            {
                return Problem("Entity set 'AppDbContext.Majors'  is null.");
            }
            var major = await _context.Majors.FindAsync(id);
            if (major != null)
            {
                _context.Majors.Remove(major);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MajorExists(int id)
        {
          return _context.Majors.Any(e => e.MajorID == id);
        }
    }
}
