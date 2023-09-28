using MongoDB.Bson.IO;
using Registration.model;

namespace Registration.repo;
using MongoDB.Driver;
using MongoDB.Bson;
public class RepoNewUser
{
    private  static MongoClient? _connection;
    
    private MongoClient? createClient()
    {
        var settings = MongoClientSettings.FromConnectionString(Properties.mongodbConnectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        return client;
    } 
    
    private MongoClient? getConnection()
    {
        
        if(_connection == null)
        {
            Console.WriteLine("_connection");
            _connection = createClient();
        }
        Console.WriteLine("old");
        return _connection;
    }

    public IMongoCollection<ModelNewUser> GetUserCollection()
    {
        var con = getConnection();
        var db = con.GetDatabase("userManagement");
        var mongoCollection = db.GetCollection<ModelNewUser>("UserLoginData");
        return mongoCollection;
    }
    

}