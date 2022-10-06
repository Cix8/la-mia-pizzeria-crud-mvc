using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.MyDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
        }

        private void PizzaSeeder()
        {
            using (Pizzeria pizzeria_db = new Pizzeria())
            {
                PizzaModel newPizza = new PizzaModel("Margherita", "La classica pizza napoletana", "pizza-margherita.jfif", 5.99F);
                pizzeria_db.Add(newPizza);
                pizzeria_db.SaveChanges();
                
                newPizza = new PizzaModel("Capricciosa", "La pizza capricciosa è una pizza tipica della cucina italiana caratterizzata da un condimento di pomodoro, mozzarella, prosciutto cotto, funghi, olive verdi e nere, e carciofini", "pizza-capricciosa.jfif", 7.99F);
                pizzeria_db.Add(newPizza);
                pizzeria_db.SaveChanges();

                newPizza = new PizzaModel("Salame Piccante", "Pizza base margherita con aggiunta di salame piccante", "pizza-salame.jfif", 6.99F);
                pizzeria_db.Add(newPizza);
                pizzeria_db.SaveChanges();
            }
        }

        private void Store(PizzaModel pizza)
        {
            using (Pizzeria pizzeria_db = new Pizzeria())
            {
                pizzeria_db.Add(pizza);
                pizzeria_db.SaveChanges();
            }
        }

        public IActionResult Index()
        {
            List<PizzaModel> pizzaList = new List<PizzaModel>();
            using (Pizzeria pizzeria_db = new Pizzeria())
            {
                pizzaList = pizzeria_db.Pizzas.OrderBy(pizza => pizza.Id).ToList<PizzaModel>();
                if(pizzaList.Count == 0)
                {
                    this.PizzaSeeder();
                    pizzaList = pizzeria_db.Pizzas.OrderBy(pizza => pizza.Id).ToList<PizzaModel>();
                }
            }
            return View(pizzaList);
        }

        public IActionResult Details(int id)
        {
            using (Pizzeria pizzeria_db = new Pizzeria())
            {
                PizzaModel thisPizza = pizzeria_db.Pizzas.Find(id);
                return View("Show", thisPizza);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }
            this.Store(new PizzaModel(model.Name, model.Description, model.Image, (float)model.Price));
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}