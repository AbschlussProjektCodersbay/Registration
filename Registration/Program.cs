using Registration.repo;
using Registration.service;
using Registration.service.Exception;

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
        Console.WriteLine(e);
        return Results.Conflict("It is not possible to create two users with the same E-mail");
    }
    catch (InvalidPasswordException e)
    {
        Console.WriteLine(e);
        return Results.Conflict("The password is unsave");
    }
    catch (InvalidMailSyntaxException e)
    {
        Console.WriteLine(e);
        return Results.Conflict("The Mail have a invalid Syntax");
    }
    
    
    
    return Results.Json("Successful Registration",statusCode: 200);
});

app.Run();
