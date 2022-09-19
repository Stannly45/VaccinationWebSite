using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace VaccinationWebSite.Model
{
    [Keyless]
    public class Register
    {
        [Required]
        public int ID_Person { get; set; }
        [Required]
        public int ID_Vaccine { get; set; }
        [Required]
        public DateTime VaccineDate { get; set; }
    }
}
