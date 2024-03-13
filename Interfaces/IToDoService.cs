using BlazorFull.Models;

namespace BlazorFull.Interfaces
{
    public interface IToDoService
    {
       Task<List<ToDoModel>> GetTasks();
       Task<ToDoModel> GetTask(Guid id);

       Task<ToDoModel> AddTask(ToDoModel toDoModel);
       Task<ToDoModel> EditTask(Guid id, ToDoModel toDoModel);

       Task<bool> DeleteTask(Guid id);
       Task IsDoneTask(Guid id);

    }
}
