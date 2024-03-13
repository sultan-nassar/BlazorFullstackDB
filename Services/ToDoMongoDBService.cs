using BlazorFull.Data;
using BlazorFull.Interfaces;
using BlazorFull.Models;
using MongoDB.Driver;

namespace BlazorFull.Services
{
    public class ToDoMongoDBService:IToDoService
    {

        private IMongoCollection<ToDoModel> _todos;

        public ToDoMongoDBService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Tasks");
            _todos = database.GetCollection<ToDoModel>("ToDoList");
            if (_todos.CountDocuments(FilterDefinition<ToDoModel>.Empty) == 0)
            {
                // Insert a default product
                InsertDefaultProduct();
            }
        }


        public async Task<List<ToDoModel>> GetTasks()
        {

            List<ToDoModel> allTasks = await _todos.Find(_ => true).ToListAsync();
            return allTasks;
        }
    

        public async Task<ToDoModel> GetTask(Guid id)
        {
            ToDoModel task = await _todos.Find(t => t.Id == id).FirstAsync();
            if (task == null)
            {
                throw new Exception("there is no task like this");
            }
           
            return task;
        }

        public async Task<ToDoModel> AddTask(ToDoModel toDoModel)
        {

            _todos.InsertOneAsync(toDoModel);
            return toDoModel;
        }

        public async Task<bool> DeleteTask(Guid id)
        {
            var task =await _todos.DeleteOneAsync(t => t.Id == id);
            if (task.DeletedCount == 0)
            {
                //exception user not found
                throw new Exception("there is no task to delete");
            }
            
            return true;

        }

        public async Task<ToDoModel> EditTask(Guid id, ToDoModel toDoModel)
        {

            var filter = Builders<ToDoModel>.Filter.Eq(u => u.Id, (id));

            var update = Builders<ToDoModel>.Update
                .Set(u => u.Title, toDoModel.Title);
          
             

            var result = await _todos.UpdateOneAsync(filter, update);


            // Check if the update was successful
            if (result.MatchedCount == 0)
            {
                throw new Exception("dd");
            }

               return toDoModel;
        }




            public async Task IsDoneTask(Guid id)
        {
            ToDoModel task = await _todos.Find(t => t.Id == id).FirstAsync();
            if (task == null)
            {
                throw new Exception("there is no task like this");
            }


            task.IsDone = !task.IsDone;
        }

        private void InsertDefaultProduct()
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

            _todos.InsertManyAsync(defaultTasks);
        }
    }
}
