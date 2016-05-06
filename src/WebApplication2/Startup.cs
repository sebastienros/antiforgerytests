using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IAntiforgery antiforgery)
        {
            app.Use(next => context =>
            {
                var tokens = antiforgery.GetAndStoreTokens(context);
                context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions() { HttpOnly = false });

                return next(context);
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();


            app.Map("/Comment", a => a.Run(async context =>
            {
                if (string.Equals("GET", context.Request.Method, StringComparison.OrdinalIgnoreCase))
                {
                    await context.Response.WriteAsync("GET called");
                }
                else if (string.Equals("POST", context.Request.Method, StringComparison.OrdinalIgnoreCase))
                {
                    await antiforgery.ValidateRequestAsync(context);

                    await context.Response.WriteAsync("POST called");
                }
            }));
        }
    }
}
