using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaccinationWebSite.Model;
using VaccinationWebSite.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace VaccinationWebSite.Pages
{
    public class NewVaccineModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IEnumerable<Vaccine> Vaccines { get; set; }
        public IEnumerable<Register> Registers { get; set; }
        public IEnumerable<String> Names { get; set; }
        public IEnumerable<DateTime> VaccineDate { get; set; }
        public Register Register { get; set; }
        public NewVaccineModel(ApplicationDbContext dbConnection)
        {
            _context = dbConnection;
        }
        public async void OnGet()
        {

            //var TableRegisters = Registers.Join(Vaccines, Registers => Registers.ID_Vaccine, Vaccine => Vaccine.ID, (Registers, Vaccines) => new { Name = Vaccines.Name, Date = Register.VaccineDate });

            /*foreach (var item in TableRegisters)
            {
                Names.Aggregate(item.Name);
                VaccineDate.Aggregate(item.Date);
            }*/
            ListVaccinesRegister();
            Vaccines = _context.Vaccines;
        }
        public async Task<IActionResult> OnPost(Register register)
        {
            ListVaccinesRegister();
            if (Registers != null)
            {
                Register lastRegister = Registers.Last();
                if (register.VaccineDate < lastRegister.VaccineDate)
                {
                    //Si la fecha del nuevo registro de vacuna es menor a la ultima dosis no se registra
                    return Redirect(string.Format("~/NewVaccine?name={0}", Convert.ToInt64(ViewData["ID"])));
                    return Page();
                }
            }
            if (ModelState.IsValid) 
            {
                await _context.Registers.AddAsync(register);
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return Page();
        }
        public void ListVaccinesRegister()
        {
            string w = Convert.ToString(Request.QueryString.Value);
            string[] i = w.Split('=');
            int id = int.Parse(i[i.Length - 1]);
            ViewData["ID"] = id;
            Registers = _context.Registers;
            Registers = Registers.Where(r => r.ID_Person.Equals(id));
        }
    }
}
