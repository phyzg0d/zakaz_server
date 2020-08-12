using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewGame4.Core;

namespace NewGame4
{
    public class Startup
    {
        public ServerContext Context;
        public StartController StartController;
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app)
        {
            Context = new ServerContext();
            StartController = new StartController(Context);
            
            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseWebSockets();

            app.UseEndpoints(endpoints => { endpoints.MapHub<ChatHub>("/chat"); });

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
                        Context.CommandModel.AddCommand(Context.Factory.CommandFactory[context.Request.Form["Command"]](context.Request.Form, context.Response));
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