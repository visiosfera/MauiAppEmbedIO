using System.Diagnostics;

namespace MauiAppEmbedIO
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            webView.Source = $"http://127.0.0.1:1234/index.htm";

            webView.Loaded += WebView_Loaded;
        }

        private async void WebView_Loaded(object? sender, EventArgs e)
        {
            CopiarPasta(@"E:\GoodBom\InstaladorGbExpedicao\publico", @"C:\regional\sistemaDeExpedicao", true);
        }

        private void CopiarPasta(string origem, string destino, bool recursivo)
        {
            var dir = new DirectoryInfo(origem);
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Não foi possível encontrar o diretório {dir.FullName}");

            DirectoryInfo[] dirs = dir.GetDirectories();
            Directory.CreateDirectory(destino);

            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destino, file.Name);
                //Status = $"Baixando o arquivo {file.Name}";
                CopyFileWithProgress(file.FullName, targetFilePath);
            }

            if (recursivo)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destino, subDir.Name);
                    CopiarPasta(subDir.FullName, newDestinationDir, true);
                }
            }
        }


        private void CopyFileWithProgress(string sourceFile, string destinationFile)
        {
            const int bufferSize = 1024 * 1024; // 1MB buffer size
            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
            {
                using (FileStream destinationStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write))
                {
                    long totalBytesRead = 0;
                    long total = ((int)(sourceStream.Length / bufferSize));

                    while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        destinationStream.Write(buffer, 0, bytesRead);
                        totalBytesRead += bytesRead;

                        var progress = (int)(totalBytesRead / total) / (bufferSize / 100);
                        var fileName = Path.GetFileName(sourceFile);

                        ApplicationController.UpdateProgress(progress, fileName);
                    }
                }
            }
        }

        private void ExecutarAtualizador(string caminhoExe)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = caminhoExe;

            //startInfo.Arguments = "argumentos aqui";

            try
            {
                using (Process process = Process.Start(processStartInfo))
                {
                    process!.WaitForExit();

                    int codigoSaida = process.ExitCode;
                    Console.WriteLine("O processo terminou com o código de saída: " + codigoSaida);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }

    }

}
