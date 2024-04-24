namespace ArbusMenuMaster
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dishes = new List<Dish>
            {
                new Dish {Name = "Карась" },
                new Dish {Name = "Картошка" },
                new Dish {Name = "Паста" },
                new Dish {Name = "Каша" },
                new Dish {Name = "Баклажан" },
            };

            IEnumerable<Dish> dishesEnumerable = dishes;

            var menu = new MenuMaster(dishesEnumerable, 1);

            Console.WriteLine($"1. Количество: {menu.TotalDishesCount}");
            Console.WriteLine($"2. Количество страниц: {menu.TotalPagesCount}");
            Console.WriteLine($"3. Количество блюд на странице: {menu.DishesCountOnPage(5)}");
            Console.WriteLine($"4. Блюда указанной страницы: {string.Join(", ", menu.DishesOnPage(3).Select(d => d.Name))}");
            Console.WriteLine($"5. Список первых блюд каждой страницы: {string.Join(", ", menu.FirstDishesOnEachPage().Select(d => d.Name))}");
        }
    }
}
