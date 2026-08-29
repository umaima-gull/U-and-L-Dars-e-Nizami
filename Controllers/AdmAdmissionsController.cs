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
    public class AdmAdmissionsController : Controller
    {
        private readonly DarsEnizamiContext _context;

        public AdmAdmissionsController(DarsEnizamiContext context)
        {
            _context = context;
        }

        // GET: AdmAdmissions
        public async Task<IActionResult> Index()
        {
            var darsEnizamiContext = _context.Admissions.Include(a => a.Form).Include(a => a.Student);
            return View(await darsEnizamiContext.ToListAsync());
        }

        // GET: AdmAdmissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = await _context.Admissions
                .Include(a => a.Form)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.AdmissionId == id);
            if (admission == null)
            {
                return NotFound();
            }

            return View(admission);
        }

        // GET: AdmAdmissions/Create
        public IActionResult Create()
        {
            ViewData["FormId"] = new SelectList(_context.AdmissionForms, "FormId", "FormId");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: AdmAdmissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdmissionId,StudentId,FormId,AdmissionDate,Status,Remarks")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormId"] = new SelectList(_context.AdmissionForms, "FormId", "FormId", admission.FormId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", admission.StudentId);
            return View(admission);
        }

        // GET: AdmAdmissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = await _context.Admissions.FindAsync(id);
            if (admission == null)
            {
                return NotFound();
            }
            ViewData["FormId"] = new SelectList(_context.AdmissionForms, "FormId", "FormId", admission.FormId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", admission.StudentId);
            return View(admission);
        }

        // POST: AdmAdmissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdmissionId,StudentId,FormId,AdmissionDate,Status,Remarks")] Admission admission)
        {
            if (id != admission.AdmissionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionExists(admission.AdmissionId))
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
            ViewData["FormId"] = new SelectList(_context.AdmissionForms, "FormId", "FormId", admission.FormId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", admission.StudentId);
            return View(admission);
        }

        // GET: AdmAdmissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = await _context.Admissions
                .Include(a => a.Form)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.AdmissionId == id);
            if (admission == null)
            {
                return NotFound();
            }

            return View(admission);
        }

        // POST: AdmAdmissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admission = await _context.Admissions.FindAsync(id);
            if (admission != null)
            {
                _context.Admissions.Remove(admission);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmissionExists(int id)
        {
            return _context.Admissions.Any(e => e.AdmissionId == id);
        }
    }
}
