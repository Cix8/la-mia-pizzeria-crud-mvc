namespace la_mia_pizzeria_static.Models
{
    public class PizzaCategories
    {
        public PizzaModel Pizza { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public List<IngredientModel> Ingredients { get; set; }
        public List<int> selectedIng { get; set; }
        public PizzaCategories()
        {
            Pizza = new PizzaModel();
            Categories = new List<CategoryModel>();
            Ingredients = new List<IngredientModel>();
        }
    }
}
