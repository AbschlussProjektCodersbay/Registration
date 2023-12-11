namespace Registration.model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class UserAddress
{
    public int ZIP{ get; set;}
    public string street{ get; set;}
    public string city{ get; set;}
    public string country{ get; set;}
    
    public int houseNumber{ get; set;}
    public int level{ get; set;}
    

}