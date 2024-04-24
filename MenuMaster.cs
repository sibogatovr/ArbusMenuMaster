namespace ArbusMenuMaster
{
    public class MenuMaster : IMenuMaster
    {
        private readonly IEnumerable<Dish> dishes;
        private readonly int dishesPerPage;
        private readonly int totalPagesCount;

        public MenuMaster(IEnumerable<Dish> dishes, int dishesPerPage)
        {
            ValidateInput(dishes, dishesPerPage);

            this.dishes = dishes;
            this.dishesPerPage = dishesPerPage;
            this.totalPagesCount = CalculateTotalPagesCount();
        }
        // 1. Общее количество блюд
        public int TotalDishesCount => dishes.Count();

        // 2. Количество страниц
        public int TotalPagesCount => totalPagesCount;

        // 3. Количество блюд на указанной странице;
        public int DishesCountOnPage(int pageNumber)
        {
            ValidatePageNumber(pageNumber);
            int startIndex = (pageNumber - 1) * dishesPerPage;
            int endIndex = Math.Min(startIndex + dishesPerPage, TotalDishesCount);
            return endIndex - startIndex;
        }

        // 4. Блюда указанной страницы;
        public IEnumerable<Dish> DishesOnPage(int pageNumber)
        {
            ValidatePageNumber(pageNumber);
            int startIndex = (pageNumber - 1) * dishesPerPage;
            return dishes.Skip(startIndex).Take(DishesCountOnPage(pageNumber));
        }

        // 5. Список первых блюд каждой страницы.
        public IEnumerable<Dish> FirstDishesOnEachPage()
        {
            for (int i = 1; i <= totalPagesCount; i++)
            {
                yield return DishesOnPage(i).FirstOrDefault();
            }
        }

        private int CalculateTotalPagesCount()
        {
            return (int)Math.Ceiling((double)TotalDishesCount / dishesPerPage);
        }

        private void ValidatePageNumber(int pageNumber)
        {
            if (pageNumber < 1 || pageNumber > totalPagesCount)
                throw new ArgumentException($"Номер страницы должен быть в диапозоне от 1 до {totalPagesCount}");
        }

        private void ValidateInput(IEnumerable<Dish> dishes, int dishesPerPage)
        {
            if (dishes == null || !dishes.Any())
                throw new ArgumentNullException(nameof(dishes), "Коллекция блюд не должна быть null или пустой");

            if (dishes.Any(d => string.IsNullOrWhiteSpace(d.Name)))
                throw new ArgumentException("Имя блюда не может быть пустым или содержать только пробельные символы");

            if (dishes.Any(d => d.Name.TrimStart().Length != d.Name.Length))
                throw new ArgumentException("Имя блюда не может начинаться с пробельных символов");

            if (dishesPerPage <= 0)
                throw new ArgumentException("Количество страниц должно быть больше 0");
        }
    }
}
