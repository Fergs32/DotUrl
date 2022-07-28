using System;
using System.Collections.Generic;
using System.Drawing;
using Leaf.xNet;
using DotUrl.Components;
using System.IO;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace DotUrl.Components
{
    internal struct ProxySettings
    {
        public static string ProxyType;
        public static List<string> Proxies;
        public static string Proxy;

        public static int GoodProxies;
        public static int BadProxies;
        public static int BannedProxies;
        public static int i;
    }
    public class Proxyscraper
    {
        public static void GetProxies()
        {
            AsciiMenu.ScraperMenu();
            Colorful.Console.Write("\n\nProxy type to scrape (HTTP, SOCKS4, SOCKS5): "); ProxySettings.ProxyType = Console.ReadLine();
            try
            {
                WebClient wc = new WebClient();
                string data = wc.DownloadString($"https://api.proxyscrape.com/v2/?request=displayproxies&protocol=" + ProxySettings.ProxyType + "&timeout=10000&country=all&ssl=all&anonymity=all");
                File.WriteAllText(Environment.CurrentDirectory + "/scrapedproxies.txt", data);
            }
            catch { }

            Colorful.Console.Write("Would you like to test these proxies? Y/N: "); string ureply = Console.ReadLine().ToLower();
            ProxySettings.Proxies = File.ReadLines("scrapedproxies.txt").ToList<string>();
            switch (ureply)
            {
                case "y":
                    TestProxies();
                    break;
                case "n":
                    break;
            }
        }


        public static void TestProxies()
        {
            new Thread(new ThreadStart(Proxyscraper.Proxy_SetWindowTitle)).Start();
            while (ProxySettings.i < ProxySettings.Proxies.Count)
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        if (ProxySettings.i >= ProxySettings.Proxies.Count)
                        {
                            Environment.Exit(0);
                        } 
                        using (HttpRequest httpRequest = new HttpRequest())
                        {
                            ProxySettings.Proxy = ProxySettings.Proxies[ProxySettings.i];
                            ProxyClient proxyClient = ProxySettings.ProxyType == "SOCKS4" ? (ProxyClient)Socks4ProxyClient.Parse(ProxySettings.Proxy) : (ProxySettings.ProxyType == "SOCKS5" ? (ProxyClient)Socks5ProxyClient.Parse(ProxySettings.Proxy) : (ProxyClient)HttpProxyClient.Parse(ProxySettings.Proxy));
                            httpRequest.Proxy = proxyClient;
                            httpRequest.KeepAliveTimeout = 6000; // Change this for more proxies ig
                            HttpResponse resp = httpRequest.Head("https://www.google.com/");
                            if (resp.StatusCode == Leaf.xNet.HttpStatusCode.OK)
                            {
                                Colorful.Console.WriteLine("[+] {0}", Color.LightSeaGreen, ProxySettings.Proxy);
                                ProxySettings.i++; ProxySettings.GoodProxies++;
                                Data.SaveProxies(ProxySettings.Proxy);
                            }
                            else
                            {
                                Colorful.Console.WriteLine("[-] {0}", Color.IndianRed, ProxySettings.Proxy);
                                ProxySettings.i++; ProxySettings.BadProxies++;
                            }
                        }
                    }
                    catch
                    {
                        ProxySettings.i++; ProxySettings.BannedProxies++;
                    }
                });
            }
        }
        public static void Proxy_SetWindowTitle()
        {
            while (true)
            {
                Colorful.Console.Title = string.Format("Developed by https://github.com/Fergs32  |  Progress: {0}/{1}  |  Good Proxies: {2}  |  Bad Proxies: {3}  |  Banned Proxies: {4}", (object)ProxySettings.i, (object)ProxySettings.Proxies.Count, (object)ProxySettings.GoodProxies, (object)ProxySettings.BadProxies, (object)ProxySettings.BannedProxies);
                Thread.Sleep(100);
            }
        }
    }
}
