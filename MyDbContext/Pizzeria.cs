using la_mia_pizzeria_static.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.MyDbContext
{
    public class Pizzeria : DbContext
    {
        private string connString = "Data Source=localhost;Initial Catalog = pizzeria_db; Integrated Security = True";
        public DbSet<PizzaModel> Pizzas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connString);
        }
    }
}
