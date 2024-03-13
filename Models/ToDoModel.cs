namespace BlazorFull.Models
{
    public class ToDoModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }

        public bool IsDone { get; set; } = false;
        public bool IsEditing { get; set; } = false; // Add this line

    }
}
