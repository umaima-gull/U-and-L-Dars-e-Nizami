using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Darsenizami.Models;

namespace Darsenizami.Controllers
{
    public class AdmFacultySubjectsController : Controller
    {
        private readonly DarsEnizamiContext _context;

        public AdmFacultySubjectsController(DarsEnizamiContext context)
        {
            _context = context;
        }

        // GET: AdmFacultySubjects
        public async Task<IActionResult> Index()
        {
            var darsEnizamiContext = _context.FacultySubjects.Include(f => f.ClassYearNavigation).Include(f => f.Faculty).Include(f => f.Subject);
            return View(await darsEnizamiContext.ToListAsync());
        }

        // GET: AdmFacultySubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultySubject = await _context.FacultySubjects
                .Include(f => f.ClassYearNavigation)
                .Include(f => f.Faculty)
                .Include(f => f.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultySubject == null)
            {
                return NotFound();
            }

            return View(facultySubject);
        }

        // GET: AdmFacultySubjects/Create
        public IActionResult Create()
        {
            ViewData["ClassYear"] = new SelectList(_context.YearLevels, "YearId", "YearId");
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyId");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            return View();
        }

        // POST: AdmFacultySubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FacultyId,SubjectId,ClassYear")] FacultySubject facultySubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facultySubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassYear"] = new SelectList(_context.YearLevels, "YearId", "YearId", facultySubject.ClassYear);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyId", facultySubject.FacultyId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", facultySubject.SubjectId);
            return View(facultySubject);
        }

        // GET: AdmFacultySubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultySubject = await _context.FacultySubjects.FindAsync(id);
            if (facultySubject == null)
            {
                return NotFound();
            }
            ViewData["ClassYear"] = new SelectList(_context.YearLevels, "YearId", "YearId", facultySubject.ClassYear);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyId", facultySubject.FacultyId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", facultySubject.SubjectId);
            return View(facultySubject);
        }

        // POST: AdmFacultySubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FacultyId,SubjectId,ClassYear")] FacultySubject facultySubject)
        {
            if (id != facultySubject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facultySubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultySubjectExists(facultySubject.Id))
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
            ViewData["ClassYear"] = new SelectList(_context.YearLevels, "YearId", "YearId", facultySubject.ClassYear);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyId", facultySubject.FacultyId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", facultySubject.SubjectId);
            return View(facultySubject);
        }

        // GET: AdmFacultySubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultySubject = await _context.FacultySubjects
                .Include(f => f.ClassYearNavigation)
                .Include(f => f.Faculty)
                .Include(f => f.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultySubject == null)
            {
                return NotFound();
            }

            return View(facultySubject);
        }

        // POST: AdmFacultySubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facultySubject = await _context.FacultySubjects.FindAsync(id);
            if (facultySubject != null)
            {
                _context.FacultySubjects.Remove(facultySubject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultySubjectExists(int id)
        {
            return _context.FacultySubjects.Any(e => e.Id == id);
        }
    }
}
