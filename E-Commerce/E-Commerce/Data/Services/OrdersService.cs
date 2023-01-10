namespace E_Commerce.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders
                          .Include(o => o.Users)
                          .Include(inc => inc.orderItems)
                          .ThenInclude(inc => inc.Movie).ToListAsync();
            if (userRole != "admin")
            {
                orders = orders.Where(o => o.UserId == userId).ToList();
            }
            return orders;
        }
            
        

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
                List<OrderItem> orderItems = new List<OrderItem>();
                OrderItem orderItem = new OrderItem();
                foreach (var item in shoppingCartItems)
                {
                    orderItem = new OrderItem();
                    orderItem.OrderId = order.Id;
                    orderItem.MovieId = item.Movie.Id;
                    orderItem.Amount = item.Amount;
                    orderItem.Price = item.Movie.Price;
                    orderItems.Add(orderItem);
                }
                await _context.OrderItems.AddRangeAsync(orderItems);
                await _context.SaveChangesAsync();
            }
        }
    }
}
