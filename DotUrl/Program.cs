using System;
using Leaf.xNet;
using System.IO;
using System.Threading;
using System.Linq;
using System.Drawing;
using DotUrl.Components;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DotUrl
{

    // Used structs to store information accordingly and efficiently. Found it easier and more compact doing so.
    internal struct Storage
    {
        // Global struct variables

        public static int Hits;
        public static int Bad;
        public static int Errors;
        public static int Overall;
        public static int Progress;

        public static int Option_picked;
    }

    internal struct RequestStorage
    {
        // Global struct variables

        public static List<string> urls;
        public static List<string> proxies;
        public static string proxyType;
    }

    internal struct FullPathDisclosure
    {
        public static string fpd;
        public static int FPD_Hits;
        public static int FPD_Fails;
    }

    internal struct Threading
    {
        /*
         * This was implemented, I had to remove as my multi threading technique (Parallel.ForEach()) was not working accordingly. So I had to replace with a slower option .foreach, apologies.
         * 
         * If anyone wants to help, please open a merge request and I will take a look.
         * 
         */
        public static int threads;
    }

    abstract class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;
            AsciiMenu.Menu();
            Checks.CheckUrls();
            Colorful.Console.WriteLine("[1] Scrape Proxies");
            Colorful.Console.WriteLine("[2] Scan URLs");
            Colorful.Console.Write("\n[Option] >> "); int.TryParse(Console.ReadLine(), out Storage.Option_picked);
            switch(Storage.Option_picked)
            {
                case 1:
                    Console.Clear();
                    Proxyscraper.GetProxies();
                    break;
                case 2:
                    UrlScan();
                    break;
            }           
        }
        public static void UrlScan()
        {
            Colorful.Console.Write("\nWould you like to enable proxies? Y/N: ", Color.BlueViolet); string enable_proxies = Console.ReadLine().ToLower();
            Colorful.Console.Write("\nWould you like to check for FPD (Full Path Disclosure) Y/N: "); FullPathDisclosure.fpd = Console.ReadLine().ToLower();
            // Used Switch() for better time complexity compared to If
            switch (enable_proxies)
            {
                case "y":
                    Checks.ProxyCheck();
                    break;
                case "n":
                    RequestStorage.proxyType = "NONE";
                    DotUrlMain.StartRequests((IEnumerable<string>)RequestStorage.urls); // Starting request process by calling StartRequest Method.
                    break;
                default:
                    Colorful.Console.WriteLine("[Error] Invalid input", Color.Red);
                    Thread.Sleep(-1);
                    break;

            }
        }
    }

    public class DotUrlMain
    {
        public static void StartRequests(IEnumerable<string> urls)
        {
            Console.Clear();
            AsciiMenu.Menu();
            string response_string = "";
            new Thread(new ThreadStart(WindowTitle.SetWindowTitle)).Start();
            foreach (string url in urls)
            {
                try
                {
                    using (HttpRequest httpRequest = new HttpRequest())
                    {
                        // Setting up request settings, such as user agents, headers etc.
                        httpRequest.UserAgent = Http.ChromeUserAgent();
                        httpRequest.IgnoreProtocolErrors = true;
                        httpRequest.AddHeader("Content-Type", "text/plain;charset=UTF-8");
                        // having google.com as your origin, the website will normally automically accept it as you're coming from the google pages.
                        httpRequest.AddHeader("Origin", "https://google.com");
                        HttpResponse httpResponse1 = httpRequest.Get(new Uri(url)); // Sendiing request to the url provided by you, which consists of our headers we set above, our user agent and origin.
                        if (httpResponse1.StatusCode == HttpStatusCode.OK)
                        {
                            response_string = httpResponse1.ToString();
                            if (FullPathDisclosure.fpd == "y")
                            {
                                FPD(response_string, url);
                            } else { }
                        }
                        string str = ((IEnumerable<string>)new string[8] // Basic IEnum string storage which then can be compared to with our response_string
                        {
                              "You have an error in your SQL syntax",
                              ".php</b> on line",
                              ".php on line",
                              "Warning:",
                              "<b>Warning</b>:",
                              "mysql_fetch_array()",
                              "syntax",
                              "MySQL"
                        }).FirstOrDefault<string>((Func<string, bool>)(s => response_string.Contains(s)));

                        if (!(str == "You have an error in your SQL syntax"))
                        {
                            if (!(str == ".php</b> on line"))
                            {
                                if (str == ".php on line")
                                {
                                    Colorful.Console.WriteLine("[Vulnerable] {0}", Color.LightSeaGreen, url);
                                    Storage.Hits++; Storage.Progress++;
                                    Data.Save(url);
                                }
                                else
                                {
                                    Colorful.Console.WriteLine("[Bad] {0}", Color.IndianRed, url);
                                    Storage.Bad++; Storage.Progress++;
                                    Data.Save(url);
                                }
                            }
                            else
                            {
                                Colorful.Console.WriteLine("[Vulnerable] {0}", Color.LightSeaGreen, url);
                                Storage.Hits++; Storage.Progress++;
                                Data.Save(url);
                            }
                        }
                        else
                        {
                            Colorful.Console.WriteLine("[Vulnerable] {0}", Color.LightSeaGreen, url);
                            Storage.Hits++; Storage.Progress++;
                            Data.Save(url);
                        }
                    }
                }
                catch (Exception)
                {
                    Storage.Errors++; Storage.Progress++;
                }
            }
        }

        public static void RequestWithProxySupport(IEnumerable<string> urls)
        {
            Console.Clear();
            AsciiMenu.Menu();
            string response_string = "";
            new Thread(new ThreadStart(WindowTitle.SetWindowTitle)).Start();
            foreach (string url in urls)
            {
            Retry:
                try
                {
                    using (HttpRequest httpRequest = new HttpRequest())
                    {
                        string proxy = "";
                        if (RequestStorage.proxyType != "NONE")
                        {
                            proxy = RequestStorage.proxies[new Random().Next(RequestStorage.proxies.Count)];
                            ProxyClient proxyClient = RequestStorage.proxyType == "SOCKS4" ? (ProxyClient)Socks4ProxyClient.Parse(proxy) : (RequestStorage.proxyType == "SOCKS5" ? (ProxyClient)Socks5ProxyClient.Parse(proxy) : (ProxyClient)HttpProxyClient.Parse(proxy));
                            httpRequest.Proxy = proxyClient;
                        }
                        httpRequest.UserAgent = Http.ChromeUserAgent();
                        httpRequest.AddHeader("Content-Type", "text/plain;charset=UTF-8");
                        httpRequest.AddHeader("Origin", "https://google.com");
                        HttpResponse httpResponse1 = httpRequest.Get(new Uri(url));
                        if (httpResponse1.StatusCode == HttpStatusCode.OK)
                        {
                            response_string = httpResponse1.ToString();
                            if (FullPathDisclosure.fpd == "y")
                            {
                                FPD(response_string, url);
                            }
                            else { }
                        }
                        string str = ((IEnumerable<string>)new string[3]
                        {
                              "You have an error in your SQL syntax",
                              ".php</b> on line",
                              ".php on line"
                        }).FirstOrDefault<string>((Func<string, bool>)(s => response_string.Contains(s)));
                        if (!(str == "You have an error in your SQL syntax"))
                        {
                            if (!(str == ".php</b> on line"))
                            {
                                if (str == ".php on line")
                                {
                                    Colorful.Console.WriteLine("[Vulnerable] {0} | Proxy Used: {1}", Color.LightSeaGreen, url, proxy);
                                    Storage.Hits++; Storage.Progress++;
                                    Data.Save(url);
                                }
                                else
                                {
                                    Colorful.Console.WriteLine("[Bad] {0}} | Proxy Used: {1}", Color.IndianRed, url, proxy);
                                    Storage.Bad++; Storage.Progress++;
                                    Data.Save(url);
                                }
                            }
                            else
                            {
                                Colorful.Console.WriteLine("[Vulnerable] {0} | Proxy Used: {1}", Color.LightSeaGreen, url, proxy);
                                Storage.Hits++; Storage.Progress++;
                                Data.Save(url);
                            }
                        }
                        else
                        {
                            Colorful.Console.WriteLine("[Vulnerable] {0} | Proxy Used: {1}", Color.LightSeaGreen, url, proxy);
                            Storage.Hits++; Storage.Progress++;
                            Data.Save(url);
                        }
                    }
                }
                catch (Exception)
                {
                    Storage.Errors++; Storage.Progress++;
                    goto Retry;
                }
            }
        }

        public static void FPD(string httpresponse, string url)
        {
            try
            {
                string str = ((IEnumerable<string>)new string[3]
                {
                    "given in",
                    "boolean in",
                    "thrown in"
                }).FirstOrDefault<string>((Func<string, bool>)(s => httpresponse.Contains(s)));

                if (!(str == "given in"))
                {
                    if (!(str == "boolean in"))
                    {
                        if (!(str == "thrown in"))
                            return;
                        string message = Regex.Match(httpresponse, "thrown in (.*) on line").Groups[1].Value.Replace("<b>", "").Replace("</b>", "");
                        if (string.IsNullOrEmpty(message)) return;
                        Data.SaveFPD(url, message);
                        FullPathDisclosure.FPD_Hits++;
                        Colorful.Console.WriteLine("[FPD Detected] {0} | Route: {1}", Color.LightYellow, url, message);
                    }
                    else
                    {
                        string message = Regex.Match(httpresponse, "boolean in (.*) Stack trace").Groups[1].Value.Replace("<b>", "").Replace("</b>", "");
                        if (string.IsNullOrEmpty(message)) return;
                        Data.SaveFPD(url, message);
                        FullPathDisclosure.FPD_Hits++;
                        Colorful.Console.WriteLine("[FPD Detected] {0} | Route: {1}", Color.LightYellow, url, message);
                    }
                }
                else
                {
                    string message = Regex.Match(httpresponse, "given in (.*) on line").Groups[1].Value.Replace("<b>", "").Replace("</b>", "");
                    if (string.IsNullOrEmpty(message)) return;
                    Data.SaveFPD(url, message);
                    FullPathDisclosure.FPD_Hits++;
                    Colorful.Console.WriteLine("[FPD Detected] {0} | Route: {1}", Color.LightYellow, url, message);
                }
            }
            catch (Exception ex)
            {
                Colorful.Console.WriteLine(ex, Color.Red);
            }
        }
    }
}
            

