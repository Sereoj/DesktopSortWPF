using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DesktopSort.UI.MarkupExtension;
using DesktopSort.UI.Models.FileManagerModel;
using DesktopSort.UI.ViewModels.Base;
using Version = DesktopSort.UI.Models.Version;

namespace DesktopSort.UI.ViewModels
{
    public class UpdaterVM : ViewModel
    {
        private string _Info;

        public UpdaterVM()
        {
        }

        public UpdaterVM(MessengerVM messengerVM)
        {
            URL_Application = "https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/updater.zip";
            URL_Info = "https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/info.txt";
            URL_Version = "https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/version.txt";
            MessengerVM = messengerVM;
        }

        public string URL_Info
        {
            get;
            set;
        }

        public string URL_Version
        {
            get;
            set;
        }

        public string URL_Application
        {
            get;
            set;
        }

        private MessengerVM MessengerVM
        {
            get;
        }

        public string Version
        {
            get;
            private set;
        }

        public string Info
        {
            get => _Info;
            set => Set(ref _Info, value);
        }

        public string FullPathArchive
        {
            get;
            set;
        }

        public bool IsUpdate()
        {
            var current = new Version().Get(false);
            var result = current.CompareTo(Version);
            return result <= 0 && result < 0;
        }

        public void Download()
        {
            FullPathArchive = Path.Combine(Environment.CurrentDirectory, "update.zip");
            DownloadApplication(@"https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/DesktopSort.Updater.exe", Path.Combine(Environment.CurrentDirectory, "DesktopSort.Updater.exe"));
            DownloadApplication(URL_Application, FullPathArchive);
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
            using (var tokSource = new CancellationTokenSource(5000))
                try
                {
                    await client.GetAsync("https://example.com/", tokSource.Token);
                }
                catch (Exception)
                {
                    return false;
                }

            return true;
        }

        private void RequestVersionAsync(string uri)
        {
            new Thread(async () =>
            {
                var internet = await CheckForInternetConnectionAsync();
                if (internet)
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
                var internet = await CheckForInternetConnectionAsync();
                if (internet)
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
                var internet = await CheckForInternetConnectionAsync();
                if (internet)
                {
                    var client = new WebClient();
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;

                    client.DownloadFileAsync(new Uri(url), name);
                }
            }).Start();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var progress = (e.BytesReceived / 1024f).ToString("#0.##") + "Kbps" + " / " +
                           (e.TotalBytesToReceive / 1024f).ToString("#0.##") + "Kbps" + " | " + e.ProgressPercentage +
                           "%";

            MessengerVM.SetMessage("UpdaterDownloadProcessText", progress);
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessengerVM.SetMessage("UpdaterDownloadFileCompleted", Version);
            if (e.Error != null) MessengerVM.SetMessage(e.Error.Message);
            if (e.Cancelled) MessengerVM.SetMessage(e.Cancelled.ToString());

            Thread.Sleep(2000);
            Process.Start("DesktopSort.Updater.exe");
            OnPropertyChanged("OnCloseProgram");
        }
    }
}