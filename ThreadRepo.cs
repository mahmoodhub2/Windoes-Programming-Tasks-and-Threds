//*TITLE : WriteFile.cs
//       * AUTHOR : Norbert Mika
//        * DATE : 2021 - 10 - 22
//        * VERSION : 
//        *AVAILABIILTY : https://conestoga.desire2learn.com/d2l/le/content/483719/viewContent/10898393/View


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadDemo5
{
    class ThreadRepo
    {
        List<Thread> threads = new List<Thread>();
        public ThreadRepo()
        { }
        // Adds the threads
        public void Add(string name, ThreadStart ts)
        {
            Thread t = new Thread(ts);
            t.Name = name;
            threads.Add(t);
        }

        // starts the threads
        public void StartAll()
        {
            foreach (Thread t in threads)
            {
                t.Start();
            }
        }

        // joins the threads
        public void JoinAll()
        {
            foreach (Thread t in threads)
            {
                t.Join();
            }
        }

        // terminates all the threads the threads
        public void KillAll()
        {
            foreach (Thread t in threads)
            {
                t.Abort();
            }
        }

    }
}
