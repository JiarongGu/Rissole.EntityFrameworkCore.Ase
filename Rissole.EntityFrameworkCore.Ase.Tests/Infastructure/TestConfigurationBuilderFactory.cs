using Microsoft.Extensions.Configuration;

namespace Rissole.EntityFrameworkCore.Ase.Tests
{
    internal class TestConfigurationBuilderFactory
    {
        public IConfigurationBuilder Create()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            configurationBuilder.AddUserSecrets("aseSecrets");

            return configurationBuilder;
        }
    }
}
