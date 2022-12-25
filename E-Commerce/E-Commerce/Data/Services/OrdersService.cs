namespace E_Commerce.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId) =>
            await _context.Orders
                          .Include(inc => inc.orderItems)
                          .ThenInclude(inc => inc.Movie)
                          .Where(o => o.UserId == userId).ToListAsync();
        

        public async Task StoreOrderAsync(List<ShoppingCartItem> shoppingCartItems, string userId, string userEmail)
        {
            Order order = new Order()
            {
                UserId = userId,
                Email = userEmail,
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            if (shoppingCartItems.Count() > 0)
            {
                List<OrderItem> OrderItems = new List<OrderItem>();
                OrderItem orderItem = new OrderItem();
                foreach (var item in shoppingCartItems)
                {
                    orderItem = new OrderItem();
                    orderItem.OrderId = order.Id;
                    orderItem.MovieId = item.Movie.Id;
                    orderItem.Amount = item.Amount;
                    orderItem.Price = item.Movie.Price;
                    OrderItems.Add(orderItem);
                }
                await _context.OrderItems.AddRangeAsync(orderItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
