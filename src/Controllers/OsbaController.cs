using System;
using System.Threading.Tasks;
using RedHatForumSpain2018.Infrastructure;
using Couchbase;
using Couchbase.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
        public string Show()
        {
            return _options.Value.PostgreSQLConnectionString;
        }
    }
}