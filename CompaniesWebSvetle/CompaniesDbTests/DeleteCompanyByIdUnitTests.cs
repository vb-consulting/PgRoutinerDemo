// <auto-generated />
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Norm;
using FluentAssertions;
using CompaniesDb.Extensions;
using CompaniesDb.Models;

namespace CompaniesDbTests
{
    public class DeleteCompanyByIdUnitTests : PostgreSqlUnitTestFixture
    {
        public DeleteCompanyByIdUnitTests(PostgreSqlFixture fixture) : base(fixture) { }

        [Fact]
        public void DeleteCompanyById_Test1()
        {
            // Arrange
            var model = Connection.CreateCompanyOnConflictDoUpdateReturning(new Company { Name = "n", NameNormalized = "nn", About = "about", AreaId = 1, Website = "website" });
            long id = model.Id;

            // Act
            Connection.DeleteCompanyById(id);

            // Assert
            Assert.Null(Connection.ReadCompanyById(id));
        }
    }
}