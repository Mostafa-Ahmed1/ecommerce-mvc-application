using eTicktets.Data.Cart;
using eTicktets.Data.Services;
using eTicktets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eTicktets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService moviesService;
        private readonly ShoppingCart shoppingCart;
        private readonly IOrdersService ordersService;

        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            this.moviesService = moviesService;
            this.shoppingCart = shoppingCart;
            this.ordersService = ordersService;
            
        }

        public async Task<IActionResult> Index()
        {
            string userId = "";
            var orders = await ordersService.GetOrderByUserIdAsync(userId);

            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<IActionResult> AddingItemToShoppingCart(int id)
        {
            var item = await moviesService.GetByIdAsync(id);

            if(item != null) shoppingCart.AddItemToCart(item);

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await moviesService.GetByIdAsync(id);

            if (item != null) shoppingCart.RemoveItemFromCart(item);

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";

            await ordersService.storeOrderAsync(items, userId, userEmailAddress);
            await shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}
