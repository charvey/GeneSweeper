using System;
using GeneSweeper.AI;
using GeneSweeper.Game;
using GeneSweeper.Game.Players;
using System.Collections.Generic;

namespace GeneSweeper
{
    class Program
    {
        private struct Choice
        {
            public string Text;
            public Func<bool> Oper;
        }

        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            List<Choice> choices = new List<Choice> {
                new Choice{Text="Play a Game",Oper=PlayGame},
                new Choice{Text="Exit", Oper=()=>true}
            };

            Choice choice = new Choice();
            int input;
            do
            {
                for (int i = 1; i <= choices.Count; i++)
                {
                    Console.Out.WriteLine(i + ". " + choices[i - 1].Text);
                }

                if (int.TryParse(Console.In.ReadLine(), out input))
                {
                    if (1 <= input && input <= choices.Count)
                    {
                        choice = choices[input - 1];
                    }
                }
                else
                {
                    choice = new Choice();
                }
            } while (choice.Oper != null && !choice.Oper());
        }

        static bool Test()
        {
            Grid g = new Grid(8, 8);
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
            Console.WriteLine(g.Apply(s) ? "Halt" : "No halt");
            Console.WriteLine(g);
            Console.ReadLine();

            return false;
        }

        static bool PlayGame()
        {
            Player player = new HumanPlayer(Board.Difficulty.Small);
            player.Play();

            return false;
        }
    }
}
