using MailAPIService.Models.Configs;
using MailAPIService.Models.DataContexts;
using MailAPIService.Models.Facades;
using MailAPIService.Models.Interfaces;
using MailAPIService.Models.Services;
using MailAPIService.Models.Services.CRUD;
using Microsoft.EntityFrameworkCore;

namespace MailAPIService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("appsettings.json");
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            string connection = builder.Configuration.GetConnectionString("Connection")!;
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            builder.Services.Configure<Config>(builder.Configuration);
            builder.Services.AddScoped<IMailService, MailService>();
            builder.Services.AddScoped<IMailLogService,MailLogService>();
            builder.Services.AddScoped<MailFacade>();
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}