using MongoDB.Bson;
using Registration.repo;
using Registration.service;
using Registration.service.Exception;
using Registration.service.ServiceAddAddressToUser;
using Registration.service.ServiceChangePreferredAddress;
using Registration.service.ServiceChangePreferredAddress.Excepion;
using Registration.service.ServiceGetAddress;
using Registration.service.ServiceGetCheckoutData;
using Registration.service.ServiceGetCheckoutData.Exceptions;
using Registration.service.ServiceGetPreferredAddressIndex;
using Registration.service.ServiceLogin;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapPost("/api/registration/newUser",  (HttpRequest request) =>
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

app.MapGet("/api/registration/getAddress/{userId}", (string userId) =>
{
    var serviceGetAddress = new ServiceGetAddress();
    try
    {
        var addresses = serviceGetAddress.GetAddresses(userId);
        return Results.Json(addresses, statusCode: 200);
    }
    catch (UserNotFoundException)
    {
        return Results.Json("User user not found", statusCode: 404);
    }
    catch (UserHaveNoAddressException)
    {
        return Results.Json("user have no address", statusCode: 422);
    }
    
});

app.MapGet("/api/registration/GetPreferredAddressIndex/{userId}", (string userId) =>
{
    var serviceGetAddress = new ServiceGetPreferredAddressIndex();
    try
    {
        var addressesIndex = serviceGetAddress.GetAddressesIndex(userId);
        return Results.Json(addressesIndex, statusCode: 200);
    }
    catch (UserNotFoundException)
    {
        return Results.Json("User user not found", statusCode: 404);
    }
    catch (UserHaveNoAddressException)
    {
        return Results.Json("user have no address", statusCode: 422);
    }
    
});


app.MapPost("/api/registration/addAddress/{userId}", (HttpRequest request,string userId) =>
{
    var addressToUser = new ServiceAddAddressToUser(request.Body,userId);
    var isSaved = addressToUser.AddAddress();
    return Results.Json("true",statusCode: 200);
});

app.MapPost("/api/registration/ChangePreferredAddress/{userId}/{addressIndex}", (string userId, int addressIndex) =>
{
    var preferredAddress = new ServiceChangePreferredAddress(userId, addressIndex);

    try
    {
        preferredAddress.ChangePreferredAddress();
    }
    catch (UserNotFoundException)
    {
        return Results.Json("user not found", statusCode: 404);
    }
    catch (IndexNotInAddressRangeException)
    {
        return Results.Json("Address out of range", statusCode: 400);
    }


    return Results.StatusCode(200);
});


app.MapGet("/api/registration/DataForCheckOut/{userId}", (string userId) =>
{
    var serviceGetCheckoutData = new ServiceGetCheckoutData(userId);
    try
    {
        var checkoutData = serviceGetCheckoutData.GetData();
        return Results.Json(checkoutData, statusCode: 200);
    }
    catch (UserNotFoundException)
    {
        return Results.Json("UserMG user not found", statusCode: 404);
    }
    catch (UserHaveNoAddressException)
    {
        return Results.Json("user have no address", statusCode: 422);
    }
    catch(IndexNotInAddressRangeException)
    {
        return Results.Json("invalid preferred index", statusCode: 400);
    }
    
});

app.MapPost("/api/registration/checkLogin/", (HttpRequest request) =>
{
    var serviceLogin = new ServiceLogin();
    var checkUser = serviceLogin.CheckUser(request.Body);
    return Results.Json(new{isValid = checkUser, userId = serviceLogin.GetUserId()});
});
    





app.Run();
