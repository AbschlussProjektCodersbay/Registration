using Registration.service;
using Registration.service.Exception;
using Xunit.Sdk;

namespace Registration.testing;
using Xunit;
public class TestValidateData
{
    private ValidateData _validator = new ValidateData();
    
    
    [Fact]
    void TrueValidateDataTest()
    {
        var mail = "tester@gmail.com";
        var password = "password12345";
        Assert.True(_validator.IsValid(mail,password));
    }
    
    [Fact]
    void MailSytaxExceptionValidateDataTest()
    {
        var mail = "testergmail.com";
        var password = "password12345";
        Assert.Throws<InvalidMailSyntaxException>(()=>_validator.IsValid(mail, password));
    }
    
    [Fact]
    void PasswordExceptionValidateDataTest()
    {
        var mail = "tester@gmail.com";
        var password = "12345";
        Assert.Throws<InvalidPasswordException>(()=>_validator.IsValid(mail, password));
    }

    [Fact]
    void MailExistExceptionValidateDataTest()
    {
        var mail = "test@mail.com";
        var password = "password12345";
        Assert.Throws<MailExistException>(()=>_validator.IsValid(mail, password));
    }
    
    
}