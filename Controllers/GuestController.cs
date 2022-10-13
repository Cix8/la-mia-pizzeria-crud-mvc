using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.MyDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class GuestController : Controller
    {
        private Pizzeria _pizzeria_db;

        public GuestController()
        {
            _pizzeria_db = new Pizzeria();
        }
        public IActionResult Index()
        {
            List<PizzaModel> pizzaList = new List<PizzaModel>();
            pizzaList = _pizzeria_db.Pizzas.OrderBy(pizza => pizza.Id).ToList<PizzaModel>();
            return View(pizzaList);
        }

        public IActionResult Details(int id)
        {
            PizzaModel thisPizza = _pizzeria_db.Pizzas.Where(pizza => pizza.Id == id).Include("Category").Include("Ingredients").First();
            return View("Show", thisPizza);
        }
    }
}
