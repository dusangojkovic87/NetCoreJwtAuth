using Microsoft.EntityFrameworkCore;
using NetCoreJwtAuth.DataContext;
using NetCoreJwtAuth.Services.IRepository;
using NetCoreJwtAuth.Services.Repository;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        {
            builder.Services.AddDbContext<AplicationDbContext>(opt =>
            {
                opt.UseSqlServer(@"Server=Dusan; Database=jwtTest; Trusted_Connection=True;");
            });
            builder.Services.AddScoped<IAuthenticate, Authenticate>();



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();

        }

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(o =>
        {
            o.AllowAnyHeader();
            o.AllowAnyOrigin();
            o.AllowAnyMethod();


        });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}