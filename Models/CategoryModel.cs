namespace la_mia_pizzeria_static.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PizzaModel> Pizzas { get; set; }
        public CategoryModel()
        {

        }
    }
}
