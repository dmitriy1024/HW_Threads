using System;
using System.Threading;

namespace HW_Threads.Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                new Thread(new Column(i).Start).Start(); ;
            }
            Console.ReadKey();
        }
    }
}
