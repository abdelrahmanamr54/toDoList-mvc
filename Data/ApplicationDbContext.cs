using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ApplicationDbContext :DbContext
    {

        public DbSet<Person> person { get; set; }
        public DbSet<Item> items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(builder
                );
        }
    }
}
