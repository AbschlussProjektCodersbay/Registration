using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using MongoDB.Bson;
using Registration.model;
using Registration.repo;

namespace Registration.service;

public class ServiceNewUser
{
    private Stream userData;
    public ServiceNewUser(Stream userData)
    {
        this.userData = userData;
    }

    public void CreateUser()
    {
        var data =DataToModel(userData);
        CheckNewUser(data.email, data.password);
        Console.WriteLine("save");
        SaveUser(data);
        
    }
    
    private  ModelNewUser DataToModel(Stream data)
    {
        using var reader = new StreamReader(data);
        var body = reader.ReadToEndAsync().Result;
        var userModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelNewUser>(body);
        userModel.joineDate = DateOnly.FromDateTime(DateTime.Now);
        
        return userModel;
    }

    private void SaveUser(ModelNewUser user)
    {
        var repo = new RepoNewUser();
        var collection = repo.GetUserCollection();
        collection.InsertOne(user);
    }
    
    

    private void CheckNewUser(string email, string password)
    {
        var validator = new ValidateData();
        validator.IsValid(email, password);
    }
    
    
    
    
    
}