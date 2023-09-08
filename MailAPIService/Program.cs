using MailAPIService.Models.Abstractions;
using MailAPIService.Models.Configs;
using MailAPIService.Models.DataContexts;
using MailAPIService.Models.DataEntities;
using MailAPIService.Models.Interfaces;
using MailAPIService.Models.Repositories;
using MailAPIService.Models.Services;
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
            builder.Services.AddScoped<IBaseRepository<MailLog>, MailLogRepository>();
            builder.Services.AddScoped<IBaseRepository<MailMessage>, MessageRepository>();
            builder.Services.AddScoped<IBaseRepository<Recipient>, RecipientRepository>();
            builder.Services.AddScoped<IBaseRepository<MessageRecipient>, MessageRecipientRepository>();
            builder.Services.AddScoped<IMailService, MailService>();
            builder.Services.Configure<Config>(builder.Configuration);
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