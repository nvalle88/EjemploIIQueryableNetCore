using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EjemploIIQueryableNetCore.Data;
using EjemploIIQueryableNetCore.Models.Negocio;
using System.Linq.Expressions;

namespace EjemploIIQueryableNetCore.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly ApplicationDbContext db;

        public EstudiantesController(ApplicationDbContext context)
        {
            db = context;
        }


        private  IQueryable<Estudiante> Estudiantes()
        {
           return db.Estudiante.Include(x=>x.EstudianteAsignatura);
        }

        private IQueryable<EstudianteAsignatura> Asignaturas(Estudiante estudiante)
        {
            return db.EstudianteAsignatura.Include(x =>x.Asignatura).Where(x=>x.IdEstudiante==estudiante.IdEstudiante);
        }
        // GET: Estudiantes

        public async Task<IActionResult> Index()
        {
            return View(await ListarEstudiantes(nombre: x=>x.Nombre.Contains("pepe")));
        }
        private async Task<List<Estudiante>> ListarEstudiantes(Expression<Func<Estudiante, bool>> nombre = null, Expression<Func<EstudianteAsignatura, bool>> nota = null)
        {
           // var lista =new List<EstudianteAsignatura>();
            //var estudiantesAprobados = Estudiantes().ForEachAsync(x =>lista.Add(Asignaturas(x).FirstOrDefault()));
            return (await (nombre != null ? Estudiantes().Where(nombre).ToListAsync():Estudiantes().ToListAsync()));
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await db.Estudiante
                .SingleOrDefaultAsync(m => m.IdEstudiante == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstudiante,Nombre")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                db.Add(estudiante);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await db.Estudiante.SingleOrDefaultAsync(m => m.IdEstudiante == id);
            if (estudiante == null)
            {
                return NotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstudiante,Nombre")] Estudiante estudiante)
        {
            if (id != estudiante.IdEstudiante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(estudiante);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.IdEstudiante))
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
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await db.Estudiante
                .SingleOrDefaultAsync(m => m.IdEstudiante == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = await db.Estudiante.SingleOrDefaultAsync(m => m.IdEstudiante == id);
            db.Estudiante.Remove(estudiante);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
            return db.Estudiante.Any(e => e.IdEstudiante == id);
        }
    }
}
