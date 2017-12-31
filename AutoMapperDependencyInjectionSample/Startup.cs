using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AutoMapperDependencyInjectionSample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper
            services.AddAutoMapper(typeof(Startup).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                // Get AutoMapper
                var mapper = context.RequestServices.GetRequiredService<IMapper>();

                // Build & map the models
                var source = new SomeModel { Name = "Some cool name" };
                var destination = mapper.Map<SomeOtherModel>(source);

                // Write the result as JSON
                var result = JsonConvert.SerializeObject(new { source, destination });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            });
        }
    }
}
