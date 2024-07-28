using Livros.Application.Service;
using Livros.Application.UseCases;
using Livros.Domain.Interfaces.Repository;
using Livros.Domain.Service;
using Livros.Infra.CrossCutting.Configuration;
using Livros.Infra.Data;
using Livros.Infra.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;


namespace Livro.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddMediatR(typeof(AssuntoInsertCommandHandler));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddFastReport();

            //Repository Interface
            services.AddScoped<IRepositoryAssunto, RepositoryAssunto>();
            services.AddScoped<IRepositoryAutor, RepositoryAutor>();
            services.AddScoped<IRepositoryLivro, RepositoryLivro>();
            services.AddScoped<IRepositoryLivro_Autor, RepositoryLivro_Autor>();
            services.AddScoped<IRepositoryLivro_Assunto, RepositoryLivro_Assunto>();
            services.AddScoped<IRepositoryPreco, RepositoryPreco>();

            services.AddTransient<ITokenService, TokenService>();
            

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                    Configuration.GetSection("TokenConfigurations")
                )
                .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseFastReport();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
