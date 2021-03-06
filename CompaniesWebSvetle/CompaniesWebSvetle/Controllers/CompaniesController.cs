using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompaniesDb.Extensions;
using CompaniesDb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace CompaniesWebSvetle.Controllers
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

        [HttpGet("areas")]
        public ActionResult<IEnumerable<CompanyArea>> GetAreas()
        {
            try
            {
                return Ok(connection.ReadCompanyAreaAll());
            }
            catch (NpgsqlException e)
            {
                logger.LogError(e, $"Erroc calling {CompanyAreaReadAll.Name}");
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("read/{id}")]
        public ActionResult<Company> GetById(int id)
        {
            try
            {
                return Ok(connection.ReadCompanyById(id));
            }
            catch (NpgsqlException e)
            {
                logger.LogError(e, $"Erroc calling {CompanyReadBy.Name}");
                return this.BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Company company)
        {
            try
            {
                company.NameNormalized = company.Name.ToLower();
                connection.CreateCompanyOnConflictDoUpdateReturning(company);
                return Ok();
            }
            catch (NpgsqlException e)
            {
                logger.LogError(e, $"Erroc calling {CompanyCreateOnConflictDoUpdateReturning.Name}");
                return this.BadRequest(e.Message);
            }
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                connection.DeleteCompanyById(id);
                return Ok();
            }
            catch (NpgsqlException e)
            {
                logger.LogError(e, $"Erroc calling {CompanyDeleteBy.Name}");
                return this.BadRequest(e.Message);
            }
        }
    }
}
