using System;
using System.Threading;

namespace HW_Threads.Task2
{
    class Column
    {
        static object _lock = new object();
        private bool _firstColActive;
        private bool _secondColActive;

        private int _length1;
        private int _length2;

        private int _rowNumber;
        private int _height = Console.WindowHeight;
        private int _width = Console.WindowWidth;

        private Random _rand;

        private string _str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Column(int rowNum)
        {
            _rowNumber = rowNum;
            _rand = new Random(rowNum * rowNum);
            _length1 = _rand.Next(3, 8);
        }

        public void Start()
        {
            int i = 0;
            int j = 0;
            Thread.Sleep(_rand.Next(10000));
            _firstColActive = true;

            while (true)
            {
                if (i == _height + _length1 && _firstColActive)
                {
                    i = 0;
                    _length1 = _rand.Next(3, 8);
                    _firstColActive = false;

                    if (!_secondColActive)
                    {
                        Thread.Sleep(_rand.Next(5000));
                        _firstColActive = true;
                    }
                }
                if (j == _height + _length2)
                {
                    _secondColActive = false;
                    _firstColActive = true;
                }
                lock (_lock)
                {
                    if (i >= _length1 && _firstColActive)
                    {
                        Console.SetCursorPosition(_rowNumber, i - _length1);
                        Console.Write(" ");
                    }
                    if (i > _length1 && _firstColActive && !_secondColActive && _rand.Next(10) == 1)
                    {
                        _secondColActive = true;
                        _length2 = _rand.Next(3, 8);
                        j = 0;
                    }
                    if (j >= _length2 && _secondColActive)
                    {
                        Console.SetCursorPosition(_rowNumber, j - _length2);
                        Console.Write(" ");
                    }
                    if (i < _height && _firstColActive)
                    {
                        if (i > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;

                            for (int k = i; k > i - _length1 && k >= 0; k--)
                            {
                                Console.SetCursorPosition(_rowNumber, k);
                                Console.Write(_str[_rand.Next(_str.Length)]);
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(_rowNumber, i);
                        Console.Write(_str[_rand.Next(_str.Length)]);
                    }
                    if (j < _height && _secondColActive)
                    {
                        if (j > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;

                            for (int k = j; k > j - _length2 && k >= 0; k--)
                            {
                                Console.SetCursorPosition(_rowNumber, k);
                                Console.Write(_str[_rand.Next(_str.Length)]);
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(_rowNumber, j);
                        Console.Write(_str[_rand.Next(_str.Length)]);
                    }
                }
                if (_firstColActive)
                {
                    i++;
                }
                if (_secondColActive)
                {
                    j++;
                }
                Thread.Sleep(200);
            }
        }
    }
}
