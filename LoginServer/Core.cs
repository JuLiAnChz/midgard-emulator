using Core.DataBase.Data;
using Core.DataBase.Models;
using Shared;
using System;
using Config;
using SimpleTCP;
using System.Net;

namespace LoginServer
{
    public static class Core
    {
        public static ApplicationDbContext? dbConn;
        public static ConfigManager? config;
        public static SimpleTcpClient charServerClient;

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

            try
            {
                Display.Info("Login server is starting...");
                Server LoginServer = new Server(config.loginServerConfig.port);
                Display.Success($"Login server is running on {config.loginServerConfig.Ip}:{config.loginServerConfig.port}!");
                #region ConnectCharServer
                Display.Info("Char server connecting...");
                int tryConnect = 0;

                while (tryConnect < 10)
                {
                    try
                    {
                        charServerClient = new SimpleTcpClient().Connect(IPAddress.Parse(Core.config!.charServerConfig.Ip).ToString(), Core.config!.charServerConfig.Port);

                        if (charServerClient != null && charServerClient.TcpClient != null && charServerClient.TcpClient.Connected)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Display.Error($"Error trying({tryConnect}) connect to Char server: {ex.Message}");
                    }

                    tryConnect++;

                    Thread.Sleep(1000);
                }

                if (charServerClient == null || charServerClient.TcpClient == null || !charServerClient.TcpClient.Connected)
                {
                    Display.Error("Char server not found.");
                } else
                {
                    Display.Success("Char server connected.");
                }
            }
            #endregion
            catch (Exception ex)
            {
                Display.Error(ex.ToString());
            }
        }
    }
}
