using MongoDB.Driver;
using Registration.model;
using Registration.repo;
using Registration.service.ServiceChangePreferredAddress.Excepion;

namespace Registration.service.ServiceChangePreferredAddress;

public class ValidateDataPreferredAddress
{
    private readonly List<ModelNewUser> _users;
    private readonly int _index;
    
    public ValidateDataPreferredAddress(string userId, int index)
    {
        this._index = index;
        this._users = FindAddressesFromUser(userId);
    }

    private List<ModelNewUser> FindAddressesFromUser(string userId)
    {
        var repo = new RepoNewUser();
        var collection = repo.GetUserCollection();
        var condition = Builders<ModelNewUser>.Filter.Eq((user) => user.Id, userId);
        var field = Builders<ModelNewUser>.Projection.Include(p => p.Addresses);

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


    public bool IsValid()
    {
        CheckIndexToLenght();
        return true;
    }

    private void CheckIndexToLenght()
    {
        
        IsUserExisting();
        var addresses = _users[0].Addresses;
        if (addresses == null)
        {
            throw new IndexNotInAddressRangeException();
        }
        if (addresses.Count <= _index || _index < 0)
        {
            throw new IndexNotInAddressRangeException();
        }
        
    }

    private void IsUserExisting()
    {
        if (_users.Count <= 0)
        {
            throw new UserNotFoundException();
        }
    }
    
}