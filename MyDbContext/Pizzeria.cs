using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.MyDbContext
{
    public class Pizzeria : IdentityDbContext<IdentityUser>
    {
        public Pizzeria()
        {
        }

        public Pizzeria(DbContextOptions<Pizzeria> options) : base(options)
        {
        }

        private string connString = "Data Source=localhost;Initial Catalog = pizzeria_db; Integrated Security = True";
        public DbSet<PizzaModel> Pizzas { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<IngredientModel> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connString);
        }
    }
}
