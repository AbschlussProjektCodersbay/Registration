using System;
using System.Security.Cryptography;
using System.Text;

namespace Registration.service;

public class HashManager 
{
 
    
    public static string CreateHash( string input)
    {
        
        byte[] data = System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(input));

        var sBuilder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();
    }

    public static bool VerifyHash( string input, string hash)
    {
        var hashOfInput = System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(input));
        Console.WriteLine("test1");
        var comparer = StringComparer.OrdinalIgnoreCase;
        return comparer.Compare(hashOfInput, hash) == 0;
    }
    
}