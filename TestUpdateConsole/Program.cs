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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            using (var client = new WebClient())
            {
                try
                {
                    
                    var getNewVersion = client.OpenRead(new Uri("https://raw.githubusercontent.com/Sereoj/uploads/main/ds_new/version.txt", UriKind.Absolute));

                    using (StreamReader StreamReader = new StreamReader(getNewVersion))
                    {
                        Console.WriteLine(StreamReader.ReadToEnd().Trim());
                        StreamReader.Close();
                    }

                }
                catch (WebException wex)
                {
                    Console.WriteLine(wex.Message);
                }
                client.Dispose();
            }
            IsUpdate();

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


        public static void IsUpdate()
        {
            Console.WriteLine(СompareVersions(new Version("1.0"), new Version("1.0")));
        }
    }
}
