namespace MoviePosterDb.IntegrationTests
{
    using System;
    using System.Configuration;

    internal static class Configuration
    {
        public static string Get(string key)
        {
            return Environment.GetEnvironmentVariable(key) ??
                   ConfigurationManager.AppSettings[key];
        }
    }
}