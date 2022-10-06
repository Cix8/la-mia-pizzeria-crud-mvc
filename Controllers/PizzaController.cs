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
        private Pizzeria _pizzeria_db;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
            _pizzeria_db = new Pizzeria();
        }

        private void PizzaSeeder()
        {
            PizzaModel newPizza = new PizzaModel("Margherita", "La classica pizza margherita napoletana", "pizza-margherita.jfif", 5.99F);
            _pizzeria_db.Add(newPizza);
            _pizzeria_db.SaveChanges();

            newPizza = new PizzaModel("Capricciosa", "La pizza capricciosa è una pizza tipica della cucina italiana caratterizzata da un condimento di pomodoro, mozzarella, prosciutto cotto, funghi, olive verdi e nere, e carciofini", "pizza-capricciosa.jfif", 7.99F);
            _pizzeria_db.Add(newPizza);
            _pizzeria_db.SaveChanges();

            newPizza = new PizzaModel("Salame Piccante", "Pizza base margherita con aggiunta di salame piccante", "pizza-salame.jfif", 6.99F);
            _pizzeria_db.Add(newPizza);
            _pizzeria_db.SaveChanges();
        }

        private void Store(PizzaModel pizza)
        {
            _pizzeria_db.Add(pizza);
            _pizzeria_db.SaveChanges();
        }

        public PizzaModel FindBy(int id)
        {
            return _pizzeria_db.Pizzas.Find(id);
        }

        public IActionResult Index()
        {
            List<PizzaModel> pizzaList = new List<PizzaModel>();
            pizzaList = _pizzeria_db.Pizzas.OrderBy(pizza => pizza.Id).ToList<PizzaModel>();
            if (pizzaList.Count == 0)
            {
                this.PizzaSeeder();
                pizzaList = _pizzeria_db.Pizzas.OrderBy(pizza => pizza.Id).ToList<PizzaModel>();
            }
            return View(pizzaList);
        }

        public IActionResult Details(int id)
        {
            PizzaModel thisPizza = this.FindBy(id);
            return View("Show", thisPizza);
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

        [HttpGet]
        public IActionResult Update(int id)
        {
            PizzaModel thisPizza = this.FindBy(id);
             if(thisPizza != null)
            {
                return View(thisPizza);
            } else
            {
                return NotFound("Non siamo riusciti a trovare la pizza selezionata...");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaModel pizza)
        {
            if(!ModelState.IsValid)
            {
                return View("Update", pizza);
            }

            PizzaModel thisPizza = this.FindBy(id);
            if(thisPizza != null)
            {
                thisPizza.Name = pizza.Name;
                thisPizza.Description = pizza.Description;
                thisPizza.Image = pizza.Image;
                thisPizza.Price = pizza.Price;

                _pizzeria_db.SaveChanges();
            } else
            {
                return NotFound("Non siamo riusciti a trovare la pizza da aggiornare");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            PizzaModel pizza = this.FindBy(id);

            if(pizza != null)
            {
                _pizzeria_db.Remove(pizza);
                _pizzeria_db.SaveChanges();
                return RedirectToAction("Index");
            } else
            {
                return NotFound("Non siamo riusciti a trovare la pizza che intendi eliminare");
            }
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