using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AbpDynamicResourceManagement.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class AbpDynamicResourceManagementDbContextFactory : IDesignTimeDbContextFactory<AbpDynamicResourceManagementDbContext>
{
    public AbpDynamicResourceManagementDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        AbpDynamicResourceManagementEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<AbpDynamicResourceManagementDbContext>()
            .UseSqlite(configuration.GetConnectionString("Default"));
        
        return new AbpDynamicResourceManagementDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AbpDynamicResourceManagement.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
