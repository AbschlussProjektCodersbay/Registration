using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
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
        var isValid =  checkNewUser(data.email, data.password);
        if (!isValid)
        {
            return ;
        }
        Console.WriteLine("save");
        saveUser(data);
        

    }
    
    private  ModelNewUser DataToModel(Stream data)
    {
        using var reader = new StreamReader(data);
        var body = reader.ReadToEndAsync().Result;
        var userModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelNewUser>(body);
        userModel.joineDate = DateOnly.FromDateTime(DateTime.Now);
        
        return userModel;
    }

    private async void saveUser(ModelNewUser user)
    {
        var repo = new RepoNewUser();
        var collection = repo.GetUserCollection();
        await collection.InsertOneAsync(user);
    }
    
    

    private bool checkNewUser(String email, String password)
    {
        var validator = new ValidateData();
        return  validator.isValid(email,password);
    }
    
    
    
    
    
}