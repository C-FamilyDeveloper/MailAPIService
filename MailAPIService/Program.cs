using MailerAPIService.Models.DataContexts;
using MailerAPIService.Models.DataEntities;
using MailerAPIService.Models.Interfaces;
using MailerAPIService.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MailAPIService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            string connection = builder.Configuration.GetConnectionString("Connection")!;
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            builder.Services.AddScoped<IBaseRepository<MailLog>, MailLogRepository>();
            builder.Services.AddScoped<IBaseRepository<MailMessage>, MessageRepository>();
            builder.Services.AddScoped<IBaseRepository<Recipient>, RecipientRepository>();
            builder.Services.AddScoped<IBaseRepository<MessageRecipient>, MessageRecipientRepository>();
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