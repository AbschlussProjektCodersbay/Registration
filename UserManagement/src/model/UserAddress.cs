namespace Registration.model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class UserAddress
{
    public int zip{ get; set;}
    public string street{ get; set;}
    public int houseNumber{ get; set;}
    public int level{ get; set;}

}