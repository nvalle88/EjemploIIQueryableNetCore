using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EjemploIIQueryableNetCore.Data;
using EjemploIIQueryableNetCore.Models.Negocio;

namespace EjemploIIQueryableNetCore.Controllers
{
    public class EstudianteAsignaturasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstudianteAsignaturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EstudianteAsignaturas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EstudianteAsignatura.Include(e => e.Asignatura).Include(e => e.Estudiante);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EstudianteAsignaturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteAsignatura = await _context.EstudianteAsignatura
                .Include(e => e.Asignatura)
                .Include(e => e.Estudiante)
                .SingleOrDefaultAsync(m => m.IdEstudianteAsignatura == id);
            if (estudianteAsignatura == null)
            {
                return NotFound();
            }

            return View(estudianteAsignatura);
        }

        // GET: EstudianteAsignaturas/Create
        public IActionResult Create()
        {
            ViewData["IdAsignatura"] = new SelectList(_context.Asignatura, "IdAsignatura", "IdAsignatura");
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiante, "IdEstudiante", "IdEstudiante");
            return View();
        }

        // POST: EstudianteAsignaturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstudianteAsignatura,Nota,IdEstudiante,IdAsignatura")] EstudianteAsignatura estudianteAsignatura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudianteAsignatura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAsignatura"] = new SelectList(_context.Asignatura, "IdAsignatura", "IdAsignatura", estudianteAsignatura.IdAsignatura);
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiante, "IdEstudiante", "IdEstudiante", estudianteAsignatura.IdEstudiante);
            return View(estudianteAsignatura);
        }

        // GET: EstudianteAsignaturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteAsignatura = await _context.EstudianteAsignatura.SingleOrDefaultAsync(m => m.IdEstudianteAsignatura == id);
            if (estudianteAsignatura == null)
            {
                return NotFound();
            }
            ViewData["IdAsignatura"] = new SelectList(_context.Asignatura, "IdAsignatura", "IdAsignatura", estudianteAsignatura.IdAsignatura);
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiante, "IdEstudiante", "IdEstudiante", estudianteAsignatura.IdEstudiante);
            return View(estudianteAsignatura);
        }

        // POST: EstudianteAsignaturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstudianteAsignatura,Nota,IdEstudiante,IdAsignatura")] EstudianteAsignatura estudianteAsignatura)
        {
            if (id != estudianteAsignatura.IdEstudianteAsignatura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudianteAsignatura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteAsignaturaExists(estudianteAsignatura.IdEstudianteAsignatura))
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
            ViewData["IdAsignatura"] = new SelectList(_context.Asignatura, "IdAsignatura", "IdAsignatura", estudianteAsignatura.IdAsignatura);
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiante, "IdEstudiante", "IdEstudiante", estudianteAsignatura.IdEstudiante);
            return View(estudianteAsignatura);
        }

        // GET: EstudianteAsignaturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteAsignatura = await _context.EstudianteAsignatura
                .Include(e => e.Asignatura)
                .Include(e => e.Estudiante)
                .SingleOrDefaultAsync(m => m.IdEstudianteAsignatura == id);
            if (estudianteAsignatura == null)
            {
                return NotFound();
            }

            return View(estudianteAsignatura);
        }

        // POST: EstudianteAsignaturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudianteAsignatura = await _context.EstudianteAsignatura.SingleOrDefaultAsync(m => m.IdEstudianteAsignatura == id);
            _context.EstudianteAsignatura.Remove(estudianteAsignatura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteAsignaturaExists(int id)
        {
            return _context.EstudianteAsignatura.Any(e => e.IdEstudianteAsignatura == id);
        }
    }
}
