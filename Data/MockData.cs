using BlazorFull.Models;

namespace BlazorFull.Data
{
    public static class MockData
    {
        public static List<ToDoModel> todosData = new List<ToDoModel>()
        {
            new ToDoModel()
            {
                Title = "Test",
                IsDone = true,
            }

        };
    }
}
