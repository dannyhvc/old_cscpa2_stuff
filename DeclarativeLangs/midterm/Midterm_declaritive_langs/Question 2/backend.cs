using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Question_2.backend
{
    public class Recipe
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public List<Ingredient> Ingredients { get; set; }
    }

    public class Ingredient
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Units { get; set; }
    }

    public class RecipeManager
    {
        public Recipe Recipe { get; set; }

        public RecipeManager(string title)
        {
            Recipe = new Recipe();
            Recipe.Title = title;
            Recipe.Ingredients = new List<Ingredient>();
        }

        public void AddIngredient(Ingredient ingredient) => Recipe.Ingredients.Add(ingredient);
    }
}
