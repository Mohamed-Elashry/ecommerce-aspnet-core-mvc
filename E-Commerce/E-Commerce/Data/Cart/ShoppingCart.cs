namespace E_Commerce.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get;set; }
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            var CartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", CartId);

            return new ShoppingCart(context) { ShoppingCartId = CartId };
        }
        public async Task AddItemToCart(Movie movie)
        {
            ShoppingCartItem cartItem = await _context.ShoppingCartItems
                                       .FirstOrDefaultAsync(c =>
                                             c.Movie == movie &&
                                             c.ShoppingCartId == ShoppingCartId) ??
                                             new ShoppingCartItem();
            if (cartItem.Id == 0)
            {
                cartItem = new ShoppingCartItem()
                {
                    Movie = movie,
                    ShoppingCartId = ShoppingCartId,
                    Amount = 1,
                };
                await _context.ShoppingCartItems.AddAsync(cartItem);
            }
            else
            {
                cartItem.Amount++;
            }
            await _context.SaveChangesAsync();
        }
        public async Task RemoveItemFromCart(Movie movie)
        {
            ShoppingCartItem cartItem = await _context.ShoppingCartItems
                                       .FirstOrDefaultAsync(c =>
                                             c.Movie == movie &&
                                             c.ShoppingCartId == ShoppingCartId) ??
                                             new();
            if (cartItem.Id > 0)
            {
                if (cartItem.Amount > 1)
                    cartItem.Amount--;
                else
                    _context.ShoppingCartItems.Remove(cartItem);
            }

            await _context.SaveChangesAsync();
        }

        public List<ShoppingCartItem> GetShoppingCartItems() => ShoppingCartItems ??
            (ShoppingCartItems =  _context.ShoppingCartItems
                                 .Where(c => c.ShoppingCartId == ShoppingCartId)
                                 .Include(m => m.Movie)
                                 .ToList());

        public double GetShoppingCartTotal() =>
             _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
            .Include(m => m.Movie)
            .Select(s => s.Movie.Price * s.Amount).Sum(); 

        public async Task ClearShoppingCartItems()
        {
            var items = await _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
