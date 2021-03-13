﻿using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Test.Models.Message;
using Version = Test.Models.Version;

namespace Test.Models.GLUpdater
{
    /*
     * Модуль обновления приложения
     * 
     */
    internal class GLUpdater : CLMessage
    {
        private static GLUpdater _model;

        public static GLUpdater Model => _model ??= new GLUpdater();

        private string new_version;
        public string NewVersion
        {
            get => new_version;
            private set => new_version = value;
        }

        private string getNewInfo;
        public string GetNewInfo
        {
            get => getNewInfo;
            private set => getNewInfo = value;
        }

        private bool update;
        public bool Update
        {
            get => update;
            private set => update = value;
        }

        private readonly string URLVersion = "https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/version.txt";
        private readonly string URLInformation = "https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/info.txt";


        ~GLUpdater()
        {
            GC.Collect(4, GCCollectionMode.Optimized, true);
        }

        private bool СompareVersions(System.Version current, System.Version actual)
        {
            var result = current.CompareTo(actual);
            if (result > 0)
                return false;
            else if (result < 0)
                return true;
            else
                return false;
        }


        public bool IsUpdate()
        {
            var version = RequestAsync(URLVersion).Result;
            ValidateVersion(version);
            if(СompareVersions(new System.Version(Version.Model.GetVersion(false)), new System.Version(version)))
            {
                NewVersion = version;
                Update = true;
                return true;
            }
            Update = false;
            return false;
        }

        public void GetNewApplication()
        {
            Task.Run(Download);
        }


        public string GetResult()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                string s = client.DownloadString(URLInformation);
                client.Dispose();
                return s;
            }
        }

        private void ValidateVersion(string version)
        {
            /*
             Todo: Проверка, что это именно версия, а не текст.
             */
            if (string.IsNullOrWhiteSpace(version))
            {
                NewVersion = "1.0";
            }
        }


        private async Task<string> RequestAsync( string uri )
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            using (var client = new WebClient())
            {
                try
                {
                    var getNewVersion = await client.OpenReadTaskAsync(new Uri(uri, UriKind.Absolute));

                    using StreamReader StreamReader = new StreamReader(getNewVersion);
                    return StreamReader.ReadToEnd().Trim();
                }
                catch (WebException ex)
                {
                    SetMessage(ex.Message);
                }
                client.Dispose();
            }
            return "0.0";
        }

        private async Task Download()
        {
            using WebClient client = new WebClient();
            IWebProxy webProxy = WebRequest.DefaultWebProxy;
            webProxy.Credentials = CredentialCache.DefaultCredentials;
            client.Proxy = webProxy;
            await client.DownloadFileTaskAsync(new Uri("https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/Test.exe"), "update.exe").ConfigureAwait(false);
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
            {
                SetMessage("Обновление завершено!");
            }
        }
}