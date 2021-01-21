using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Test.Tests.Modules
{
        internal class GLUpdater
        {
            private static GLUpdater _model;

            public static GLUpdater Model => _model ??= new GLUpdater();

            public string New_version { get; set; }


            public bool IsUpdate()
            {
                try
                {
                    var v1 = new Version("1.0");
                    var v2 = new Version(New_version);
                    return v1.CompareTo(v2) <= -1;
                }
                catch(Exception ex )
                {
                Console.WriteLine(ex.Message);
                    return false;
                } 
            }
            public void Checker()
            {
                Task.Run(Update);
            }

            public void UpdateApplication()
            {
                if (IsUpdate())
                {
                    Task.Run(Download).GetAwaiter();
                }
            }
            
            public async Task Update()
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                await RequestAsync();
                await Task.Delay(1000);
                IsUpdate();
            }

            private async Task RequestAsync()
            {
            using (var client = new WebClient())
            {
                try
                {

                    var getNewVersion = await client.OpenReadTaskAsync(new Uri("https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/version.txt", UriKind.Absolute));

                    using (StreamReader StreamReader = new StreamReader(getNewVersion))
                    {
                        New_version = StreamReader.ReadToEnd().Trim();
                        StreamReader.Close();
                    }

                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                client.Dispose();
            }
        }

            private async Task Download()
            {
                await Task.Delay(1000);

                using WebClient client = new WebClient();
                await client.DownloadFileTaskAsync(new Uri("https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/Test.exe"), "update.exe");
                
            }

        }
    }
