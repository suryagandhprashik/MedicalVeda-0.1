namespace MVC.Boilerplate.Models.Category.Queries
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string  Name { get; set; }
        public List<EventsForCategory>? Events { get; set; }
    }

    public class EventsForCategory
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Artist { get; set; }
        public DateTime Date { get; set; }
        public Guid CategoryId { get; set; }
    }
}
