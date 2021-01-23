using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestUpdateConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(RequestAsync("https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/version.txt").Result);
            Console.WriteLine(RequestAsync("https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/info.txt").Result);
            Console.ReadLine();
        }
        private static bool СompareVersions(Version current, Version actual)
        {
            var result = current.CompareTo(actual);
            if (result > 0)
                return false;
            else if (result < 0)
                return true;
            else
                return false;
        }

        private static async Task<string> RequestAsync(string uri)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            using (var client = new WebClient())
            {
                try
                {
                    var getNewVersion = await client.OpenReadTaskAsync(new Uri(uri, UriKind.Absolute));

                    using (StreamReader StreamReader = new StreamReader(getNewVersion))
                    {
                        return StreamReader.ReadToEnd().Trim();
                    }
                }
                catch (WebException ex)
                {
                    //SetMessage(ex.Message);
                }
                client.Dispose();
            }
            return "0.0";
        }

        public static void IsUpdate()
        {
            Console.WriteLine(СompareVersions(new Version("1.0"), new Version("1.0")));
        }
    }
}
