// <auto-generated />
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Norm;
using NpgsqlTypes;
using Npgsql;
using CompaniesWebBlazor.Shared;

namespace CompaniesDb.Extensions
{
    public static class CompaniesCreateOnConflictDoUpdateReturning
    {
        public const string Name = "companies";

        public static string Sql(Company model, params string[] conflictedFields) => $@"
            INSERT INTO ""companies""
            (
                ""id"",
                ""name"",
                ""name_normalized"",
                ""website"",
                ""area_id"",
                ""about"",
                ""modified""
            )
            {(model.Id != default ? "OVERRIDING SYSTEM VALUE" : "")}
            VALUES
            (
                {(model.Id == default ? "DEFAULT" : "@id")},
                @name,
                @nameNormalized,
                @website,
                @areaId,
                @about,
                {(model.Modified == default ? "DEFAULT" : "@modified")}
            )
            ON CONFLICT ({string.Join(", ", conflictedFields)})
            DO UPDATE SET
                ""name"" = EXCLUDED.""name"",
                ""name_normalized"" = EXCLUDED.""name_normalized"",
                ""website"" = EXCLUDED.""website"",
                ""area_id"" = EXCLUDED.""area_id"",
                ""about"" = EXCLUDED.""about"",
                ""modified"" = EXCLUDED.""modified""
            RETURNING
                ""id"",
                ""name"",
                ""name_normalized"",
                ""website"",
                ""area_id"",
                ""about"",
                ""modified""";

        /// <summary>
        /// Insert new record in table ""companies"" with values instance of a "CompaniesDb.Extensions.Company" class and return updated record mapped to an instance of a "CompaniesDb.Extensions.Company" class.
        /// Fields with defined default values id, modified will have the default when null value is supplied.
        /// When conflict occures, update with provided model.
        /// </summary>
        /// <param name="model">Instance of a "CompaniesDb.Extensions.Company" model class.</param>
        /// <param name="conflictedFields">Params list of field names that are tested for conflict. Default is list of primary keys.</param>
        /// <returns>Single instance of a "CompaniesDb.Extensions.Company" class that is mapped to resulting record of table ""companies""</returns>
        public static Company CreateOnConflictDoUpdateReturningCompanies(this NpgsqlConnection connection, Company model, params string[] conflictedFields)
        {
            return connection
                .Prepared()
                .Read<Company>(Sql(model, conflictedFields.Length == 0 ? new string[] { "id" } : conflictedFields), 
                    ("id", model.Id, NpgsqlDbType.Bigint),
                    ("name", model.Name, NpgsqlDbType.Varchar),
                    ("name_normalized", model.NameNormalized, NpgsqlDbType.Varchar),
                    ("website", model.Website, NpgsqlDbType.Varchar),
                    ("area_id", model.AreaId, NpgsqlDbType.Integer),
                    ("about", model.About, NpgsqlDbType.Varchar),
                    ("modified", model.Modified, NpgsqlDbType.Timestamp))
                .SingleOrDefault();
        }

        /// <summary>
        /// Insert new record in table ""companies"" with values instance of a "CompaniesDb.Extensions.Company" class and return updated record mapped to an instance of a "CompaniesDb.Extensions.Company" class.
        /// Fields with defined default values id, modified will have the default when null value is supplied.
        /// When conflict occures, update with provided model.
        /// </summary>
        /// <param name="model">Instance of a "CompaniesDb.Extensions.Company" model class.</param>
        /// <param name="conflictedFields">Params list of field names that are tested for conflict. Default is list of primary keys.</param>
        /// <returns>Single instance of a "CompaniesDb.Extensions.Company" class that is mapped to resulting record of table ""companies""</returns>
        public static async ValueTask<Company> CreateOnConflictDoUpdateReturningCompaniesAsync(this NpgsqlConnection connection, Company model, params string[] conflictedFields)
        {
            return await connection
                .Prepared()
                .ReadAsync<Company>(Sql(model, conflictedFields.Length == 0 ? new string[] { "id" } : conflictedFields), 
                    ("id", model.Id, NpgsqlDbType.Bigint),
                    ("name", model.Name, NpgsqlDbType.Varchar),
                    ("name_normalized", model.NameNormalized, NpgsqlDbType.Varchar),
                    ("website", model.Website, NpgsqlDbType.Varchar),
                    ("area_id", model.AreaId, NpgsqlDbType.Integer),
                    ("about", model.About, NpgsqlDbType.Varchar),
                    ("modified", model.Modified, NpgsqlDbType.Timestamp))
                .SingleOrDefaultAsync();
        }
    }
}
