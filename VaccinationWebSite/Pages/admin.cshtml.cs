using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VaccinationWebSite.Data;
using VaccinationWebSite.Model;

namespace VaccinationWebSite.Pages
{
    public class adminModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IEnumerable<Vaccine> Vaccines { get; set; }
        public IEnumerable<Person> Persons { get; set; }
        public IEnumerable<String> Names { get; set; }
        public IEnumerable<DateTime> VaccineDate { get; set; }
        public Register Register { get; set; }
        public adminModel(ApplicationDbContext dbConnection)
        {
            _context = dbConnection;
        }
        public void OnGet()
        {
            ListPersons();
        }

        public void ListPersons()
        {
            Persons = _context.Persons.ToList();
        }
    }
}
