using MongoDB.Driver;
using Registration.model;
using Registration.repo;
using Xunit.Sdk;

namespace Registration.service.ServiceAddAddressToUser;

public class ServiceAddAddressToUser
{
    private Stream _addressData;
    private string userId;
    public ServiceAddAddressToUser(Stream addressData, string userId)
    {
        this._addressData = addressData;
        this.userId = userId;
    }

    public bool AddAddress()
    {
        var userAddress = DataToAddressModel();
        updateAddress(userAddress);
        return true;
    }
    
    private  UserAddress DataToAddressModel()
    {
        using var reader = new StreamReader(this._addressData);
        var body = reader.ReadToEndAsync().Result;
        var addressData = Newtonsoft.Json.JsonConvert.DeserializeObject<UserAddress>(body);
        Console.WriteLine($"level {addressData.level} street{addressData.street} zip{addressData.zip} houseNumber{addressData.houseNumber}");
        return addressData;
    }

    private void updateAddress(UserAddress userAddress)
    {
        var repo = new RepoNewUser();
        var connection = repo.GetUserCollection();
        var user = connection.FindSync(user => user.Id == userId).ToList();
        var newAddress = user[0].Addresses;
        
        newAddress.Add(userAddress);
        var filter = Builders<ModelNewUser>.Filter.Eq(user => user.Id ,userId);
        var update = Builders<ModelNewUser>.Update.Set(user => user.Addresses, newAddress);
        connection.UpdateOne(filter, update);
        
    }
    
    
    
}