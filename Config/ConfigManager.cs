using Config.Interfaces;
using Microsoft.Extensions.Configuration;
using Shared;

namespace Config
{
    public class ConfigManager
    {
        private readonly IConfigurationBuilder configuration;
        public DatabaseConfig dbConfig;
        public LoginServerConfig loginServerConfig;
        public CharServerConfig charServerConfig;
        public EmulatorConfig emulatorConfig;

        public ConfigManager()
        {
            string pathConfig = "App/Config";
            string basePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.ToString(), pathConfig);

            if(basePath != null)
            {
                configuration = new ConfigurationBuilder().SetBasePath(basePath);
                this.getDatabaseConfig();
                this.getLoginServerConfig();
                this.getCharServerConfig();
                this.getEmulatorConfig();
            } else
            {
                throw new InvalidOperationException("Unable to determine base path");
            }

        }

        public IConfigurationRoot LoadJsonFile(string fileName)
        {
            try
            {
                return configuration.AddJsonFile(fileName, false, true).Build();
            } catch (Exception ex)
            {
                Display.Error($"Load {fileName} is not possible. Exception: {ex.Message}");
                throw;
            }
        }

        private void getDatabaseConfig()
        {
            IConfigurationRoot dbConfigJson = this.LoadJsonFile("database.json");
            DatabaseConfig databaseConfig = new DatabaseConfig
            {
                host = dbConfigJson["host"]!,
                username = dbConfigJson["username"]!,
                password = dbConfigJson["password"]!,
                databaseName = dbConfigJson["dbname"]!,
                port = Int32.Parse(dbConfigJson["port"]!)
            };
            this.dbConfig = databaseConfig;
        }

        private void getLoginServerConfig()
        {
            IConfigurationRoot dbConfigJson = this.LoadJsonFile("login-server.json");
            LoginServerConfig lsConfig = new LoginServerConfig
            {
                port = Int32.Parse(dbConfigJson["port"]!),
                Ip = dbConfigJson["ip"]!,
                EnabledUserCount = Boolean.Parse(dbConfigJson["usercount_enabled"]!),
                UserCountLow = Int32.Parse(dbConfigJson["usercount_low"]!),
                UserCountMedium = Int32.Parse(dbConfigJson["usercount_medium"]!),
                UserCountHigh = Int32.Parse(dbConfigJson["usercount_high"]!),
                ServerType = Int32.Parse(dbConfigJson["server_type"]!),
                ShowDisplayAsNew = Int32.Parse(dbConfigJson["display_as_new"]!)
            };
            this.loginServerConfig = lsConfig;
        }

        private void getCharServerConfig()
        {
            IConfigurationRoot dbConfigJson = this.LoadJsonFile("char-server.json");
            CharServerConfig lsConfig = new CharServerConfig
            {
                Port = Int32.Parse(dbConfigJson["port"]!),
                Ip = dbConfigJson["ip"]!
            };
            this.charServerConfig = lsConfig;
        }

        private void getEmulatorConfig()
        {
            IConfigurationRoot dbConfigJson = this.LoadJsonFile("emulator-config.json");
            EmulatorConfig emuConfig = new EmulatorConfig
            {
                ServerName = dbConfigJson["server_name"]!
            };
            this.emulatorConfig = emuConfig;
        }
    }
}