namespace ArbusMenuMaster
{
    public interface IMenuMaster
    {
        int TotalDishesCount { get; }
        int TotalPagesCount { get; }
        int DishesCountOnPage(int pageNumber);
        IEnumerable<Dish> DishesOnPage(int pageNumber);
        IEnumerable<Dish> FirstDishesOnEachPage();
    }
}
