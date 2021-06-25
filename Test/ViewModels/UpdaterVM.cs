﻿using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using Test.ViewModels.Base;
using System.Net.Http;

namespace Test.ViewModels
{
    public class UpdaterVM : ViewModel
    {
        public string URL_Info
        {
            get; set;
        }
        public string URL_Version
        {
            get; set;
        }

        public string URL_Application
        {
            get; set;
        }
        private MessengerVM MessengerVM
        {
            get;set;
        }
        public string Version
        {
            get; private set;
        }
        private string _Info;
        public string Info
        {
            get => _Info; set => Set(ref _Info, value);
        }
        public bool IsUpdate()
        {
            var current = new Models.Version().Get(false);
            var result = current.CompareTo(Version);
            if ( result > 0 )
                return false;
            else if ( result < 0 )
                return true;
            else
                return false;
        }
        public void Download()
        {
            string name = Path.GetFileName(URL_Application);
            DownloadApplication(URL_Application, name);
        }

        public void GetInfo()
        {
            RequestInfoAsync(URL_Info);
        }

        public void GetVersion()
        {
            RequestVersionAsync(URL_Version);
        }

        private async Task<bool> CheckForInternetConnectionAsync()
        {
            var client = new HttpClient();
            using ( var tokSource = new CancellationTokenSource(5000) )
            {
                try
                {
                    await client.GetAsync("https://example.com/", tokSource.Token);
                }
                catch ( Exception )
                {
                    return false;
                }
            }
            return true;
        }

        private void RequestVersionAsync(string uri)
        {
            new Thread(async () =>
            {
                bool internet = await CheckForInternetConnectionAsync();
                if ( internet )
                {
                    var client = new HttpClient();
                    Version = client.GetStringAsync(new Uri(uri, UriKind.Absolute)).Result.Trim();
                }
            }).Start();
        }

        private void RequestInfoAsync(string uri)
        {
            new Thread(async () =>
            {
                bool internet = await CheckForInternetConnectionAsync();
                if ( internet )
                {
                    var client = new HttpClient();
                    var getInfo = client.GetStringAsync(new Uri(uri, UriKind.Absolute)).Result.Trim();
                    Info = getInfo;
                }
            }).Start();
        }

        private void DownloadApplication(string url, string name)
        {
            new Thread(async () =>
            {
                bool internet = await CheckForInternetConnectionAsync();
                if ( internet )
                {
                    var client = new WebClient();
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);

                    client.DownloadFileAsync(new Uri(url), name);
                }
            }).Start();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var progress = "Загружено " + (  e.BytesReceived / 1024f ).ToString("#0.##") + "КБ" + " / " + (  e.TotalBytesToReceive / 1024f ).ToString("#0.##") + "КБ" + " | " + e.ProgressPercentage + "%";

            MessengerVM.SetMessage(progress);
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessengerVM.SetMessage(Version);
            if ( e.Error != null )
            {
                MessengerVM.SetMessage(e.Error.Message);
            }
            if ( e.Cancelled )
            {
                MessengerVM.Messager = e.Cancelled.ToString();
            }
        }
        public UpdaterVM()
        {
        }
        public UpdaterVM(MessengerVM messengerVM)
        {
            URL_Application = "https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/Test.exe";
            URL_Info = "https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/info.txt";
            URL_Version = "https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/version.txt";
            MessengerVM = messengerVM;
        }
    }
}
