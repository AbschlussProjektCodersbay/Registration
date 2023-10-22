using MongoDB.Driver;
using Registration.model;
using Registration.repo;
using Registration.service;
using Xunit;

namespace Registration.testing;

public class TestServiceNewUser
{
    private string TESTMAIL = "testmailfortesting@mail.com";
    private Stream testStream = GenerateStreamFromString(
        """{"firstname":"tester1234","lastname":"Heinz","email":"testmailfortesting@mail.com","password":"password123"}""");
    private  RepoNewUser? repo = new RepoNewUser();
    


    public static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
    
    [Fact]
    void CreateUserTest()
    {
        var collection = repo.GetUserCollection();
        
        ServiceNewUser _serviceNewUser = new ServiceNewUser(testStream);
        _serviceNewUser.CreateUser();
        
        var foundMail =  collection.FindSync((user) => user.email == TESTMAIL).ToList();
        collection.DeleteMany(user => user.email == TESTMAIL);
        Assert.Single(foundMail);
    }
}