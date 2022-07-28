using System;
using Colorful;
using System.Drawing;
using System.Text.RegularExpressions;

namespace DotUrl.Components
{
    public class AsciiMenu
    {
        public static void Menu()
        {
            string[] AsciiMenu = new string[]
            {
                "",
                "",
                "",
                "\t\t                 █    ██  ██▀███   ██▓    ",
                "\t\t                 ██  ▓██▒▓██ ▒ ██▒▓██▒    ",
                "\t\t                ▓██  ▒██░▓██ ░▄█ ▒▒██░    ",
                "\t\t                ▓▓█  ░██░▒██▀▀█▄  ▒██░    ",
                "\t\t            ██▓ ▒▒█████▓ ░██▓ ▒██▒░██████▒",
                "\t\t            ▒▓▒ ░▒▓▒ ▒ ▒ ░ ▒▓ ░▒▓░░ ▒░▓  ░",
                "\t\t          ░▒  ░░▒░ ░ ░   ░▒ ░ ▒░░ ░ ▒  ░",
                "\t\t          ░    ░░░ ░ ░   ░░   ░   ░ ░  ",
                "\t\t           ░     ░        ░         ░  ░",
                "\t\t           ░                            ",
                "",
                "\t\t           Coded By | github.com/Fergs32 |",
                "",
            };
            foreach(string line in AsciiMenu)
            {
                Colorful.Console.WriteLine(line, Color.White);
            }
        }

        public static void ScraperMenu()
        {
            string[] ScraperMenu = new string[]
            {
                "",
                "",
                "",
              "                █    ██  ██▀███   ██▓         ██████  ▄████▄   ██▀███   ▄▄▄       ██▓███  ▓█████  ██▀███  ",
              "                ██  ▓██▒▓██ ▒ ██▒▓██▒       ▒██    ▒ ▒██▀ ▀█  ▓██ ▒ ██▒▒████▄    ▓██░  ██▒▓█   ▀ ▓██ ▒ ██▒",
              "               ▓██  ▒██░▓██ ░▄█ ▒▒██░       ░ ▓██▄   ▒▓█    ▄ ▓██ ░▄█ ▒▒██  ▀█▄  ▓██░ ██▓▒▒███   ▓██ ░▄█ ▒",
              "               ▓▓█  ░██░▒██▀▀█▄  ▒██░         ▒   ██▒▒▓▓▄ ▄██▒▒██▀▀█▄  ░██▄▄▄▄██ ▒██▄█▓▒ ▒▒▓█  ▄ ▒██▀▀█▄  ",
              "           ██▓ ▒▒█████▓ ░██▓ ▒██▒░██████▒   ▒██████▒▒▒ ▓███▀ ░░██▓ ▒██▒ ▓█   ▓██▒▒██▒ ░  ░░▒████▒░██▓ ▒██▒",
              "           ▒▓▒ ░▒▓▒ ▒ ▒ ░ ▒▓ ░▒▓░░ ▒░▓  ░   ▒ ▒▓▒ ▒ ░░ ░▒ ▒  ░░ ▒▓ ░▒▓░ ▒▒   ▓▒█░▒▓▒░ ░  ░░░ ▒░ ░░ ▒▓ ░▒▓░",
              "           ░▒  ░░▒░ ░ ░   ░▒ ░ ▒░░ ░ ▒  ░   ░ ░▒  ░ ░  ░  ▒     ░▒ ░ ▒░  ▒   ▒▒ ░░▒ ░      ░ ░  ░  ░▒ ░ ▒░",
              "           ░    ░░░ ░ ░   ░░   ░   ░ ░      ░  ░  ░  ░          ░░   ░   ░   ▒   ░░          ░     ░░   ░ ",
              "            ░     ░        ░         ░  ░         ░  ░ ░         ░           ░  ░            ░  ░   ░     ",
              "            ░                                        ░                                                  ",
              "\t\t\t           Coded By | github.com/Fergs32 |",
            };
            foreach (string line in ScraperMenu)
            {
                Colorful.Console.WriteLine(line, Color.White);
            }
        }
    }
}
