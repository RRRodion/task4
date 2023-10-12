using ConsoleApp1;
using System;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(args);
            game.Start();
        }
    }
}