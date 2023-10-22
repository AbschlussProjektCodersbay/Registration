using MongoDB.Bson.IO;
using Registration.model;

namespace Registration.repo;
using MongoDB.Driver;
using MongoDB.Bson;
public class RepoNewUser
{
    private  static MongoClient? _connection;
    
    private MongoClient CreateClient()
    {
        var settings = MongoClientSettings.FromConnectionString(Properties.mongodbConnectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        return new MongoClient(settings);
    } 
    
    private MongoClient GetConnection()
    {
        
        if(_connection == null)
        {
            Console.WriteLine("_connection");
            _connection = CreateClient();
        }
        Console.WriteLine("old");
        return _connection;
    }

    public IMongoCollection<ModelNewUser> GetUserCollection()
    {
        var con = GetConnection();
        var db = con.GetDatabase("userManagement");
        var mongoCollection = db.GetCollection<ModelNewUser>("UserLoginData");
        return mongoCollection;
    }
    

}