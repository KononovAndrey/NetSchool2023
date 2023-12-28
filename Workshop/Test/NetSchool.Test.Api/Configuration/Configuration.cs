namespace NetSchool.Test.Api;

using System.Collections.Generic;
using System.Linq;

public class Configuration
{
    public IEnumerable<KeyValuePair<string, string>> Variables => _variables;

    public void SetVariable(string key, string value)
    {
        _variables = _variables.Where(x => x.Key != key).ToList();
        _variables.Add(Value(key, value));
    }

    private static KeyValuePair<string, string> Value(string key, string value)
    {
        return new KeyValuePair<string, string>(key, value);
    }

    private List<KeyValuePair<string, string>> _variables = new List<KeyValuePair<string, string>>
    {
        Value("Main:PublicHostUrl", "http://localhost"),
        Value("Main:InternalHostUrl", "http://localhost"),
        Value("Main:UploadFileSizeLimit", "20971520"),
        Value("Main:AllowedOrigins", ""),

        Value("Log:Level", "Error"),
        Value("Log:WriteToConsole", "false"),
        Value("Log:WriteToFile", "false"),
        Value("Log:FileRollingInterval", "false"),
        Value("Log:FileRollingSize", ""),

        Value("Database:Type", "PgSql"),
        Value("Database:ConnectionString", "Server=localhost;Port=5432;Database=test;User Id=postgres;Password=pwd;"),
        Value("Database:Init:AddDataFromFiles", "false"),
        Value("Database:Init:AddDemoData", "false"),
        Value("Database:Init:AddAdministrator", "false"),
        Value("Database:Init:Administrator:Name", ""),
        Value("Database:Init:Administrator:Email", ""),
        Value("Database:Init:Administrator:Password", ""),

        Value("Swagger:Enabled", "false"),
        Value("Swagger:OAuthClientId", ""),
        Value("Swagger:OAuthClientSecret", ""),

    }.ToList();
}