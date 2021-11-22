using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Employees.Monolith.Configurations.Models.AppSettings.Entities
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class LogLevel
    {
        [JsonPropertyName("Default")]
        public string Default { get; set; }

        [JsonPropertyName("Microsoft")]
        public string Microsoft { get; set; }

        [JsonPropertyName("Microsoft.Hosting.Lifetime")]
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class Logging
    {
        [JsonPropertyName("LogLevel")]
        public LogLevel LogLevel { get; set; }
    }

    public class File
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Tag")]
        public string Tag { get; set; }
    }

    public class AppSettings
    {
        [JsonPropertyName("Tag")]
        public string Tag { get; set; }

        [JsonPropertyName("Files")]
        public List<File> Files { get; set; }
    }

    public class Token
    {
        [JsonPropertyName("ExpiredAt")]
        public int ExpiredAt { get; set; }
    }

    public class Authentications
    {
        [JsonPropertyName("Token")]
        public Token Token { get; set; }
    }

    public class Swagger
    {
        [JsonPropertyName("IsAuthentication")]
        public bool IsAuthentication { get; set; }

        [JsonPropertyName("XmlFile")]
        public string XmlFile { get; set; }
    }

    public class RootAppSetting
    {
        [JsonPropertyName("Logging")]
        public Logging Logging { get; set; }

        [JsonPropertyName("AllowedHosts")]
        public string AllowedHosts { get; set; }

        [JsonPropertyName("AppSettings")]
        public AppSettings AppSettings { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("Authentications")]
        public Authentications Authentications { get; set; }

        [JsonPropertyName("Swagger")]
        public Swagger Swagger { get; set; }
    }
}
