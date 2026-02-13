
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TickVento.Application.Abstractions.Payments;
using TickVento.Application.Abstractions.Persistence;
using TickVento.Infrastructure.Payments;
using TickVento.Infrastructure.Persistence.Data;
using TickVento.Infrastructure.Persistence.Repositories;
using TickVento.Infrastructure.Persistence.UnitOfWork;

namespace TickVento.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Register DbContext
            builder.Services.AddDbContext<TickVentoDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("TickVentoConnectionString")
                )
            );
            // Register Repositories 
            builder.Services.AddScoped<IUserRepository,UserRepository>();
            builder.Services.AddScoped<IEventRepository,EventRepository>();
            builder.Services.AddScoped<IBookingRepository,BookingRepository>();
            builder.Services.AddScoped<IPaymentRepository,PaymentRepository>();
           
            // Register UnitOfWork
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            
            // Register Payment Gateway
            builder.Services.AddScoped<IPaymentGateway,SandboxPaymentGateway>();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
