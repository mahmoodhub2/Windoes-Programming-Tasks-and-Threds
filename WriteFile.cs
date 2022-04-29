//*TITLE : WriteFile.cs
//       * AUTHOR : Norbert Mika
//        * DATE : 2021 - 10 - 22
//        * VERSION : 
//        *AVAILABIILTY : https://conestoga.desire2learn.com/d2l/le/content/483719/viewContent/10898393/View


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace MutexExample_WriteFile
{
    class WriteFile
    {
        const string directory = @"c:\ma hmood\";
        StreamWriter file = null;

        /** Function: WriteData
        * Description: it writes data to the file after it makes the previus thread wait in line
        * Parameters: Mutex mut represnts the mutex, string filename represents the filepath, string data, in which you wanna insert into the file
        * Returns: string, which is the result.
        */
        public string WriteData(Mutex mut, string filename, string data)
        {
            string filePath = filename;
            string result = "OK";
            mut.WaitOne();
            try
            {
                file = File.AppendText(filename);
                file.WriteLine(data);
            }
            catch (Exception ex)
            {
                result = "Exception: " + ex.Message;
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                    file = null;
                }
            }
            mut.ReleaseMutex();
            return result;
        }



        /** Function: WriteData
        * Description: it writes data to the file after it makes the previus thread wait in line
        * Parameters: string filename represents the filepath, string data, in which you wanna insert into the file
        * Returns: string, which is the result.
        */
        public string WriteData(string filename, string data)
        {
            string filePath = directory + filename + ".txt";
            string result = "OK";
            try
            {
                file = File.AppendText(filename);
                file.WriteLine(data);
            }
            catch (Exception ex)
            {
                result = "Exception: " + ex.Message;
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                    file = null;
                }
            }
            return result;
        }

    }
}
