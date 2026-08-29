using Darsenizami.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Darsenizami.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DarsEnizamiContext _context;


        public HomeController(ILogger<HomeController> logger, DarsEnizamiContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminIndex()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction(nameof(Login));
            }

            return RedirectToAction("Index", "AdmAdmins");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string loginId, string password)
        {
            if (string.IsNullOrWhiteSpace(loginId) || string.IsNullOrWhiteSpace(password))
            {
                TempData["Icon"] = "warning";
                TempData["Title"] = "Login Required";
                TempData["Message"] = "Please enter your user ID/email and password.";
                return Redirect("/Home/Login");
            }

            int? userId = null;
            if (int.TryParse(loginId.Trim(), out var parsedUserId))
            {
                userId = parsedUserId;
            }

            var user = await _context.Users
                .Include(u => u.Admins)
                .FirstOrDefaultAsync(u =>
                    ((userId.HasValue && u.UserId == userId.Value)
                     || u.Email == loginId.Trim()
                     || u.FullName == loginId.Trim()));

            if (user == null)
            {
                TempData["Icon"] = "error";
                TempData["Title"] = "Login Failed";
                TempData["Message"] = "Email or Password Incorrect.";
                return Redirect("/Home/Login");
            }


            return RedirectToAction("Index", "AdmAdmins");
          
        }

        public IActionResult LoginSuccess()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction(nameof(Login));
            }

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Icon"] = "success";
            TempData["Title"] = "Logged Out";
            TempData["Message"] = "You have been logged out successfully.";
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string email, string pword, string pword2)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pword))
            {
                TempData["Icon"] = "warning";
                TempData["Title"] = "Missing Information";
                TempData["Message"] = "Please fill all required fields.";
                return View();
            }

            if (pword != pword2)
            {
                TempData["Icon"] = "error";
                TempData["Title"] = "Password Error";
                TempData["Message"] = "Password and re-type password must match.";
                return View();
            }

            var normalizedEmail = email.Trim();
            var emailExists = await _context.Users.AnyAsync(u => u.Email == normalizedEmail);
            if (emailExists)
            {
                TempData["Icon"] = "error";
                TempData["Title"] = "Account Exists";
                TempData["Message"] = "This email is already registered.";
                return View();
            }

            var user = new User
            {
                FullName = username.Trim(),
                Email = normalizedEmail,
                Password = pword,
                Role = "customer",
                Status = "active",
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["Icon"] = "success";
            TempData["Title"] = "Registered";
            TempData["Message"] = "Account registered successfully. Please log in.";
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Contact()
        {
            return View();
        }
        public async Task<IActionResult> Books()
        {
            var darsEnizamiContext = _context.Books.Include(b => b.Subject).Include(b => b.YearLevelNavigation);
            return View(await darsEnizamiContext.ToListAsync());
        }

        public async Task<IActionResult> Faculty()
        {
            var darsEnizamiContext = _context.Faculties.Include(f => f.User);
            return View(await darsEnizamiContext.ToListAsync());
        }



        public IActionResult AdmissionForms()
        {
            ViewData["FormId"] = new SelectList(_context.AdmissionForms, "FormId", "FormId");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");

            return View();
        }

        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormId,FullName,Dob,Gender,Contact,Address,PreviousInstitute,Documents,SubmissionDate")] AdmissionForms admissionForm)
        {

            _context.Add(admissionForm);
            await _context.SaveChangesAsync();


            TempData["Icon"] = "success";
            TempData["Title"] = "Success";
            TempData["Message"] = "Account registered ";


            return Redirect("/Home/Index");

        }









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
