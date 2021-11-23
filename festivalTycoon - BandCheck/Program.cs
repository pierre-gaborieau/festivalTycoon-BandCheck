using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace festivalTycoon___BandCheck
{
    class Program
    {

        public static void WriteAllLinesBetter(string path, params string[] lines)
        {
            if (path == null)
                throw new ArgumentNullException("path");
            if (lines == null)
                throw new ArgumentNullException("lines");

            using (var stream = File.OpenWrite(path))
            {
                stream.SetLength(0);
                using (var writer = new StreamWriter(stream))
                {
                    if (lines.Length > 0)
                    {
                        for (var i = 0; i < lines.Length - 1; i++)
                        {
                            writer.WriteLine(lines[i]);
                        }
                        writer.Write(lines[lines.Length - 1]);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            string answer;
            Console.WriteLine("Welcome to festivalTycoon - BandCheck");
            Console.WriteLine("This console is here to help you aggregate, multiple bands files and to remove duplicates names.");
            Console.WriteLine("Before starting the process, make sure to open this program in the same folder as the multiples files");
            
            do
            {
                Console.WriteLine("Can we continue ? (y/n)");
                answer = Console.ReadLine().ToLower();

            } while (!(answer == "y" || answer == "n"));

            if(answer == "y")
            {
                File.Delete("bands_bandcheck.txt");
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                List<string> fileNames = new List<string>();
                List<string> bands = new List<string>();
                fileNames.AddRange(Directory.GetFiles(dir, "*.txt"));
                if(fileNames.Count() > 0)
                {
                    foreach(string file in fileNames)
                    {
                        List<string> tempA = File.ReadAllLines(file).ToList();
                        tempA.Remove(tempA[0]);

                        foreach(string band in tempA)
                        {
                            bool exist = false;
                            foreach(string item in bands)
                            {
                                if(item.ToLower() == band.ToLower())
                                {
                                    exist = true;
                                    break;
                                }
                            }
                            if (!exist)
                            {
                                bands.Add(band);
                            }
                        }
                    }

                    bands = bands.OrderBy(a => a).ToList();
                    bands.Insert(0, "Created with festivalTycoon - bandcheck");
                    WriteAllLinesBetter("bands_bandcheck.txt", bands.ToArray());
                    Console.WriteLine("Program is ended, you can move 'bands_bandcheck.txt' to 'C:\\Documents\\Festival Tycoon\\UserFiles\\Names\\' to get your bands inside the game !");
                }
            }
            else
            {
                Console.WriteLine("Please move your files to the folder and restart the app.");
            }
            Console.ReadLine();
        }
    }


}


