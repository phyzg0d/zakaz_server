using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using NewGame4.Core;

namespace NewGame4
{
    public class Startup
    {
        public HttpContext HttpContext;
        public ServerContext Context;
        public StartController StartController;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add(new AuthorizeFilter()));
            services.AddCors(allowsites=>{allowsites.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            services.AddSignalR();
            
            // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //     .AddCookie(options =>
            //     { 
            //         options.Cookie.Name = "SimpleTalk.AuthCookieAspNetCore";
            //         options.LoginPath = "/Home/Login";
            //         options.LogoutPath = "/Home/Logout";
            //         options.Cookie.HttpOnly = true;
            //         options.Cookie.SameSite = SameSiteMode.Lax;
            //     });
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = HttpOnlyPolicy.None;
                options.CheckConsentNeeded = context => true;
            });
        }
        

        public void Configure(IApplicationBuilder app)
        {
            Context = new ServerContext();
            StartController = new StartController(Context, HttpContext);
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
            
            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseWebSockets();
            
            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseAuthentication();    
            app.UseAuthorization();
            app.UseCors(options => options.AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
            
            app.UseEndpoints(endpoints => { endpoints.MapHub<ChatHub>("/chat"); });

            // app.Run(async (context) =>
            // {
            //     if (context.Request.Cookies.ContainsKey("Name"))
            //     {
            //         string name = context.Request.Cookies[$"{_name}"];
            //         await context.Response.WriteAsync($"Hello {_name}!");
            //     }
            //     else
            //     {
            //         context.Response.Cookies.Append("Name", $"{_name}");
            //         await context.Response.WriteAsync("Hello World!");
            //     }
            // });
            
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/game_request")
                {
                    if (!context.Request.HasFormContentType)
                    {
                        context.Response.Redirect("/");
                    }
                    else
                    {
                        var test = context.Request.Query;
                        Context.CommandModel.AddCommand(Context.Factory.CommandFactory[context.Request.Form["Command"]](context.Request.Form, context.Response, context.Request));
                    }
                }
                else
                {
                    await next();
                }
            });
        }
    }
}