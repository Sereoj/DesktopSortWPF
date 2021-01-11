using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            if (Version.Model.GetVersion(false) == New_version)
            {
                return false;
            }
            return true;
        }
        public void Checker()
        {
            Task.Run(RequestAsync);
        }

        private async Task RequestAsync()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://raw.githubusercontent.com/Sereoj/DesktopSort/Design/Updates/xml/update.xml");
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    New_version = reader.ReadToEnd();
                }
            }
            response.Close();
        }
    }
}