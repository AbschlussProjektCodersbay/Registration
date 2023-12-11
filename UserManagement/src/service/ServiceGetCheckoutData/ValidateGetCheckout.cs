using MongoDB.Driver;
using Registration.model;
using Registration.repo;
using Registration.service.ServiceChangePreferredAddress.Excepion;
using Registration.service.ServiceGetCheckoutData.Exceptions;

namespace Registration.service.ServiceGetCheckoutData;

public class ValidateGetCheckout
{
    private readonly List<ModelNewUser> _users;
    
    
    public ValidateGetCheckout(string userId)
    {
        this._users = FindAddressesFromUser(userId);
    }
    
    public void ValidateData(){
        IsUserExisting();
        HaveAddress();
        IsAddressInRange();
    }
    
    
    private List<ModelNewUser> FindAddressesFromUser(string userId)
    {
        var repo = new RepoNewUser();
        var collection = repo.GetUserCollection();
        var condition = Builders<ModelNewUser>.Filter.Eq((user) => user.Id, userId);
        var field = Builders<ModelNewUser>.Projection
            .Include(p => p.Addresses)
            .Include(p => p.preferredAddressIndex);

        try
        {
            var results= collection.Find(condition).Project<ModelNewUser>(field).ToList();
            return results;
        }
        catch (FormatException )
        {
            throw new UserNotFoundException();
        }
        
    }


    private void IsUserExisting()
    {
        if (_users == null)
        {
            throw new UserNotFoundException();
        }

        if (_users.Count == 0)
        {
            throw new UserNotFoundException();
        }
    }

    private void IsAddressInRange()
    {
        var preferAddress = _users[0].preferredAddressIndex;
        Console.Write(preferAddress);
        if (_users[0].Addresses.Count  <= preferAddress || preferAddress < 0)
        {
            throw new IndexNotInAddressRangeException();
        }
        
    }

    private void HaveAddress()
    {
        if (_users[0].Addresses.Count == 0)
        {
            throw new UserHaveNoAddressException();
        }
    }
}