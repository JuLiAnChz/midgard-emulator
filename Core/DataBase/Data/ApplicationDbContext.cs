using Core.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;

namespace Core.DataBase.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected string _databaseServer;
        protected string _databaseUserName;
        protected string _databasePassword;
        protected string _databaseName;
        protected int _databasePort;

        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Character>? Characters { get; set; }

        public ApplicationDbContext(string host, string username, string password, string dbname, int port) {
            _databaseServer = host;
            _databaseUserName = username;
            _databasePassword = password;
            _databaseName = dbname;
            _databasePort = port;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                string connectionString = $"Server={_databaseServer},{_databasePort}; User ID={_databaseUserName}; Password={_databasePassword}; Database={_databaseName}";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            } catch (Exception ex)
            {
                Display.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
