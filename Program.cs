using Microsoft.OpenApi.Models;
using UnitOfWork;

namespace MEIBCLocationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IGeoLocationUnitOfWork, GeoLocationUnitOfWork>();
            builder.Services.AddSingleton<ICalendarUnitOfWork, CalendarUnitOfWork>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger(
                    options =>
                    {
                        options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
                        options.PreSerializeFilters.Add((swaggerDoc, httpRequest) =>
                        {
                            swaggerDoc.Servers = new List<OpenApiServer>
                            {
                                new OpenApiServer { Url = "http://localhost:81" },
                              
                            };
                        });
                    }
                );
                app.UseSwaggerUI();
           // }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(builder => builder.MapControllers());

            app.Run();
        }
    }
}
