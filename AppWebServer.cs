using System.Reflection;
using EmbedIO;
using EmbedIO.WebApi;

namespace MauiAppEmbedIO
{
    public class AppWebServer
    {
        public void CreateWebServer()
        {
            Task.Factory.StartNew(async () =>
            {                
                var applicationController = new ApplicationController();

                WebServer output = new WebServer(server => server.WithUrlPrefix("http://127.0.0.1:1234").WithMode(HttpListenerMode.EmbedIO));                
                output.WithWebApi("/api", m => m.WithController(() => applicationController));
                output.WithEmbeddedResources("/", Assembly.GetExecutingAssembly(), "MauiAppEmbedIO.wwwroot");

                await output.RunAsync();
            });
        }

    }
}
