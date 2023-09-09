using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PizzaApp
{
    class CustomPizza : Pizza
    {
        private static int customPizzaCount = 0;

        public CustomPizza() : base("Customized", 5, false, null)
        {
            customPizzaCount++;
            Name = "Customized " + customPizzaCount;

            Ingredients = new List<string>();

            while (true)
            {
                Console.Write($"Enter an ingredient for customized pizza {customPizzaCount} (Press ENTER to finish): ");
                var ingredient = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ingredient))
                {
                    break;
                }
                if (Ingredients.Contains(ingredient))
                {
                    Console.WriteLine("ERROR: This ingredient is already present in the pizza.");
                }
                else
                {
                    Ingredients.Add(ingredient);
                    Console.WriteLine($"Current ingredients: {string.Join(", ", Ingredients)}");
                }

                Console.WriteLine();
            }

            Price = 5 + Ingredients.Count * 1.5f;
        }
    }

    class Pizza
    {
        public string Name { get; protected set; }
        public float Price { get; protected set; }
        public bool Vegetarian { get; private set; }
        public List<string> Ingredients { get; protected set; }

        public Pizza(string name, float price, bool vegetarian, List<string> ingredients)
        {
            Name = name;
            Price = price;
            Vegetarian = vegetarian;
            Ingredients = ingredients ?? new List<string>();
        }

        public void Show()
        {
            string vegetarianBadge = Vegetarian ? " (V)" : "";
            string formattedName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Name.ToLower());

            var formattedIngredients = Ingredients.Select(i => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(i.ToLower())).ToList();

            Console.WriteLine($"{formattedName}{vegetarianBadge} - {Price:C}");
            Console.WriteLine("Ingredients: " + string.Join(", ", formattedIngredients));
            Console.WriteLine();
        }

        public bool ContainsIngredient(string ingredient)
        {
            return Ingredients.Any(i => i.ToLower().Contains(ingredient.ToLower()));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var pizzas = new List<Pizza>() {
                new Pizza("4 cheese", 11.5f, true, new List<string> { "cantal", "mozzarella", "goat cheese", "gruyère" }),
                new Pizza("Indian", 10.5f, false, new List<string> { "curry", "mozzarella", "chicken", "bell pepper", "onion", "cilantro" }),
                new Pizza("Mexican", 13f, false, new List<string> {"beef", "mozzarella", "corn", "tomatoes", "onion", "cilantro"}),
                new Pizza("Margherita", 8f, true, new List<string> { "tomato sauce", "mozzarella", "basil" }),
                new Pizza("Calzone", 12f, false, new List<string> { "tomato", "ham", "parsley", "onions"}),
                new Pizza("complete", 9.5f, false, new List<string> { "ham", "egg", "cheese" }),
                new CustomPizza(),
                new CustomPizza()
            };

            foreach (var pizza in pizzas)
            {
                pizza.Show();
            }
        }
    }
}
