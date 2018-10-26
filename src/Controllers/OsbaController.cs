using System;
using System.Threading.Tasks;
using RedHatForumSpain2018.Infrastructure;
using Couchbase;
using Couchbase.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Dapper;
using Npgsql;

namespace RedHatForumSpain2018.Controllers
{
    [Route("api/osba")]
    public class OsbaController : Controller
    {
        IOptions<PostgreSQLOptions> _options;

        public OsbaController(IOptions<PostgreSQLOptions> options)
        {
            _options = options;
        }

        [HttpGet("show")]
        public async Task<string> Show()
        {
            var connectionString = $"User ID={_options.Value.PostgreSQLUserID};Password={_options.Value.PostgreSQLPassword};Host={_options.Value.PostgreSQLHost};Port={_options.Value.PostgreSQLPort};Database={_options.Value.PostgreSQLDatabase};Pooling={_options.Value.PostgreSQLPooling};SslMode=Require";
            var connection = new NpgsqlConnection(connectionString);
            using (connection)
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteScalarAsync("select version();");
                return result.ToString();
            }
        }
    }
}