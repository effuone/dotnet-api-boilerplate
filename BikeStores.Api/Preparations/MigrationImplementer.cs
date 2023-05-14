using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStores.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace BikeStores.Api.Preparations
{
    public static class MigrationImplementer
    {
        public static async Task PrepPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                await SeedData(serviceScope.ServiceProvider.GetService<BikeStoresContext>());
            }
        }
        public static async Task SeedData(BikeStoresContext context)
        {
            System.Console.WriteLine("Applying migrations...");
            await context.Database.MigrateAsync();
        }
    }
}