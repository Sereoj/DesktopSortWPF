using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Threading.Tasks;
using Test.Services.Message;
using Version = Test.Models.Version;

namespace Test.Services.GLUpdater
{
    /*
     * Модуль обновления приложения
     * 
     */
    internal class GLUpdater : CLMessage
    {
        private static GLUpdater _model;

        public static GLUpdater Model => _model ??= new GLUpdater();

        public string New_version { get; set; }


        public bool IsUpdate()
        {
            var v1 = new System.Version(Version.Model.GetVersion(false));
            var v2 = new System.Version(New_version);

            return v1.CompareTo(v2) <= -1;
        }
        public void Checker()
        {
            Task.Run(RequestAsync).Wait(1000);
        }

        public void UpdateApplication()
        {
            if (IsUpdate())
            {
                Task.Run(Download).GetAwaiter();
            }
        }


        private async Task RequestAsync()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/version.txt");
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US) AppleWebKit/534.20 (KHTML, like Gecko) Chrome/11.0.672.2 Safari/534.20";
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using StreamReader reader = new StreamReader(stream);
                New_version = reader.ReadToEnd();
            }
            response.Close();
        }

        private async Task Download()
        {
            await Task.Delay(1000);

            using WebClient client = new WebClient();
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/Test.exe"), "update.exe");
            //client.Dispose();
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            SetMessage("Обновление завершено!");
        }
    }
}