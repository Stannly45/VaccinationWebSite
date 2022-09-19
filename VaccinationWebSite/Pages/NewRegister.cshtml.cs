using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaccinationWebSite.Model;
using VaccinationWebSite.Data;
namespace VaccinationWebSite.Pages
{
    public class NewRegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Person people { get; set; }
        public Vaccine vaccine { get; set; }
        public Register register { get; set; }
        public VaccinesModel(ApplicationDbContext dbConnection)
        {
            _context = dbConnection;
        }
        public void OnGet()
        {
            if (ViewData["person"] != "null")
            {
                people = (Person)ViewData["person"];
            }
            else
            {

            }
        }
        public async Task<IActionResult> OnPost()
        {

            return RedirectToPage("Register");
        }
    }
}
