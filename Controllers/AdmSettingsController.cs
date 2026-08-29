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
    public class AdmSettingsController : Controller
    {
        private readonly DarsEnizamiContext _context;

        public AdmSettingsController(DarsEnizamiContext context)
        {
            _context = context;
        }

        // GET: AdmSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Settings.ToListAsync());
        }

        // GET: AdmSettings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .FirstOrDefaultAsync(m => m.SettingId == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // GET: AdmSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdmSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SettingId,Key,Value")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(setting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(setting);
        }

        // GET: AdmSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings.FindAsync(id);
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting);
        }

        // POST: AdmSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SettingId,Key,Value")] Setting setting)
        {
            if (id != setting.SettingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingExists(setting.SettingId))
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
            return View(setting);
        }

        // GET: AdmSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .FirstOrDefaultAsync(m => m.SettingId == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // POST: AdmSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var setting = await _context.Settings.FindAsync(id);
            if (setting != null)
            {
                _context.Settings.Remove(setting);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SettingExists(int id)
        {
            return _context.Settings.Any(e => e.SettingId == id);
        }
    }
}
