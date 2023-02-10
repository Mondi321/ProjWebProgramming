namespace ProjWebProgramming.Models
{
    public class ShoppingCartItem
    {
        public Guid ShoppingCartItemId { get; set; }

        public Movie? Movie { get; set; }
        public int Amount { get; set; }


        public string ShoppingCartId { get; set; } = string.Empty;
    }
}
