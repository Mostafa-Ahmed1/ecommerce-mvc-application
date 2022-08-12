using eTicktets.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eTicktets.Data.ViewComponents
{
    // [ViewComponent]
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = shoppingCart.GetShoppingCartItems();

            return View(items.Count);
        }
    }
}
