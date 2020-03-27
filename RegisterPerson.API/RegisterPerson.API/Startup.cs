using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegisterPerson.API.Services.Context.Implementation;
using RegisterPerson.DataAccess.Abstract.Entities;
using RegisterPerson.DataAccess.SqlServer.Context;
using RegisterPerson.Domain.Services.Implementation;
using RegisterPerson.Domain.Services.Interfaces;

namespace RegisterPerson.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Registra o banco de dados
            var connection = Configuration["SQLServerConnection:SQLServerConnectionString"];
            //var optionsBuilder = new DbContextOptionsBuilder<SQLServerContext>();
            
            services.AddDbContext<SQLServerContext>(options => options.UseSqlServer(connection));

            // Registra as dependências
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonServiceSqlServer, PersonServiceSqlServer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
