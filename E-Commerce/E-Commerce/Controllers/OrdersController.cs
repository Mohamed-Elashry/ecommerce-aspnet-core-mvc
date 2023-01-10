using E_Commerce.Data.Cart;
using E_Commerce.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ShoppingCart _shoppingCart;
        public OrdersController(IUnitOfWork unitOfWork,ShoppingCart shoppingCart)
        {
            _unitOfWork= unitOfWork;
            _shoppingCart= shoppingCart;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _unitOfWork.Orders.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            orders = orders.Where(c => c.orderItems.Count() > 0).ToList();
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var cartItems = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = cartItems;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal =  _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<RedirectToActionResult> AddToShoppingCart(int id)
        {
            var item =await _unitOfWork.Movies.GetByIdAsync(id);
            if (item != null)
                await _shoppingCart.AddItemToCart(item);

            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _unitOfWork.Movies.GetByIdAsync(id);
            if (item != null)
                await _shoppingCart.RemoveItemFromCart(item);

            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> CompleteOrder()
        {
            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var _userEmail = User.FindFirstValue(ClaimTypes.Email);
            var Items = _shoppingCart.GetShoppingCartItems();
            await _unitOfWork.Orders.StoreOrderAsync(Items, _userId, _userEmail);
            await _shoppingCart.ClearShoppingCartItems();
            return View("CompleteOrder");
        }
    }
}
