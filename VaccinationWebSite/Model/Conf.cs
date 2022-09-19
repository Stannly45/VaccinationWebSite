using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace VaccinationWebSite.Model
{
    [Keyless]
    public class Conf
    {
        public int NotificationDays { get; set; }
        public int SecondVaccine { get; set; }
        public int ThirdVaccine { get; set; }
        public int FourthVaccine { get; set; }
    }
}
