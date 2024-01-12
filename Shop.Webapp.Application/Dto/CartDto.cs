namespace Shop.Webapp.Application.Dto
{
    public class CartDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
    }
}
