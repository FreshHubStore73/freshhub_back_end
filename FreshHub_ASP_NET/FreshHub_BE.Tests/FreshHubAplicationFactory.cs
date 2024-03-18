using FluentAssertions.Common;
using FreshHub_BE.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshHub_BE.Tests
{
    //internal class FreshHubAplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    //{
    //    public Task InitializeAsync()
    //    {           
    //        var appDbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseMySql(Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0))));
    //    }

    //    Task IAsyncLifetime.DisposeAsync()
    //    {
    //        throw new NotImplementedException();
            
    //    }
    //    protected override void ConfigureWebHost(IWebHostBuilder builder)
    //    {
    //        base.ConfigureWebHost(builder);
    //        var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<String, String> 
    //        {
    //            ["ConectionStrings:DefaultConection"] = ""
    //        });
    //    }
    //}
}
