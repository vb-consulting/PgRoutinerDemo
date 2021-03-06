// <auto-generated />
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Norm;
using FluentAssertions;
using CompaniesDb.Extensions;
using CompaniesWebBlazor.Shared;

namespace CompaniesDbTests
{
    public class ReadCompanyAreaAllUnitTests : PostgreSqlUnitTestFixture
    {
        public ReadCompanyAreaAllUnitTests(PostgreSqlFixture fixture) : base(fixture) { }

        [Fact]
        public void ReadCompanyAreaAll_Test1()
        {
            // Arrange

            // Act
            var result = Connection.ReadCompanyAreaAll().ToList();

            // Assert
            new List<CompanyArea>
            {
                new CompanyArea{ Id = 1, Name = "IT" },
                new CompanyArea{ Id = 2, Name = "Healthcare" },
                new CompanyArea{ Id = 3, Name = "Finance" },
                new CompanyArea{ Id = 4, Name = "Trade" },
                new CompanyArea{ Id = 5, Name = "AI" },
                new CompanyArea{ Id = 6, Name = "Services" },
                new CompanyArea{ Id = 7, Name = "Marketing" },
                new CompanyArea{ Id = 8, Name = "Production" },
                new CompanyArea{ Id = 9, Name = "Manufacturing" },
                new CompanyArea{ Id = 10, Name = "Transportation" },
            }.Should().BeEquivalentTo(result);
        }
    }
}
