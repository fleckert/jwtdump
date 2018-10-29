using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;

class Program
{
    static void Main(string[] args)
    {
        if (args?.Length != 1 || string.IsNullOrWhiteSpace(args[0]))
        {
            Console.WriteLine("usage: jwtdump <bearer_token>");
            return;
        }

        string bearerToken = args[0];

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        if (!jwtSecurityTokenHandler.CanReadToken(bearerToken))
        {
            Console.WriteLine($"{jwtSecurityTokenHandler.GetType().FullName}.{nameof(jwtSecurityTokenHandler.CanReadToken)}(...) is false.");
            return;
        }

        JwtSecurityToken jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(bearerToken);

        string json = JsonConvert.SerializeObject(new
        {
            jwtSecurityToken.Payload,
            jwtSecurityToken.ValidFrom,
            jwtSecurityToken.ValidTo
        }, Formatting.Indented);

        Console.WriteLine(Environment.NewLine + json);
    }
}

