using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test.Tests.Modules
{
        internal class GLUpdater
        {
            private static GLUpdater _model;

            public static GLUpdater Model => _model ??= new GLUpdater();

            private string new_version;
            public string NewVersion
            {
                get => new_version;
                private set => new_version = value;
            }

            private void ValidateVersion(string version)
            {
            /*
             Todo: Проверка, что это именно версия, а не текст.
             
             */
                if (!string.IsNullOrWhiteSpace(version))
                {
                    NewVersion = "1.0";
                }
            }
            
            private bool СompareVersions(Version current, Version actual)
            {
                var result = current.CompareTo(actual);
                if (result > 0)
                    return false;
                else if (result < 0)
                    return true;
                else
                    return true;
            }


            public bool IsUpdate()
            {
                var version = RequestAsync().Result;
                return СompareVersions(new Version("1.0"), new Version(version));
            }
            
            public void GetNewApplication()
            {
                Task.Run(Download);
            }

            public string GetResult()
            {
            Task<string> task = RequestAsync();
            return task.Result;
            }

            private async Task<string> RequestAsync()
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                using (var client = new WebClient())
                {
                    try
                    {

                        var getNewVersion = await client.OpenReadTaskAsync(new Uri("https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/version.txt", UriKind.Absolute));

                        using (StreamReader StreamReader = new StreamReader(getNewVersion))
                        {
                            NewVersion = StreamReader.ReadToEnd().Trim();
                            ValidateVersion(NewVersion);
                            StreamReader.Close();
                            return NewVersion;
                        }
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    client.Dispose();
                }
            return "0.0";
            }

            private async Task Download()
            {
                using (WebClient client = new WebClient())
                {
                    IWebProxy webProxy = WebRequest.DefaultWebProxy;
                    webProxy.Credentials = CredentialCache.DefaultCredentials;
                    client.Proxy = webProxy;
                    await client.DownloadFileTaskAsync(new Uri("https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/Test.exe"), "update.exe").ConfigureAwait(false); ;
                }
            }
        }
    }
