using System.Text.Json;
using MongoDB.Driver;
using Registration.model;
using Registration.repo;
using Registration.service.ServiceChangePreferredAddress.Excepion;

namespace Registration.service.ServiceGetCheckoutData;

public class ServiceGetCheckoutData
{
    private readonly string _userId;
    

    public ServiceGetCheckoutData(string userId)
    {
        _userId = userId;
    }

    public string GetData()
    {
        var validateGetCheckout = new ValidateGetCheckout(_userId);
        validateGetCheckout.ValidateData();

        var user = GetDataFromMongoDb();
        var checkoutData = DataToModel(user);
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(checkoutData);
        return json;
    }

    private ModelNewUser GetDataFromMongoDb()
    {
        var repo = new RepoNewUser();
        var collection = repo.GetUserCollection();
        var condition = Builders<ModelNewUser>.Filter.Eq((user) => user.Id, _userId);
        var field = Builders<ModelNewUser>.Projection
            .Include(p => p.Addresses)
            .Include(p => p.firstname)
            .Include(p => p.lastName)
            .Include(p => p.prefertAddressIndex);

        try
        {
            var results= collection.Find(condition).Project<ModelNewUser>(field).ToList();
            return results[0];
        } 
        catch (FormatException )
        {
            throw new UserNotFoundException();
        }
    }

    private CheckoutUserModel DataToModel(ModelNewUser user)
    {
        var checkoutData = new CheckoutUserModel
        {
            lastName = user.lastName,
            firstName = user.firstname,
            address = user.Addresses[user.prefertAddressIndex]
        };
        return checkoutData;
    }
    
    
    
    
}