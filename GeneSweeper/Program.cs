using System;
using GeneSweeper.AI;
using GeneSweeper.Game;
using GeneSweeper.Game.Players;
using System.Collections.Generic;

namespace GeneSweeper
{
    public class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            var menu = new Menu("Main Menu", null, new List<Menu>
                {
                    new Menu("Play a Game",null,new List<Menu>{
                        new Menu("Beginner",(s)=>{s.Remove("Difficulty");s.Add("Difficulty",Board.Difficulty.Beginner);}),
                        new Menu("Intermediate",(s)=>{s.Remove("Difficulty");s.Add("Difficulty",Board.Difficulty.Intermediate);}),
                        new Menu("Advanced",(s)=>{s.Remove("Difficulty");s.Add("Difficulty",Board.Difficulty.Advanced);})
                    })
                });

            var state = new Dictionary<string, object>();

            menu.Display(state);
        }
    }
}
