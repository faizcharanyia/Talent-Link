using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sp23Team09FinalProject.DAL;
using sp23Team09FinalProject.Models;

namespace sp23Team09FinalProject.Controllers
{
    public class GlobalController : Controller
    {
        private readonly AppDbContext _context;

        public GlobalController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Global
        public async Task<IActionResult> Index()
        {
              return View(await _context.CurrDate.ToListAsync());
        }


        // GET: Global/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CurrDate == null)
            {
                return NotFound();
            }

            var @global = await _context.CurrDate.FindAsync(id);
            if (@global == null)
            {
                return NotFound();
            }
            return View(@global);
        }

        // POST: Global/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CurrDate")] Global @global)
        {
            if (id != @global.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@global);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlobalExists(@global.ID))
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
            return View(@global);
        }

        

        private bool GlobalExists(int id)
        {
          return _context.CurrDate.Any(e => e.ID == id);
        }
    }
}
