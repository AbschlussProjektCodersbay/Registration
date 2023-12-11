using MongoDB.Driver;
using Registration.model;
using Registration.repo;
using Registration.service.ServiceChangePreferredAddress.Excepion;

namespace Registration.service.ServiceGetPreferredAddressIndex;

public class ServiceGetPreferredAddressIndex
{
    public Dictionary<string,int> GetAddressesIndex(string userId)
    {
        Console.WriteLine("addresses");
        var addressesIndex = GetDataFromMongoDb(userId);
        var indexDic = new Dictionary<string, int>();
        indexDic.Add("AddressIndex",addressesIndex);
        return indexDic;
    }
    
    
    private int GetDataFromMongoDb(string userId)
    {
        var repo = new RepoNewUser();
        var collection = repo.GetUserCollection();
        var condition = Builders<ModelNewUser>.Filter.Eq((user) => user.Id, userId);
        var field = Builders<ModelNewUser>.Projection
            .Include(p => p.preferredAddressIndex);
            
        try
        {
            Console.WriteLine("start");
            var results= collection.Find(condition).Project<ModelNewUser>(field).ToList();
            Console.WriteLine(results);
            return results[0].preferredAddressIndex;
        } 
        catch (FormatException )
        {
            throw new UserNotFoundException();
        }
    }
}