using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace RedHatForumSpain2018.Infrastructure
{
    public class PostgreSQLOptions
    {
        public string PostgreSQLUserID { get; set; } = "";
        public string PostgreSQLPassword { get; set; } = "";
        public string PostgreSQLHost { get; set; } = "localhost";
        public string PostgreSQLPort { get; set; } = "5432";
        public string PostgreSQLDatabase { get; set; } = "";
        public string PostgreSQLPooling { get; set; } = "true";
    }
}