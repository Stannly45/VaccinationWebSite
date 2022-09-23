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
            
            if (ModelState.IsValid && personValid(person))
            {
                Persons = _context.Persons;
                Persons = Persons.Where(s => s.CI.Equals(person.CI));

                if (Persons.Count() > 0)
                {
                    return Page();
                }
                await _context.Persons.AddAsync(person);
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();
        }

        public bool personValid(Person p)
        {
            if (p.Name.Length < 2 || p.LastName.Length < 2 || p.CI < 99999 || p.BirthDate.Date > DateTime.Now.Date.AddYears(-3))
            {
                return false;
            }
            return true;
        }
    }
}
