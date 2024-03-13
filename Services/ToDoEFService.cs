using BlazorFull.Interfaces;
using BlazorFull.Models;
using BlazorFull.Services.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BlazorFull.Services
{
    public class ToDoEFService: IToDoService
    {
        private readonly ApplicationDbContext _context;
        public ToDoEFService(ApplicationDbContext context)
        {
            _context = context;

            if (!_context.Todos.Any())
            {
                // Insert default tasks if the table is empty
                InsertDefaultTasks();
            }

        }


        public async Task<List<ToDoModel>> GetTasks()
        {

            List<ToDoModel> allTasks = await _context.Todos.ToListAsync();
            return allTasks;
        }


        public async Task<ToDoModel> GetTask(Guid id)
        {
            ToDoModel ? task = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                throw new Exception("there is no task like this");
            }

            return task;
        }

        public async Task<ToDoModel> AddTask(ToDoModel toDoModel)
        {

           await _context.Todos.AddAsync(toDoModel);
            await _context.SaveChangesAsync();

            return toDoModel;
        }




        public async Task<bool> DeleteTask(Guid id)
        {
            var task = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                throw new Exception("there is no task like this");

            }
            _context.Todos.Remove(task);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<ToDoModel> EditTask(Guid id, ToDoModel toDoModel)
        {

            var ToDoSql = await _context.Todos.FindAsync(id);
            if (ToDoSql == null)
            {
                return null;
            }

            // Update fields in UserSqlModel
            ToDoSql.Title = toDoModel.Title;

            // Save changes in the database
            await _context.SaveChangesAsync();
            return toDoModel;
        }




        public async Task IsDoneTask(Guid id)
        {
            ToDoModel task = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                throw new Exception("there is no task like this");
            }


            task.IsDone = !task.IsDone;
            await _context.SaveChangesAsync();

        }

        private void InsertDefaultTasks()
        {
            var defaultTasks = new List<ToDoModel>
                {
                    new ToDoModel
                    {
                        Title = "buy a Laptop",

                    },
                    new ToDoModel
                    {
                        Title = "send a message",

                    },
                    new ToDoModel
                    {
                        Title = "Do homework",
                    },
            };

            _context.Todos.AddRange(defaultTasks);
            _context.SaveChanges();

        }

    }
}
