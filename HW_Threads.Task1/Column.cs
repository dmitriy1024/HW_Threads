using System;
using System.Threading;

namespace HW_Threads.Task1
{
    class Column
    {
        static object _lock = new object();
        private int _length;
        private int _rowNumber;
        private int _height = Console.WindowHeight;
        private int _width = Console.WindowWidth;

        private Random _rand;

        private string _str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Column(int rowNum)
        {
            _rowNumber = rowNum;
            _rand = new Random(rowNum * rowNum);
            _length = _rand.Next(3, 8);
        }

        public void Start()
        {
            int i = 0;
            Thread.Sleep(_rand.Next(10000));

            while (true)
            {
                if (i == _height + _length)
                {
                    i = 0;
                    _length = _rand.Next(3, 8);
                    Thread.Sleep(_rand.Next(5000));
                }
                lock (_lock)
                {
                    if (i >= _length)
                    {
                        Console.SetCursorPosition(_rowNumber, i - _length);
                        Console.Write(" ");
                    }
                    if (i < _height)
                    {
                        if (i > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            for (int k = i; k > i - _length && k >= 0; k--)
                            {
                                Console.SetCursorPosition(_rowNumber, k);
                                Console.Write(_str[_rand.Next(_str.Length)]);
                            }                                                                              
                        }

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(_rowNumber, i);
                        Console.Write(_str[_rand.Next(_str.Length)]);
                    }
                }
                i++;
                Thread.Sleep(150);
            }
        }
    }
}
