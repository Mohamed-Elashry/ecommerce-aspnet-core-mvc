namespace E_Commerce.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser Users { get; set; }
        public string UserId { get; set; }
        public List<OrderItem> orderItems { get; set; }
    }
}
