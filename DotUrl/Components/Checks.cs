using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Text;

namespace DotUrl.Components
{
    public class Checks
    {
        public static void ProxyCheck()
        {
            for (; RequestStorage.proxyType != "HTTP" && RequestStorage.proxyType != "SOCKS4" && RequestStorage.proxyType != "SOCKS5" && RequestStorage.proxyType != "NONE"; RequestStorage.proxyType = Colorful.Console.ReadLine())
                Colorful.Console.Write("Proxy type (HTTP/SOCKS4/SOCKS5/NONE): ");
            if (RequestStorage.proxyType != "NONE")
            try
            {
                RequestStorage.proxies = File.ReadLines("proxies.txt").ToList<string>();
                    if (RequestStorage.proxies.Count < 1)
                    {
                        Colorful.Console.WriteLine("[ProxyCheck] >> No proxies in file.", Color.BlueViolet, RequestStorage.proxies.Count);
                    }
                    else
                    {
                        Colorful.Console.WriteLine("[ProxyCheck] >> Grabbed {0} proxies from file", Color.BlueViolet, RequestStorage.proxies.Count);
                        Console.Clear();
                        AsciiMenu.Menu();
                        DotUrlMain.RequestWithProxySupport((IEnumerable<string>)RequestStorage.urls);
                    }
            }
            catch (Exception)
            {
                Colorful.Console.WriteLine("Unable to grab proxies from text file, maybe missing \"proxies.txt\" file?");
            }
        }
        public static void CheckUrls()
        {
            try
            {
                RequestStorage.urls = File.ReadLines("urls.txt").ToList<string>();
                Storage.Overall = RequestStorage.urls.Count;
                Colorful.Console.WriteLine("\t\t\t\t [ Loaded {0} Urls ]\n", Color.BlueViolet, RequestStorage.urls.Count);
            }
            catch (Exception)
            {
                Colorful.Console.WriteLine("Unable to grab proxies from text file, maybe missing \"urls.txt\" file?");
            }
        }
    } 
}
