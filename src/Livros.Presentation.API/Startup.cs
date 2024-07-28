using Amazon.S3;
using Jaeger;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTracing;
using OpenTracing.Util;
using System;
using System.Globalization;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Livros.Application.Service;
using Livros.Domain.Interfaces.Repository;
using Livros.Domain.Service;
using Livros.Infra.CrossCutting.Configuration;
using Livros.Infra.Data;
using Livros.Infra.Data.Repositories;
using Livros.Presentation.API.UseCases;
using Livros.Application.UseCases;

namespace Livros.Presentation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add Health Check
            services.AddHealthChecks();

            services.AddControllers();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMediatR(typeof(AssuntoInsertCommandHandler));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Repository Interface
            services.AddScoped<IRepositoryAssunto, RepositoryAssunto>();
            services.AddScoped<IRepositoryAutor, RepositoryAutor>();
            services.AddScoped<IRepositoryLivro, RepositoryLivro>();
            services.AddScoped<IRepositoryLivro_Autor, RepositoryLivro_Autor>();
            services.AddScoped<IRepositoryLivro_Assunto, RepositoryLivro_Assunto>();
            services.AddScoped<IRepositoryPreco, RepositoryPreco>();


            services.AddTransient<ITokenService, TokenService>();
            
            //Assunto
            services.AddTransient<InsertAssuntoPresenter>();
            services.AddTransient<UpdateAssuntoPresenter>();
            services.AddTransient<GetAssuntoPresenter>();
            services.AddTransient<GetByIdAssuntoPresenter>();
            services.AddTransient<DeleteAssuntoPresenter>();
            //services.AddTransient<GetFiltersAssuntoPresenter>();

            //Autor
            services.AddTransient<InsertAutorPresenter>();
            services.AddTransient<UpdateAutorPresenter>();
            services.AddTransient<GetAutorPresenter>();
            services.AddTransient<GetByIdAutorPresenter>();
            services.AddTransient<DeleteAutorPresenter>();
            //services.AddTransient<GetFiltersAutorPresenter>();

            //Livros
            services.AddTransient<InsertLivroPresenter>();
            services.AddTransient<UpdateLivroPresenter>();
            services.AddTransient<GetLivroPresenter>();
            services.AddTransient<GetByIdLivroPresenter>();
            services.AddTransient<DeleteLivroPresenter>();
            //services.AddTransient<GetFiltersLivroPresenter>();
                       
            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Livros"
                });
            });

            services.AddHttpClient("Vixi", c =>
            {
                c.BaseAddress = new Uri("http://localhost:5001/api/");
            }).AddHttpMessageHandler<InjectOpenTracingHeaderHandler>();

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                    Configuration.GetSection("TokenConfigurations")
                )
                .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfigurations.Issuer,
                    ValidAudience = tokenConfigurations.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });


            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "Vixi",
            //                      builder =>
            //                      {
            //                          builder.WithOrigins("http://localhost:5000",
            //                                              "https://localhost:5001",
            //                                              "*");
            //                      });
            //});


            //services.AddCors(options => options.AddDefaultPolicy(builder =>
            //{
            //    builder.AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .SetIsOriginAllowed(origin => true)
            //    .AllowCredentials();
            //}));



            services.AddSingleton<ITracer>(serviceProvider =>
            {
                var serviceName = serviceProvider
                    .GetRequiredService<IWebHostEnvironment>()
                    .ApplicationName;

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                var tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .Build();

                // Allows code that can't use DI to also access the tracer.
                GlobalTracer.Register(tracer);

                return tracer;
            });

            services.AddAWSService<IAmazonS3>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Health check path
            app.UseHealthChecks("/check");

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Livros V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHealthChecks("/status-json",
                new HealthCheckOptions()
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonSerializer.Serialize(
                            new
                            {
                                currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                statusApplication = report.Status.ToString(),
                                version = "1.1.3",
                            });

                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });

            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true) // allow any origin
              .AllowCredentials()); // allow credentials

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
