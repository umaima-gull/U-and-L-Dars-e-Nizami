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
    public class AdmBooksController : Controller
    {
        private readonly DarsEnizamiContext _context;
        private readonly IWebHostEnvironment _environment;
        private const string BookUploadFolder = "uploads/books";

        public AdmBooksController(DarsEnizamiContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: AdmBooks
        public async Task<IActionResult> Index()
        {
            var darsEnizamiContext = _context.Books.Include(b => b.Subject).Include(b => b.YearLevelNavigation);
            return View(await darsEnizamiContext.ToListAsync());
        }

        // GET: AdmBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Subject)
                .Include(b => b.YearLevelNavigation)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: AdmBooks/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            ViewData["YearLevel"] = new SelectList(_context.YearLevels, "YearId", "YearId");
            return View();
        }

        // POST: AdmBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Author,YearLevel,SubjectId,PdfLink,Description")] Book book, IFormFile? pdfFile)
        {
            if (ModelState.IsValid)
            {
                var pdfPath = await SavePdfAsync(pdfFile);
                if (pdfPath == null && pdfFile is { Length: > 0 })
                {
                    ModelState.AddModelError("PdfLink", "Please upload PDF file only.");
                    ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", book.SubjectId);
                    ViewData["YearLevel"] = new SelectList(_context.YearLevels, "YearId", "YearId", book.YearLevel);
                    return View(book);
                }

                if (!string.IsNullOrWhiteSpace(pdfPath))
                {
                    book.PdfLink = pdfPath;
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", book.SubjectId);
            ViewData["YearLevel"] = new SelectList(_context.YearLevels, "YearId", "YearId", book.YearLevel);
            return View(book);
        }

        // GET: AdmBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", book.SubjectId);
            ViewData["YearLevel"] = new SelectList(_context.YearLevels, "YearId", "YearId", book.YearLevel);
            return View(book);
        }

        // POST: AdmBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Author,YearLevel,SubjectId,PdfLink,Description")] Book book, IFormFile? pdfFile)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingBook = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == id);
                    if (existingBook == null)
                    {
                        return NotFound();
                    }

                    var pdfPath = await SavePdfAsync(pdfFile);
                    if (pdfPath == null && pdfFile is { Length: > 0 })
                    {
                        ModelState.AddModelError("PdfLink", "Please upload PDF file only.");
                        ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", book.SubjectId);
                        ViewData["YearLevel"] = new SelectList(_context.YearLevels, "YearId", "YearId", book.YearLevel);
                        return View(book);
                    }

                    book.PdfLink = !string.IsNullOrWhiteSpace(pdfPath) ? pdfPath : existingBook.PdfLink;
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", book.SubjectId);
            ViewData["YearLevel"] = new SelectList(_context.YearLevels, "YearId", "YearId", book.YearLevel);
            return View(book);
        }

        // GET: AdmBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Subject)
                .Include(b => b.YearLevelNavigation)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: AdmBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }

        private async Task<string?> SavePdfAsync(IFormFile? pdfFile)
        {
            if (pdfFile == null || pdfFile.Length == 0)
            {
                return string.Empty;
            }

            if (!string.Equals(Path.GetExtension(pdfFile.FileName), ".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var uploadRoot = Path.Combine(_environment.WebRootPath, BookUploadFolder.Replace('/', Path.DirectorySeparatorChar));
            Directory.CreateDirectory(uploadRoot);

            var safeFileName = $"{Guid.NewGuid():N}.pdf";
            var filePath = Path.Combine(uploadRoot, safeFileName);

            await using var stream = System.IO.File.Create(filePath);
            await pdfFile.CopyToAsync(stream);

            return "/" + BookUploadFolder + "/" + safeFileName;
        }
    }
}
