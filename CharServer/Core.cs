using Config;
using Core.DataBase.Data;
using Shared;
using System;

namespace CharServer
{
    public static class Core
    {
        public static ApplicationDbContext? dbConn;
        public static ConfigManager? config;

        public static void Init()
        {
            Display.BreakLine();
            Display.Header();
            Display.BreakLine();
            #region Configuration
            Display.Info("Loading configurations...");
            config = new ConfigManager();
            Display.Success("Configuration is loaded!");
            #endregion
            #region Database
            Display.Info("Connecting to database...");
            dbConn = new ApplicationDbContext(
                host: config.dbConfig.host,
                username: config.dbConfig.username,
                password: config.dbConfig.password,
                dbname: config.dbConfig.databaseName,
                port: config.dbConfig.port
            );
            Display.Success("Database is connected!");
            #endregion
            Display.Info("Char server is starting...");
            Server charServer = new Server(config.charServerConfig.Port);
            Display.Success($"Char server is running on {config.charServerConfig.Ip}:{config.charServerConfig.Port}!");
        }
    }
}
