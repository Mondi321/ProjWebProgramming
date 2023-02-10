using System.ComponentModel.DataAnnotations.Schema;

namespace ProjWebProgramming.Models
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }

        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
