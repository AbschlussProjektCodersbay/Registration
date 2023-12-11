using MongoDB.Driver;
using Registration.model;
using Registration.repo;
using Registration.service.ServiceChangePreferredAddress.Excepion;

namespace Registration.service.ServiceGetAddress;

public class ServiceGetAddress
{
    public List<UserAddress> GetAddresses(string userId)
    {
        Console.WriteLine("addresses");
        var addresses = GetDataFromMongoDb(userId);
        
        return addresses;
    }
    
    
    private List<UserAddress> GetDataFromMongoDb(string userId)
    {
        var repo = new RepoNewUser();
        var collection = repo.GetUserCollection();
        var condition = Builders<ModelNewUser>.Filter.Eq((user) => user.Id, userId);
        var field = Builders<ModelNewUser>.Projection
            .Include(p => p.Addresses);
            
        try
        {
            Console.WriteLine("start");
            var results= collection.Find(condition).Project<ModelNewUser>(field).ToList();
            Console.WriteLine(results);
            return results[0].Addresses;
        } 
        catch (FormatException )
        {
            throw new UserNotFoundException();
        }
    }
}