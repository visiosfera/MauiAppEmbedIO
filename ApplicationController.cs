using EmbedIO.Routing;
using EmbedIO;
using EmbedIO.WebApi;

namespace MauiAppEmbedIO
{
    public class ApplicationController : WebApiController
    {
        private static int progress = 0;
        private static string status = "Iniciando...";

        [Route(HttpVerbs.Get, "/progress")]
        public object GetProgress()
        {
            return new { Progress = progress, Status = status };
        }

        // Método para atualizar o progresso e a string
        public static void UpdateProgress(int value, string info)
        {
            if (value >= 0 && value <= 100)
            {
                progress = value;
                status = info;
            }
        }
    }

}
