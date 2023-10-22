namespace Registration.model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class UserAddress
{
    public int zip;
    public string street;
    public int houseNumber;
    public int level; 

}