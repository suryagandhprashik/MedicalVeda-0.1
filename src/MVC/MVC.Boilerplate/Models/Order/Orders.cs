namespace MVC.Boilerplate.Models.Order
{
    public class Orders
    {
        public Guid Id { get; set; }
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
    }
}
