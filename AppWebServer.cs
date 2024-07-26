using System.Reflection;
using System.Text;
using EmbedIO;
using EmbedIO.Actions;
using EmbedIO.WebApi;
using EmbedIO.WebSockets;

namespace MauiAppEmbedIO
{
    public class AppWebServer
    {
        public void CreateWebServer()
        {
            string htmlString = @"
                        <!DOCTYPE html>
                        <html lang='en'>
                        <head>
                            <meta charset='UTF-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <title>My Web App</title>
                            <style>
                                body {
                                    font-family: Arial, sans-serif;
                                    background-color: #f0f0f0;
                                    margin: 0;
                                    padding: 0;
                                    display: flex;
                                    justify-content: center;
                                    align-items: center;
                                    height: 100vh;
                                    color: #333;
                                }
                                h1 {
                                    color: #007BFF;
                                }
                            </style>
                        </head>
                        <body>
                            <h1>Welcome to My Web App</h1>
                            <p>This is a simple web application.</p>
                            <script>
                                document.addEventListener('DOMContentLoaded', () => {
                                    console.log('Document is ready');
                                });
                            </script>
                        </body>
                        </html>";
            Task.Factory.StartNew(async () =>
            {                
                var applicationController = new ApplicationController();

                WebServer output = new WebServer(server => server.WithUrlPrefix("http://127.0.0.1:1234").WithMode(HttpListenerMode.EmbedIO));
                
                output.WithWebApi("/api", m => m.WithController(() => applicationController));
                output.WithModule(new ProgressWebSocket());

                //output.WithEmbeddedResources("/", assembly, "EasePricePanelEditor.Data");
                output.WithEmbeddedResources("/", Assembly.GetExecutingAssembly(), "MauiAppEmbedIO.wwwroot");

                //output.WithModule(new ActionModule("/", HttpVerbs.Any, ctx =>
                // {
                //     ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                //     ctx.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                //     ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
                //     return ctx.SendStringAsync(htmlString, "text/html", Encoding.UTF8);
                // }));

                await output.RunAsync();
            });
        }

    }
}
