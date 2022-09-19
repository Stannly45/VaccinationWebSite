using System.ComponentModel.DataAnnotations;
namespace VaccinationWebSite.Model
{
    public class Vaccine
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
