using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaccinationWebSite.Model;
using VaccinationWebSite.Data;
namespace VaccinationWebSite.Pages
{
    public class NewVaccineModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IEnumerable<Vaccine> Vaccines { get; set; }
        public Register Register { get; set; }
        int ID;
        public NewVaccineModel(ApplicationDbContext dbConnection)
        {
            _context = dbConnection;
        }
        public void OnGet()
        {
            string w = Convert.ToString(Request.QueryString.Value);
            string[] i = w.Split('=');
            ViewData["ID"] = int.Parse(i[i.Length - 1]);
            //ID = Request.QueryString["name"];
            //string a = Convert.ToString(Request.QueryString("name"));
            Vaccines = _context.Vaccines;
        }
        public async Task<IActionResult> OnPost(Register register)
        {

            if (ModelState.IsValid) 
            {
                await _context.Registers.AddAsync(register);
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
