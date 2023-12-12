using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config.Interfaces
{
    public class DatabaseConfig
    {
        public string host {  get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string databaseName { get; set; }
        public int port { get; set; }
    }
}
