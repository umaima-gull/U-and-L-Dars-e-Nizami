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
    public class AdmAdmissionFormsController : Controller
    {
        private readonly DarsEnizamiContext _context;

        public AdmAdmissionFormsController(DarsEnizamiContext context)
        {
            _context = context;
        }

        // GET: AdmAdmissionForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdmissionForms.ToListAsync());
        }

        // GET: AdmAdmissionForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admissionForm = await _context.AdmissionForms
                .FirstOrDefaultAsync(m => m.FormId == id);
            if (admissionForm == null)
            {
                return NotFound();
            }

            return View(admissionForm);
        }

        // GET: AdmAdmissionForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdmAdmissionForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormId,FullName,Dob,Gender,Contact,Address,PreviousInstitute,Documents,SubmissionDate")] AdmissionForms admissionForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admissionForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admissionForm);
        }

        // GET: AdmAdmissionForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admissionForm = await _context.AdmissionForms.FindAsync(id);
            if (admissionForm == null)
            {
                return NotFound();
            }
            return View(admissionForm);
        }

        // POST: AdmAdmissionForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormId,FullName,Dob,Gender,Contact,Address,PreviousInstitute,Documents,SubmissionDate")] AdmissionForms admissionForm)
        {
            if (id != admissionForm.FormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admissionForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionFormExists(admissionForm.FormId))
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
            return View(admissionForm);
        }

        // GET: AdmAdmissionForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admissionForm = await _context.AdmissionForms
                .FirstOrDefaultAsync(m => m.FormId == id);
            if (admissionForm == null)
            {
                return NotFound();
            }

            return View(admissionForm);
        }

        // POST: AdmAdmissionForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admissionForm = await _context.AdmissionForms.FindAsync(id);
            if (admissionForm != null)
            {
                _context.AdmissionForms.Remove(admissionForm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmissionFormExists(int id)
        {
            return _context.AdmissionForms.Any(e => e.FormId == id);
        }
    }
}
