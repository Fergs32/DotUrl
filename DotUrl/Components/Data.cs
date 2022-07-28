using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text;

namespace DotUrl.Components
{
    public class Data
    {
        public static string CurrentDir = Directory.GetCurrentDirectory();
        public static string d = CurrentDir + "\\Hits";
        public static string d2 = CurrentDir + "\\Proxychecker";

        public static void Save(string url)
        {
            string obj = (string)(object)url;
            try
            {
                if (!Directory.Exists(d))
                {
                    Directory.CreateDirectory(d);
                    Thread.Sleep(1000);
                    File.AppendAllText("Hits/Vulnerables.txt", obj + Environment.NewLine);

                }
                else
                {
                    if (Directory.Exists(d))
                    {
                        File.AppendAllText("Hits/Vulnerables.txt", obj + Environment.NewLine);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); Console.ReadLine(); }
        }
        public static void SaveFPD(string url, string message)
        {
            string obj = (string)(object)url + " | Route: " + message;
            try
            {
                if (!Directory.Exists(d))
                {
                    Directory.CreateDirectory(d);
                    Thread.Sleep(1000);
                    File.AppendAllText("Hits/FPD.txt", obj + Environment.NewLine);

                }
                else
                {
                    if (Directory.Exists(d))
                    {
                        File.AppendAllText("Hits/FPD.txt", obj + Environment.NewLine);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); Console.ReadLine(); }
        }

        public static void SaveProxies(string Proxies)
        {
            string obj = (string)(object)Proxies;
            try
            {
                if (!Directory.Exists(d2))
                {
                    Directory.CreateDirectory(d2);
                    Thread.Sleep(1000);
                    File.AppendAllText("Proxychecker/Proxies.txt", obj + Environment.NewLine);

                }
                else
                {
                    if (Directory.Exists(d2))
                    {
                        File.AppendAllText("Proxychecker/Proxies.txt", obj + Environment.NewLine);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); Console.ReadLine(); }
        }
    }
}
