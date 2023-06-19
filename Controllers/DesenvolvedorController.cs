using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevSkills.Models;

namespace DevSkills.Controllers
{
    public class DesenvolvedorController : Controller
    {
        private readonly DevSkillsDBContext _context;

        public DesenvolvedorController(DevSkillsDBContext context)
        {
            _context = context;
        }

        // GET: Desenvolvedor
        public async Task<IActionResult> Index()
        {
              return _context.Desenvolvedor != null ? 
                          View(await _context.Desenvolvedor.ToListAsync()) :
                          Problem("Entity set 'DevSkillsDBContext.Desenvolvedor'  is null.");
        }

        // GET: Desenvolvedor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Desenvolvedor == null)
            {
                return NotFound();
            }

            var desenvolvedor = await _context.Desenvolvedor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desenvolvedor == null)
            {
                return NotFound();
            }

            return View(desenvolvedor);
        }

        // GET: Desenvolvedor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Desenvolvedor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email")] Desenvolvedor desenvolvedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(desenvolvedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(desenvolvedor);
        }

        // GET: Desenvolvedor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Desenvolvedor == null)
            {
                return NotFound();
            }

            var desenvolvedor = await _context.Desenvolvedor.FindAsync(id);
            if (desenvolvedor == null)
            {
                return NotFound();
            }
            return View(desenvolvedor);
        }

        // POST: Desenvolvedor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email")] Desenvolvedor desenvolvedor)
        {
            if (id != desenvolvedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(desenvolvedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesenvolvedorExists(desenvolvedor.Id))
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
            return View(desenvolvedor);
        }

        // GET: Desenvolvedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Desenvolvedor == null)
            {
                return NotFound();
            }

            var desenvolvedor = await _context.Desenvolvedor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desenvolvedor == null)
            {
                return NotFound();
            }

            return View(desenvolvedor);
        }

        // POST: Desenvolvedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Desenvolvedor == null)
            {
                return Problem("Entity set 'DevSkillsDBContext.Desenvolvedor'  is null.");
            }
            var desenvolvedor = await _context.Desenvolvedor.FindAsync(id);
            if (desenvolvedor != null)
            {
                _context.Desenvolvedor.Remove(desenvolvedor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesenvolvedorExists(int id)
        {
          return (_context.Desenvolvedor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
