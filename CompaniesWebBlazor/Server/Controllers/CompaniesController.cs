using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompaniesDb.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace CompaniesWebBlazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ILogger<CompaniesController> logger;
        private readonly NpgsqlConnection connection;

        public CompaniesController(ILogger<CompaniesController> logger, NpgsqlConnection connection)
        {
            this.logger = logger;
            this.connection = connection;
        }

        [HttpGet("{filter}/{page}/{pageSize}")]
        public IActionResult Get(string filter, int page, int pageSize)
        {
            try
            {
                return Content(connection.SearchCompanies(filter, page, pageSize), "application/json");
            }
            catch (NpgsqlException e)
            {
                logger.LogError(e, $"Erroc calling {PgRoutineSearchCompanies.Name}");
                return this.BadRequest(e.Message);
            }
        }
    }
}
