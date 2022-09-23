using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaccinationWebSite.Model;
using VaccinationWebSite.Data;
using System.Net.Mail;
using System.Net;

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
                        int sendEmail = 0;
                        switch (RegistersForUser.Count())
                        {
                            case 1:
                                if (dateNow >= LastRegister.VaccineDate.Date.AddDays(Config.SecondVaccine - Config.NotificationDays))
                                {
                                    sendEmail = 1;
                                }
                                break;
                            case 2:
                                if ((dateNow >= LastRegister.VaccineDate.Date.AddDays(Config.ThirdVaccine - Config.NotificationDays)))
                                {
                                    sendEmail = 1;
                                }
                                break;
                            case 3:
                                if ((dateNow >= LastRegister.VaccineDate.Date.AddDays(Config.FourthVaccine - Config.NotificationDays)))
                                {
                                    sendEmail = 1;
                                }
                                break;
                        }
                        if(sendEmail == 1)
                        {
                            try
                            {

                                MailMessage mail = new MailMessage();
                                mail.To.Add(p.Email);
                                mail.Subject = "Vaccine Alert!";
                                mail.Body = "<div class=\"alert alert-danger\">\r\n    <strong>Alert!</strong> The date of your next vaccine is approaching, for more information check the following <a href=\"https://localhost:7121/Register\" class=\"alert-link\">Link</a>.\r\n  </div>";
                                mail.IsBodyHtml = true;

                                //Sender
                                mail.From = new MailAddress("john.dante.cr.058@gmail.com", "John");

                                //Configuration SMTP
                                SmtpClient client = new SmtpClient();
                                client.UseDefaultCredentials = false;
                                //GMAIL Password
                                client.Credentials = new NetworkCredential("john.dante.cr.058@gmail.com", "rbbmmgissspgigoq");
                                client.Port = 587;
                                client.EnableSsl = true;
                                client.Host = "smtp.gmail.com";
                                client.Send(mail);

                                notified = 1;
                            }
                            catch(Exception x)
                            {
                                notified = 0;
                            }
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