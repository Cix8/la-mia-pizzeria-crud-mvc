using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class IngredientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PizzaModel> Pizzas { get; set; }

        public IngredientModel()
        {

        }
    }
}
