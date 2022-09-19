using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaccinationWebSite.Model;
using VaccinationWebSite.Data;

namespace VaccinationWebSite.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Person people { get; set; }
        public RegisterModel(ApplicationDbContext dbConnection)
        {
            _context = dbConnection;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            int ci = int.Parse(Request.Form["ci"]);
            if (ci == 0)
            {
                return Page();
            }
            people = _context.Persons.Find(ci);
            if (people != null)
            {
                ViewData["person"] = people;
            }
            else
            {
                ViewData["person"] = "null";
            }
            return RedirectToPage("NewRegister");
        }
    }
}
