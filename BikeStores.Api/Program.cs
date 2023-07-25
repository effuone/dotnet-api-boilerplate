using Microsoft.EntityFrameworkCore;
using BikeStores.Domain.Context;
using BikeStores.Application.Interfaces;
using BikeStores.Application.Repositories;
using BikeStores.Api.Preparations;
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //configuring MSSQL database connection string from secret store
    string server = builder.Configuration["DB_SERVER"];
    string port = builder.Configuration["DB_PORT"];
    string user = builder.Configuration["DB_USER"];
    string password = builder.Configuration["DB_PASSWORD"];
    string database = builder.Configuration["DB_DATABASE"];

    //linux MSSQL docker image connection
    var connectionString = $"Server=tcp:{server}, {port};Database={database};User Id={user};Password={password}";  

    //Windows direct MSSQL connection
    // string connectionString = @$"Data Source={server};Initial Catalog={database}; Trusted_Connection=True;";

    builder.Services.AddDbContext<BikeStoresContext>(options=>options.UseSqlServer(connectionString));

        //Dependency Injection
        builder.Services.AddScoped<IBrandRepository, BrandRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IStoreRepository, StoreRepository>();
        builder.Services.AddSwaggerGen();

        //supressing async suffix in action names for retrieving object via "CreatedAtAction" method after HttpPost requests
        builder.Services.AddMvc(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        });
}
var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseHttpsRedirection();
    }
    else
    {
        app.UseHsts();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseRouting();
    app.MapControllers();
    await MigrationImplementer.PrepPopulation(app);
    app.Run();
}