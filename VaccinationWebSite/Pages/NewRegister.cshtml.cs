using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaccinationWebSite.Model;
using VaccinationWebSite.Data;
namespace VaccinationWebSite.Pages
{
    public class NewRegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Person Person { get; set; }
        public NewRegisterModel(ApplicationDbContext dbConnection)
        {
            _context = dbConnection;
        }
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPost(Person person)
        {

            if (ModelState.IsValid) ///Se validan los campos del producto
            {
                await _context.Persons.AddAsync(person);
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            
            return Page();
        }
    }
}
