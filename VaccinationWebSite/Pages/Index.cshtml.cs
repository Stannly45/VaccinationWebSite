using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaccinationWebSite.Model;
using VaccinationWebSite.Data;
namespace VaccinationWebSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        public IEnumerable<Register> Registers { get; set; }
        public IEnumerable<Person> Persons { get; set; }
        public Conf Config { get; set; }
        public IEnumerable<Register> RegistersForUser { get; set; }
        public IndexModel(ApplicationDbContext dbConnection)
        {
            _context = dbConnection;
        }
        public void OnGet()
        {
            SendEmailAsync();
        }
        public async Task<IActionResult> SendEmailAsync()
        {
            Registers = _context.Registers.ToList();
            Persons = _context.Persons.ToList();
            Config = _context.Conf.First();
            foreach (var p in Persons)
            {
                RegistersForUser = Registers.Where(s => s.ID_Person.Equals(p.ID));
                if (RegistersForUser.Count() > 0)
                {
                    Register LastRegister = RegistersForUser.Last();
                    if (LastRegister.Notified == 0)
                    {
                        DateTime dateNow = DateTime.Now.Date;
                        int notified = 0;
                        switch (RegistersForUser.Count())
                        {
                            case 1:
                                if (dateNow >= LastRegister.VaccineDate.Date.AddDays(Config.SecondVaccine - Config.NotificationDays))
                                {
                                    /*
                                     Send Email to
                                        p.Email
                                     */
                                    notified = 1;
                                }
                                break;
                            case 2:
                                if ((dateNow >= LastRegister.VaccineDate.Date.AddDays(Config.ThirdVaccine - Config.NotificationDays)))
                                {
                                    /*
                                     Send Email to
                                        p.Email
                                     */
                                    notified = 1;
                                }
                                break;
                            case 3:
                                if ((dateNow >= LastRegister.VaccineDate.Date.AddDays(Config.FourthVaccine - Config.NotificationDays)))
                                {
                                    /*
                                     Send Email to
                                        p.Email
                                     */
                                    notified = 1;
                                }
                                break;
                        }
                        if (notified == 1)
                        {
                            LastRegister.Notified = 1;
                            _context.Registers.Update(LastRegister);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            return Page();
        }
    }
}