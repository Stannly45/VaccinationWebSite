using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaccinationWebSite.Model;
using VaccinationWebSite.Data;
using Microsoft.EntityFrameworkCore;

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
            if(ModelState.IsValid)
            {
                string stringCi = Request.Form["ci"];
                if (stringCi.Trim() == "")
                {
                    return Page();
                }
                else
                {
                    int ci = int.Parse(stringCi);
                    if (ci <= 0)
                    {
                        return Page();
                    }
                    else
                    {
                        var people = from m in _context.Persons select m;
                        people = people.Where(s => s.CI.Equals(ci));

                        if (people.Count() > 0)
                        {
                            Person p = people.First();
                            return Redirect(string.Format("~/NewVaccine?name={0}", p.ID));
                        }
                        else
                        {
                            return RedirectToPage("NewRegister");
                        }
                    }
                }
            }
            return Page();
        }
    }
}
