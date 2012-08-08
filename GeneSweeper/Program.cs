using System;

namespace GeneSweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid g = new Grid(8,8);
            Console.WriteLine(g);
            Console.ReadLine();

            RuleSet s = RuleSet.GenerateRandom();
            Console.WriteLine(g.Apply(s) ? "Halt" : "No halt");
            Console.WriteLine(g);
            Console.ReadLine();

            s.Add(new Rule(2635248996351737968));
            Console.WriteLine(g.Apply(s) ? "Halt" : "No halt");
            Console.WriteLine(g);
            Console.ReadLine();

            s.Add(new Rule(2594073385365405936));
            Console.WriteLine(g.Apply(s) ? "Halt" : "No halt");
            Console.WriteLine(g);
            Console.ReadLine();

            s.Add(new Rule(1 << 4));
            Console.WriteLine(g.Apply(s)?"Halt":"No halt");
            Console.WriteLine(g);
            Console.ReadLine();


        }

        static void PlayGame()
        {
            Player player = new HumanPlayer(Board.Difficulty.Small);
            player.Play();
        }
    }
}
