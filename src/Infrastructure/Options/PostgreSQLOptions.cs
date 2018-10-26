using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace RedHatForumSpain2018.Infrastructure
{
    public class PostgreSQLOptions
    {
        public string PostgreSQLConnectionString { get; set; } = "";
    }
}