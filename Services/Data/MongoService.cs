using MongoDB.Driver;

namespace BlazorFull.Services.Data
{
    public class MongoService
    {
        public static IMongoClient CreateMongoClient(IConfiguration configuration)
        {
            string environment = configuration.GetValue<string>("Environment");
            string connectionString = environment == "development" ? "mongodb://localhost:27017" : "Atlas string";
            try
            {
                MongoClient m = new MongoClient(connectionString);
                Console.WriteLine("Connected to mongoDb succesfully");
                return m;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
    
}
