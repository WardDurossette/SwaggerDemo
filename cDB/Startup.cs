using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace cDB
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
            services.AddMvc();


            services.AddSwaggerGen(options => 
                options.SwaggerDoc("v1", new Info { Title = "cDB", Version = "v1"})
                );


            services.Configure<IISOptions>(options =>
            { 
                options.ForwardClientCertificate = false;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment() || env.IsStaging())
            {

                string sSwaggerPath = "/SwaggerDemo/swagger/v1/swagger.json";
                if (System.Diagnostics.Debugger.IsAttached) sSwaggerPath = "/swagger/v1/swagger.json";

                app.UseSwaggerUI(options =>
                    options.SwaggerEndpoint(sSwaggerPath, "cDB")
                    );
                
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
