// <auto-generated />
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Norm;
using NpgsqlTypes;
using Npgsql;

namespace CompaniesDb.Extensions
{
    public static class PgRoutineSearchCompanies
    {
        public const string Name = "search_companies";

        /// <summary>
        /// Executes plpgsql function "search_companies"
        /// 
        /// Search companies by filter and return data page with results.
        /// Parameters:
        /// - `_filter` is `json` with following schema `{"search", "area_id"}`
        /// - `page` page indexed from 1
        /// - `page_size`, default is 25
        /// 
        /// Returning json schema:
        /// `{"count", {"id", "name", "website", "area", "about", "modified"}}`
        /// 
        /// </summary>
        /// <param name="filter">_filter json</param>
        /// <param name="page">_page integer</param>
        /// <param name="pageSize">_page_size integer</param>
        /// <returns>string</returns>
        public static string SearchCompanies(this NpgsqlConnection connection, string filter, int? page, int? pageSize)
        {
            return connection
                .AsProcedure()
                .Read<string>(Name,
                    ("_filter", filter, NpgsqlDbType.Json),
                    ("_page", page, NpgsqlDbType.Integer),
                    ("_page_size", pageSize, NpgsqlDbType.Integer))
                .SingleOrDefault();
        }

        /// <summary>
        /// Asynchronously executes plpgsql function "search_companies"
        /// 
        /// Search companies by filter and return data page with results.
        /// Parameters:
        /// - `_filter` is `json` with following schema `{"search", "area_id"}`
        /// - `page` page indexed from 1
        /// - `page_size`, default is 25
        /// 
        /// Returning json schema:
        /// `{"count", {"id", "name", "website", "area", "about", "modified"}}`
        /// 
        /// </summary>
        /// <param name="filter">_filter json</param>
        /// <param name="page">_page integer</param>
        /// <param name="pageSize">_page_size integer</param>
        /// <returns>ValueTask whose Result property is string</returns>
        public static async ValueTask<string> SearchCompaniesAsync(this NpgsqlConnection connection, string filter, int? page, int? pageSize)
        {
            return await connection
                .AsProcedure()
                .ReadAsync<string>(Name,
                    ("_filter", filter, NpgsqlDbType.Json),
                    ("_page", page, NpgsqlDbType.Integer),
                    ("_page_size", pageSize, NpgsqlDbType.Integer))
                .SingleOrDefaultAsync();
        }
    }
}
