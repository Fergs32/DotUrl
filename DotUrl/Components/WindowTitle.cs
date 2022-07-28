using System;
using System.Threading;

namespace DotUrl.Components
{
    public class WindowTitle
    {
        public static void SetWindowTitle()
        {
            while(true)
            {
                Colorful.Console.Title = string.Format("Developed by https://github.com/Fergs32  |  Progress: {0}/{1}  |  Vulnerables: {2}  |  FPD: {3}  |  Bads: {4}  |  Errors: {5}", (object)Storage.Progress, (object)Storage.Overall, (object)Storage.Hits, (object)FullPathDisclosure.FPD_Hits, (object)Storage.Bad, (object)Storage.Errors);
                Thread.Sleep(100);
            }
        }
    }
}
