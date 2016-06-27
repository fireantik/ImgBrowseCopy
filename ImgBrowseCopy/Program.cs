using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgBrowseCopy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Arg 1: source path");
            Console.WriteLine("Arg 2: destination path");
            Console.WriteLine("Arg 3: min width [default=0]");
            Console.WriteLine("Arg 4: min height [default=0]");

            if (args.Length < 2) return;

            DirectoryInfo source, dest;
            int width, height;

            try
            {
                source = new DirectoryInfo(args[0]);
                dest = new DirectoryInfo(args[1]);

                if (!source.Exists || !dest.Exists)
                {
                    Console.WriteLine("Source or destination does not exist");
                    return;
                }

                width = args.Length >= 3 ? int.Parse(args[2]) : 0;
                height = args.Length >= 4 ? int.Parse(args[3]) : 0;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(source, dest, width, height));
        }
    }
}
