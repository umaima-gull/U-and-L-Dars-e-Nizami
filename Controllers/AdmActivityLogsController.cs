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
    public class AdmActivityLogsController : Controller
    {
        private readonly DarsEnizamiContext _context;

        public AdmActivityLogsController(DarsEnizamiContext context)
        {
            _context = context;
        }

        // GET: AdmActivityLogs
        public async Task<IActionResult> Index()
        {
            var darsEnizamiContext = _context.ActivityLogs.Include(a => a.User);
            return View(await darsEnizamiContext.ToListAsync());
        }

        // GET: AdmActivityLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityLog = await _context.ActivityLogs
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (activityLog == null)
            {
                return NotFound();
            }

            return View(activityLog);
        }

        // GET: AdmActivityLogs/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: AdmActivityLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogId,UserId,Action,Timestamp")] ActivityLog activityLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activityLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", activityLog.UserId);
            return View(activityLog);
        }

        // GET: AdmActivityLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityLog = await _context.ActivityLogs.FindAsync(id);
            if (activityLog == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", activityLog.UserId);
            return View(activityLog);
        }

        // POST: AdmActivityLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogId,UserId,Action,Timestamp")] ActivityLog activityLog)
        {
            if (id != activityLog.LogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activityLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityLogExists(activityLog.LogId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", activityLog.UserId);
            return View(activityLog);
        }

        // GET: AdmActivityLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityLog = await _context.ActivityLogs
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (activityLog == null)
            {
                return NotFound();
            }

            return View(activityLog);
        }

        // POST: AdmActivityLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activityLog = await _context.ActivityLogs.FindAsync(id);
            if (activityLog != null)
            {
                _context.ActivityLogs.Remove(activityLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityLogExists(int id)
        {
            return _context.ActivityLogs.Any(e => e.LogId == id);
        }
    }
}
