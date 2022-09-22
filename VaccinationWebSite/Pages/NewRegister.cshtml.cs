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
        public IEnumerable<Person> Persons { get; set; }
        public NewRegisterModel(ApplicationDbContext dbConnection)
        {
            _context = dbConnection;
        }
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPost(Person person)
        {
            Persons = _context.Persons;
            Persons = Persons.Where(s => s.CI.Equals(person.CI));

            if (Persons.Count() > 0)
            {
                return Page();
            }
            if (ModelState.IsValid)
            {
                await _context.Persons.AddAsync(person);
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();

        }
    }
}
