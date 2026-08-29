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
    public class AdmStudentsController : Controller
    {
        private readonly DarsEnizamiContext _context;

        public AdmStudentsController(DarsEnizamiContext context)
        {
            _context = context;
        }

        // GET: AdmStudents
        public async Task<IActionResult> Index()
        {
            var darsEnizamiContext = _context.Students.Include(s => s.User).Include(s => s.YearLevelNavigation);
            return View(await darsEnizamiContext.ToListAsync());
        }

        // GET: AdmStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.YearLevelNavigation)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: AdmStudents/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            ViewData["YearLevel"] = new SelectList(_context.YearLevels, "YearId", "YearId");
            return View();
        }

        // POST: AdmStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,UserId,RollNo,YearLevel,JoiningDate,Status,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", student.UserId);
            ViewData["YearLevel"] = new SelectList(_context.YearLevels, "YearId", "YearId", student.YearLevel);
            return View(student);
        }

        // GET: AdmStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", student.UserId);
            ViewData["YearLevel"] = new SelectList(_context.YearLevels, "YearId", "YearId", student.YearLevel);
            return View(student);
        }

        // POST: AdmStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,UserId,RollNo,YearLevel,JoiningDate,Status,Address")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", student.UserId);
            ViewData["YearLevel"] = new SelectList(_context.YearLevels, "YearId", "YearId", student.YearLevel);
            return View(student);
        }

        // GET: AdmStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.YearLevelNavigation)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: AdmStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
