using ArbusMenuMaster;
using FluentAssertions;

namespace ArbusMenu.Tests
{
    public class MenuMasterTests
    {
        [Test]
        public void TotalDishesCount_ShouldReturnCorrectValue()
        {
            var dishes = new List<Dish>
            {
                new Dish {Name = "Борщ" },
                new Dish {Name = "Картошка" },
                new Dish {Name = "Паста" },
                new Dish {Name = "Капуста" },
                new Dish {Name = "Авокадо" }
            };
            var menu = new MenuMaster(dishes, 2);

            var totalDishesCount = menu.TotalDishesCount;

            totalDishesCount.Should().Be(5);
        }

        [TestCase(5, 2, ExpectedResult = 3, Description = "Ожидаю что будет 3 страницы")]
        [TestCase(5, 1, ExpectedResult = 5, Description = "Ожидаю что будет 5 страниц")]
        [TestCase(5, 3, ExpectedResult = 2, Description = "Ожидаю что будет 2 страницы")]
        public int TotalPagesCount_ShouldReturnCorrectValue(int dishesCount, int dishesPerPage)
        {
            var dishes = Enumerable.Range(1, dishesCount).Select(i => new Dish { Name = i.ToString() });
            var menu = new MenuMaster(dishes, dishesPerPage);

            return menu.TotalPagesCount;
        }

        [TestCase(5, 1, 1, ExpectedResult = 1, Description = "Ожидаю что на 1й странице будет 1 блюдо")]
        [TestCase(5, 2, 2, ExpectedResult = 2, Description = "Ожидаю что на 2й странице будет 2 блюда")]
        [TestCase(150, 15, 9, ExpectedResult = 15, Description = "Ожидаю что на 9й странице будет 15 блюд")]
        public int DishesCountOnPage_ShouldReturnCorrectCount(int dishesCount, int dishesPerPage, int pageNumber)
        {
            var dishes = Enumerable.Range(1, dishesCount).Select(i => new Dish { Name = $"Dish {i}" });
            var menu = new MenuMaster(dishes, dishesPerPage);

            int result = menu.DishesCountOnPage(pageNumber);

            return result;
        }

        [Test]
        public void DishesCountOnPage_ShouldThrowArgumentException_WhenPageNumberIsOutOfRange()
        {
            var dishes = Enumerable.Range(1, 5).Select(i => new Dish { Name = i.ToString() });
            var menu = new MenuMaster(dishes, 2);

            Action action = () => menu.DishesCountOnPage(4);
            action.Should().Throw<ArgumentException>()
                .WithMessage($"Номер страницы должен быть в диапозоне от 1 до {menu.TotalPagesCount}");
        }

        [Test]
        public void DishesOnPage_ShouldReturnCorrectDishes()
        {
            var dishes = Enumerable.Range(1, 5).Select(i => new Dish { Name = i.ToString() });
            var menu = new MenuMaster(dishes, 2);

            var dishesOnPage = menu.DishesOnPage(2);

            dishesOnPage.Should().HaveCount(2);
            dishesOnPage.Should().Contain(d => d.Name == "3");
            dishesOnPage.Should().Contain(d => d.Name == "4");
        }

        [Test]
        public void FirstDishesOnEachPage_ShouldReturnCorrectDishes()
        {
            var dishes = Enumerable.Range(1, 5).Select(i => new Dish { Name = i.ToString() });
            var menu = new MenuMaster(dishes, 2);

            var firstDishes = menu.FirstDishesOnEachPage().ToList();

            firstDishes.Should().HaveCount(3);
            firstDishes.Should().Contain(d => d.Name == "1");
            firstDishes.Should().Contain(d => d.Name == "3");
            firstDishes.Should().Contain(d => d.Name == "5");
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenDishesIsNull()
        {
            IEnumerable<Dish> dishes = null;
            int dishesPerPage = 2;

            Action action = () => new MenuMaster(dishes, dishesPerPage);
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenDishNameStartsWithWhitespace()
        {
            var dishes = new List<Dish>
        {
            new Dish { Name = "Борщ" },
            new Dish { Name = "Картошка" },
            new Dish { Name = "   Паста" },
            new Dish { Name = "Капуста" },
            new Dish { Name = "Авокадо" }
        };

            Action action = () => new MenuMaster(dishes, 2);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenPagesIsInvalid()
        {
            var dishes = new List<Dish>();

            // for pages = 0
            Action actionZeroPages = () => new MenuMaster(dishes, 0);
            actionZeroPages.Should().Throw<ArgumentException>();

            // for negative pages
            Action actionNegativePages = () => new MenuMaster(dishes, -1);
            actionNegativePages.Should().Throw<ArgumentException>();
        }
    }
}