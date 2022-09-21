using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace VaccinationWebSite.Model
{
    public class Register
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int ID_Person { get; set; }
        [Required]
        public int ID_Vaccine { get; set; }
        [Required]
        public DateTime VaccineDate { get; set; }
        public int Notified { get; set; }
    }
}
