using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneSweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Board b = new Board(16, 30, 99);
            Console.WriteLine(b);
            b.Flag(5,5);
            Console.WriteLine(b);
            b.Reveal(9,9);
            Console.WriteLine(b);
            */
            Board b = new Board(Board.Difficulty.Small);
            do
            {
                Console.Clear();
                Console.WriteLine(b);
                string[] input = (Console.ReadLine()??"").Split(' ');
                if(input[0][0]=='r')
                    b.Reveal(byte.Parse(input[1]),byte.Parse(input[2]));
                if (input[0][0] == 'f')
                    b.Flag(int.Parse(input[1]), int.Parse(input[2]));
            } while (b.CurrentState == Board.State.Playing);
            Console.WriteLine(b.CurrentState);
            Console.ReadLine();
        }
    }
}
