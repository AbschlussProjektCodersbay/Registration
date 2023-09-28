using System.Runtime.InteropServices.JavaScript;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Registration.model;

public class ModelNewUser
{
    [BsonId] 
    [BsonRepresentation(BsonType.ObjectId)]
    public String Id{ get; set; }
    
    public string firstname { get; set; }
    public String lastName { get; set; }
    public String email { get; set; }
    public String password { get; set; }
    public DateOnly joineDate { get; set; }
}