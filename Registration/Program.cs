using Registration.repo;
using Registration.service;
using Registration.service.Exception;
using Registration.service.ServiceAddAddressToUser;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapPost("/registration/newUser",  (HttpRequest request) =>
{
    var userService = new ServiceNewUser(request.Body);

    try
    {
        userService.CreateUser();
    }
    catch (MailExistException e)
    {
        
        return Results.Conflict("It is not possible to create two users with the same E-mail");
    }
    catch (InvalidPasswordException e)
    {
        
        return Results.Conflict("The password is unsave");
    }
    catch (InvalidMailSyntaxException e)
    {
        
        return Results.Conflict("The Mail have a invalid Syntax");
    }
    catch (NullReferenceException)
    {
        return Results.Conflict("Missing Data");
    }
    
    return Results.Json("Successful Registration",statusCode: 200);
});


app.MapPost("/registration/addAddress/{userId}", (HttpRequest request,string userId) =>
{
    var addressToUser = new ServiceAddAddressToUser(request.Body,userId);
    var isSaved = addressToUser.AddAddress();
    return Results.Json("true",statusCode: 200);
});




app.Run();
