using MongoDB.Driver;
using Registration.model;
using Registration.repo;
namespace Registration.service.ServiceChangePreferredAddress;

public class ServiceChangePreferredAddress
{
    private String userId;
    private int addressIndex;

    public ServiceChangePreferredAddress(string userId, int addressIndex)
    {
        this.userId = userId;
        this.addressIndex = addressIndex;
    }

    public void ChangePreferredAddress()
    {
        var validator = new ValidateDataPreferredAddress(userId, addressIndex);
        validator.IsValid();
        UpdateInMongoDb();
    }

    private void UpdateInMongoDb()
    {
        var repo = new RepoNewUser();
        var connection = repo.GetUserCollection();
        
        var filter = Builders<ModelNewUser>.Filter.Eq(user => user.Id ,userId);
        var update = Builders<ModelNewUser>.Update.Set(user => user.prefertAddressIndex, addressIndex);
        connection.UpdateOne(filter, update);
    }
}