/*
  FILE : Program.cs
* PROJECT :  Assignment 4#
* PROGRAMMER : Mahmood Al-Zubaidi
* FIRST VERSION : 22 Oct/2021
* DESCRIPTION : The purpose of this function is to use threads to insert into the inputted file
* and to moniter the time so that once the file's size exceeds the inputted size, it stops and 
* print the size.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using MutexExample_WriteFile;

namespace ThreadDemo5
{
    class Program
    {
        static volatile bool Run = true;
        static volatile string filePath = "";
        static volatile int size = 0;
        private static Mutex mut;

        static void Main(string[] args)
        {
            if (args.Length == 0 || (args.Length == 1 && args[0] == "") || args.Length > 2)
            {
                Console.WriteLine("Usage[File Name][File Size]");
                Environment.Exit(0);
            }
            else if(args.Contains("/?"))
            {
                Console.WriteLine("Please write the file name that you want to create or open as the first argument, then write the file's name as the second argument");
                Console.WriteLine("Usage[File Name][File Size]");
                Environment.Exit(0);
            }
            if(Int32.Parse(args[1]) < 1000 || Int32.Parse(args[1]) > 20000000)
            {
                Console.WriteLine("File size is outside of range, which between 1000 and 20 000 000");
            }
            {

            }
            filePath = args[0];
            size = Int32.Parse(args[1]);
            if (!File.Exists(filePath))
            {
                FileStream fs = File.Create(filePath);
                fs.Close();
            }


            ThreadRepo tRepo = new ThreadRepo();
            int i = 0;
            while (i < 50)
            {
                i++;
                tRepo.Add("T", new ThreadStart(Go));
            }

            if (!Mutex.TryOpenExisting("MyMutex", out mut))
            {
                mut = new Mutex(true, "MyMutex");
                mut.ReleaseMutex();
            }

            tRepo.StartAll();


            while (true)
            {
                Thread.Sleep(1000);

                FileInfo fi = new FileInfo(filePath);

                Console.WriteLine("Current Size:   " + fi.Length.ToString());
                if (fi.Length >= size)
                {
                    Run = false;
                    Console.WriteLine("Final Size:   " + fi.Length.ToString());
                    
                    break;
                }

            }


            tRepo.JoinAll();
            tRepo.KillAll();

            Console.WriteLine("Finished");
            Console.ReadKey();

        }


        static void Go()
        {

            Random rand = new Random();

            WriteFile wf = new WriteFile();

            try
            {
                while (Run)
                {

                    Thread.Sleep(1000);
                    wf.WriteData(mut, filePath, wordGenrater(50));
                    System.Threading.Thread.Sleep(1000);

                }
            }
            catch (ThreadAbortException ex)
            {
                Console.WriteLine("{0} Aborted", Thread.CurrentThread.Name);
            }
        }




        /** Function: wordGenrater
        * Description: it genrates strings based with the number of letters that you assign to it as an argument
        * Parameters: strLength, wich the represent the number of letters
        * Returns: a string
        */
        private static Random rnd = new Random();
        public static string wordGenrater(int strLength)
        {
            const string charcters = "HIJKLPQRSTMUABGVWXYZNOCDEF";
            return new string(Enumerable.Repeat(charcters, strLength)
              .Select(word => word[rnd.Next(word.Length)]).ToArray());
        }


    }
}
