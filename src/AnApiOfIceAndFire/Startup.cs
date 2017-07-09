using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Books;
using AnApiOfIceAndFire.Data.Characters;
using AnApiOfIceAndFire.Data.Houses;
using AnApiOfIceAndFire.Infrastructure;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Infrastructure.Versioning;
using AnApiOfIceAndFire.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AnApiOfIceAndFire
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc().AddJsonOptions(opts =>
            {
                //Use indented to make it more readable for the consumer, using gzip is better for bandwidth anyway.
                opts.SerializerSettings.Formatting = Formatting.Indented;

                //Use camelCase for naming of properties since it's more of a standard
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                //Use the ISO format instead of Microsoft format. This is to make it easier for the consumer to parse the date, especially if they don't use a Microsoft stack themselves.
                opts.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;

                //We want to represent our enums with their names instead of their numerical values. This is to make it more readable for the consumer.
                opts.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new AcceptHeaderVersionReader();
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                options.CreateBadRequest = (request, code, message, detail) => new BadRequestObjectResult(new { message = "Given API version is not supported" });
            });

            services.Configure<ConnectionOptions>(Configuration.GetSection("ConnectionStrings"));

            services.AddSingleton<IPagingLinksFactory<BookFilter>, BookPagingLinksFactory>();
            services.AddSingleton<IPagingLinksFactory<CharacterFilter>, CharacterPagingLinksFactory>();
            services.AddSingleton<IPagingLinksFactory<HouseFilter>, HousePagingLinksFactory>();

            services.AddSingleton<IModelMapper<BookEntity, Book>, BookMapper>();
            services.AddSingleton<IModelMapper<CharacterEntity, Character>, CharacterMapper>();
            services.AddSingleton<IModelMapper<HouseEntity, House>, HouseMapper>();

            services.AddSingleton<IEntityRepository<BookEntity, BookFilter>, BookRepository>();
            services.AddSingleton<IEntityRepository<CharacterEntity, CharacterFilter>, CharacterRepository>();
            services.AddSingleton<IEntityRepository<HouseEntity, HouseFilter>, HouseRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //config.EnableCors(cors);
            app.UseCors(cors =>
            {
                cors.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET", "HEAD");
            });

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseMvc();
        }
    }
}
