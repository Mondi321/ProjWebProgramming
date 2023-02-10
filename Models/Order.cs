using System.ComponentModel.DataAnnotations.Schema;

namespace ProjWebProgramming.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public string Email { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
}
