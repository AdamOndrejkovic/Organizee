namespace Core.Models
{
    // Todo item
    public class TodoItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
    }
}