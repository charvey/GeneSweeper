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
            Menu DifficultyMenu = new Menu("Select Difficulty", null, new List<Menu>{
                        new Menu("Beginner",(s)=>{s["Difficulty"]=Board.Difficulty.Beginner;}),
                        new Menu("Intermediate",(s)=>{s["Difficulty"]=Board.Difficulty.Intermediate;}),
                        new Menu("Advanced",(s)=>{s["Difficulty"]=Board.Difficulty.Advanced;})
                    });
            Action<Dictionary<string, object>> todo = (s) => { Console.Out.WriteLine("TODO"); };

            var menu = new Menu("Main Menu", null, new List<Menu>
                {
                    new Menu("Play a Game",null,new List<Menu>{
                        DifficultyMenu,
                        new Menu("Play Game",todo)}),
                    new Menu("AI Tools",null,new List<Menu>{
                        new Menu("View Trial",null,new List<Menu>{
                            new Menu("Display Stats",todo),
                            new Menu("Evolve",todo),
                            new Menu("Load Best",null,new List<Menu>{
                                new Menu ("Best Living",todo),
                                new Menu("Best Ever",todo)
                            }),
                            new Menu("Run Best",null,new List<Menu>{
                                new Menu("Fast",todo),
                                new Menu("Slow",todo),
                                new Menu("Manual",todo)
                            })
                        }),
                        new Menu("New Trial",todo),
                        new Menu("Load Trial",todo),
                        new Menu("Close Trial",todo)
                    })
                });

            var state = new Dictionary<string, object>();

            menu.Display(state);
        }
    }
}
