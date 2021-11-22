using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Employees.Monolith.Configurations.Models.Branches.Entities
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

    public class Origin
    {
        [JsonPropertyName("Url")]
        public string Url { get; set; }
    }

    public class Cors
    {
        [JsonPropertyName("Origins")]
        public List<Origin> Origins { get; set; }
    }

    public class Database
    {
        [JsonPropertyName("Connection")]
        public string Connection { get; set; }
    }

    public class Swagger
    {
        [JsonPropertyName("IsActive")]
        public bool IsActive { get; set; }
    }

    public class RootBranch
    {
        [JsonPropertyName("Logging")]
        public Logging Logging { get; set; }

        [JsonPropertyName("AllowedHosts")]
        public string AllowedHosts { get; set; }

        [JsonPropertyName("Cors")]
        public Cors Cors { get; set; }

        [JsonPropertyName("Database")]
        public Database Database { get; set; }

        [JsonPropertyName("Swagger")]
        public Swagger Swagger { get; set; }
    }


}
