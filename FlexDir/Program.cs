using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using FuzzyStrings; 

namespace FlexDir
{

    class ApproximateMatcher
    {
        public static void Approximate(string a, string b )
        {
            {
                double matchResult = DiceCoefficientExtensions.DiceCoefficient(a, b);
                Console.WriteLine(a + " | " + b + " | " + matchResult); 
            }
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            string DEFAULT_DIRECTORY = @"H:\Informatics\Flash\_container\";
            string CURRENT_DIRECTORY;

            /** check is the default directory exists **/
            if (Directory.Exists(DEFAULT_DIRECTORY))
            {
                /** Changes CURRENT_DIRECTORY to DEFAULT_DIRECTORY **/
                CURRENT_DIRECTORY = DEFAULT_DIRECTORY;
            }
            else
            {
                Console.WriteLine(DEFAULT_DIRECTORY);
                CURRENT_DIRECTORY = Directory.GetCurrentDirectory();
            }

            List<string> files = new List<string>();

            /** Get all Files **/
            files.AddRange(Directory.GetFiles(CURRENT_DIRECTORY));

            int filesize = files.Count;
            for (int i = 10; i < files.Count; )
            {
                files.RemoveAt(i);
            }

            Console.WriteLine("Files in Directory: " + CURRENT_DIRECTORY);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(files[i]);
            }

            Console.WriteLine("\nRegex Sorted\n");

            /** Get your sorting regex **/
            //Regex fileSortRegex = new Regex(@"\[.*?\]");
            Regex fileSortRegex = new Regex(@"(?<=\]).*?(?=\[)");
            for (int i = 0; i < files.Count; i++)
            {
                Match regexMatch = fileSortRegex.Match(files[i]);
                if (regexMatch.ToString().Trim() == String.Empty) { continue; } // skips the empty ones. 
                for (int j = 0; j < files.Count; j++)
                {
                    if (files[j].Trim() == String.Empty) { continue; } // skips the empty ones.

                    if (i == j) { continue; }
                    ApproximateMatcher.Approximate( fileSortRegex.Match(files[i]).ToString() , fileSortRegex.Match(files[j]).ToString() ); 
                }
            }

            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
