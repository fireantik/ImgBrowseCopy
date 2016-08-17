using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgBrowseCopy
{
    class Options
    {
        [Option('s', "source", Required = true, HelpText = "Source path to read images from")]
        public string SourcePath { get; set; }

        [Option('d', "destination", Required = true, HelpText = "Destination path to save images to")]
        public string DestinationPath { get; set; }

        [Option('w', "width", DefaultValue = (ushort)1920, HelpText = "Minimal width of images")]
        public ushort Width { get; set; }

        [Option('h', "height", DefaultValue = (ushort)1080, HelpText = "Minimal height of images")]
        public ushort Height { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var helptext = HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
            helptext.AddPreOptionsLine("Browses images in source folder, displays them and allows user to copy them to destination folder");
            helptext.AddPreOptionsLine("Controls:");
            helptext.AddPreOptionsLine("    Right: Select next image");
            helptext.AddPreOptionsLine("    Left: Select previous image");
            helptext.AddPreOptionsLine("    Enter: Copy image from source to destination");
            helptext.AddPreOptionsLine("");
            return helptext;
        }

        public DirectoryInfo Source => new DirectoryInfo(SourcePath);
        public DirectoryInfo Destination => new DirectoryInfo(DestinationPath);
    }

    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Options options = new Options();
            if (Parser.Default.ParseArguments(args, options))
            {
                if (!options.Source.Exists || !options.Destination.Exists)
                {
                    Console.WriteLine("Source or destination path does not exist");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(options.Source, options.Destination, options.Width, options.Height));
            }
            else
            {
                Console.WriteLine("Command line arguments invalid");
            }
        }
    }
}
