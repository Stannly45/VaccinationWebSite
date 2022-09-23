using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using VaccinationWebSite.Data;
using VaccinationWebSite.Model;

namespace VaccinationWebSite.Pages
{
    public class notifyModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IEnumerable<Vaccine> Vaccines { get; set; }
        public IEnumerable<Register> Registers { get; set; }
        public IEnumerable<String> Names { get; set; }
        public IEnumerable<DateTime> VaccineDate { get; set; }
        public Register Register { get; set; }
        public Person Person { get; set; }
        public notifyModel(ApplicationDbContext dbConnection)
        {
            _context = dbConnection;
        }
        public void OnGet()
        {
            ListVaccinesRegister();
            Vaccines = _context.Vaccines;
        }
        public async Task<IActionResult> OnPost(Register register)
        {
            ListVaccinesRegister();
            int i = Registers.Count();
            if (i > 0)
            {
                Person = _context.Persons.Find(register.ID_Person);
                MailMessage mail = new MailMessage();
                mail.To.Add(Person.Email);
                mail.Subject = "Vaccine Alert!";
                mail.Body = @"<h1 style=""text-align:center; color:#0DC9EF; font-family: Verdana, Arial, Helvetica, sans-serif;""; >
                        Vaccination Alert!
                        </h1>
                        <p style=""text-align:center; font-size: 14px; font-family: Verdana, Arial, Helvetica, sans-serif;""; >
                            The date of your next vaccination is getting closer, do not forget that you have to get</br> vaccinated at one of the established points. You can also go to the Hospital to get</br> vaccinated. More information in the link
                        </p>
                        <div style=""text-align:center;"">
                            <a href=""https://localhost:7121/Register"">
                                <button style=""background-color: #4CAF50;
                            border: none;
                            color: white;
                            padding: 15px 32px;
                            text-align: center;
                            text-decoration: none;
                            display: inline-block;
                            font-size: 16px;"">More Info
                                </button>
                            </a>
    
                        </div>";
                mail.IsBodyHtml = true;

                //Sender
                mail.From = new MailAddress("john.dante.cr.058@gmail.com", "Hospital");

                //Configuration SMTP
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                //GMAIL Password
                client.Credentials = new NetworkCredential("john.dante.cr.058@gmail.com", "rbbmmgissspgigoq");
                client.Port = 587;
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Send(mail);
            }
            else
            {
                return Page();
            }
            return RedirectToPage("admin");
        }
        public void ListVaccinesRegister()
        {
            string w = Convert.ToString(Request.QueryString.Value);
            string[] i = w.Split('=');
            int id = int.Parse(i[i.Length - 1]);
            ViewData["ID"] = id;
            Registers = _context.Registers.ToList();
            Registers = Registers.Where(r => r.ID_Person.Equals(id));
        }
    }
}
