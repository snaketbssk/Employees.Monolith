using SnakeTBs.Configurations.Parsers.Entities;
using SnakeTBs.Configurations.Statics.Entities;
using System;

namespace Employees.Monolith.Configurations.Entities
{
    public static class AppConfiguration
    {
        public static void Set(params string[] args)
        {
            var parser = new ArgumentsParser('=', args);
            if (parser.ContainsKey(EnvironmentVariablesStatic.AspNetCoreBranch))
            {
                Environment.SetEnvironmentVariable(
                    variable: EnvironmentVariablesStatic.AspNetCoreBranch,
                    value: parser.GetValue(EnvironmentVariablesStatic.AspNetCoreBranch));
            }
        }
    }
}
