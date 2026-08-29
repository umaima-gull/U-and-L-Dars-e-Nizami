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
    public class AdmAttendancesController : Controller
    {
        private readonly DarsEnizamiContext _context;

        public AdmAttendancesController(DarsEnizamiContext context)
        {
            _context = context;
        }

        // GET: AdmAttendances
        public async Task<IActionResult> Index()
        {
            var darsEnizamiContext = _context.Attendances.Include(a => a.Student).Include(a => a.Subject);
            return View(await darsEnizamiContext.ToListAsync());
        }

        // GET: AdmAttendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Subject)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: AdmAttendances/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            return View();
        }

        // POST: AdmAttendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttendanceId,StudentId,SubjectId,AttendanceDate,Status")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", attendance.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", attendance.SubjectId);
            return View(attendance);
        }

        // GET: AdmAttendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", attendance.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", attendance.SubjectId);
            return View(attendance);
        }

        // POST: AdmAttendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttendanceId,StudentId,SubjectId,AttendanceDate,Status")] Attendance attendance)
        {
            if (id != attendance.AttendanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AttendanceId))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", attendance.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", attendance.SubjectId);
            return View(attendance);
        }

        // GET: AdmAttendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Subject)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: AdmAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.AttendanceId == id);
        }
    }
}
