using Microsoft.EntityFrameworkCore;
using VaccinationWebSite.Model;
namespace VaccinationWebSite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Conf> Conf { get; set; }
    }
}
