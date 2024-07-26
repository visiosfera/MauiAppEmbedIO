using EmbedIO.Routing;
using EmbedIO;
using EmbedIO.WebApi;
using System.Text.Json;
using System.Text;
using EmbedIO.WebSockets;

namespace MauiAppEmbedIO
{
    public class ApplicationController : WebApiController
    {
        private static int progress = 0;

        [Route(HttpVerbs.Get, "/progress")]
        public int GetProgress() => progress;

        // Método para atualizar o progresso
        public static void UpdateProgress(int value)
        {
            if (value >= 0 && value <= 100)
            {
                progress = value;
            }
        }

        [Route(HttpVerbs.Post, "/init")]
        public async Task<DataActionResult<string>> Init()
        {
           
            //return await Task.FromResult<string>("TESTE");

            var response = new
            {
                Successful = true,
                Message = "Operação realizada com sucesso"
            };

            string r = JsonSerializer.Serialize(response);

            //return r;

            return new DataActionResult<string> { Successful = true, Message = "Mídia criada com sucesso" };
            //return await Task.FromResult<string>("{\"Successful\":true,\"Message\":\"Opera\\u00E7\\u00E3o realizada com sucesso\"}");
        }

        [Route(HttpVerbs.Get, "/data")]
        public async Task SendData()
        {
            Response.ContentType = "application/json";

            List<string> chunks = new List<string>
            {
                "{ \"chunk\": \"data part 1\" }",
                "{ \"chunk\": \"data part 2\" }",
                "{ \"chunk\": \"data part 3\" }"
            };

            foreach (var chunk in chunks)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(chunk);
                await Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                await Response.OutputStream.FlushAsync();

                // Simula um atraso para representar um fluxo de dados real
                await Task.Delay(1000);
            }
        }

        [Route(HttpVerbs.Get, "/counter")]
        public Task GetCounter()
        {
            int counter = 1000;  // Exemplo de contador
            int chunkSize = 100;
            var chunks = new List<int>();
            for (int i = 0; i < counter; i += chunkSize)
            {
                chunks.Add(Math.Min(chunkSize, counter - i));
            }

            return HttpContext.SendDataAsync(chunks);
        }

    }
    public class ProgressWebSocket : WebSocketModule
    {
        public ProgressWebSocket()
            : base("/ws", true)
        {
        }

        protected override async Task OnMessageReceivedAsync(IWebSocketContext context, byte[] buffer, IWebSocketReceiveResult result)
        {
            // Simulando o progresso de 0 a 100
            for (int progress = 0; progress <= 100; progress++)
            {
                var message = $"{{\"progress\": {progress}}}";
                await SendAsync(context, message);
                await Task.Delay(100); // Simulando tempo de processamento
            }
        }
    }

}
