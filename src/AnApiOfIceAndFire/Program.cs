using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Books;
using AnApiOfIceAndFire.Data.Characters;
using AnApiOfIceAndFire.Data.Houses;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AnApiOfIceAndFire
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            
            builder.Services.AddResponseCaching();
            builder.Services.AddMemoryCache();
            builder.Services.AddResponseCompression();

            builder.Services.Configure<ConnectionOptions>(builder.Configuration.GetSection("ConnectionStrings"));

            builder.Services.AddSingleton<IPagingLinksFactory<BookFilter>, BookPagingLinksFactory>();
            builder.Services.AddSingleton<IPagingLinksFactory<CharacterFilter>, CharacterPagingLinksFactory>();
            builder.Services.AddSingleton<IPagingLinksFactory<HouseFilter>, HousePagingLinksFactory>();

            builder.Services.AddSingleton<IModelMapper<BookEntity, Book>, BookMapper>();
            builder.Services.AddSingleton<IModelMapper<CharacterEntity, Character>, CharacterMapper>();
            builder.Services.AddSingleton<IModelMapper<HouseEntity, House>, HouseMapper>();

            builder.Services.AddSingleton<IEntityRepository<BookEntity, BookFilter>, BookRepository>();
            builder.Services.AddSingleton<IEntityRepository<CharacterEntity, CharacterFilter>, CharacterRepository>();
            builder.Services.AddSingleton<IEntityRepository<HouseEntity, HouseFilter>, HouseRepository>();

            builder.Services.Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler(exceptionHandlerApp =>
                {
                    exceptionHandlerApp.Run(async context =>
                    {
                        var result = JsonSerializer.Serialize("Something went terribly wrong! https://www.youtube.com/watch?v=t3otBjVZzT0");
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                        await context.Response.WriteAsync(result);

                    });
                });

                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors(cors =>
            {
                cors.AllowAnyOrigin().AllowAnyHeader().WithExposedHeaders("Link").WithMethods("GET", "HEAD");
            });
            app.UseResponseCompression();

            app.UseFileServer();

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}