using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.ViewModels.Base;

namespace Test.ViewModels
{
    public class UpdaterVM : ViewModel
    {
        private int _ProgressPercentage;
        public int ProgressPercentage
        {
            get => _ProgressPercentage;
            set => Set(ref _ProgressPercentage, value);
        }
        private MessengerVM MessengerVM
        {
            get;set;
        }
        public void DownloadApplication(string url, string name)
        {
            new Thread(() =>
            {
                var client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);

                client.DownloadFileAsync(new Uri(url), name);
            }).Start();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var progress = "Загружено " + (  e.BytesReceived / 1024f ).ToString("#0.##") + "КБ" + " / " + (  e.TotalBytesToReceive / 1024f ).ToString("#0.##") + "КБ" + " | " + e.ProgressPercentage + "%";

            MessengerVM.SetMessage(progress);
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
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
            MessengerVM = messengerVM;
        }
    }
}
