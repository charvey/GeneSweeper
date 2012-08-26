using System;
using GeneSweeper.Game;
using GeneSweeper.Game.Players;
using System.Collections.Generic;
using GeneticAlgorithm;

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
            Action<Dictionary<string, object>> todo = (s) => Console.Out.WriteLine("TODO");

            var menu = new Menu("Main Menu", null, new List<Menu>
                {
                    new Menu("Play a Game",null,new List<Menu>{
                        DifficultyMenu,
                        new Menu("Play Game",(s)=>{
                            if (s.ContainsKey("Difficulty") && s["Difficulty"] is Board.Difficulty)
                                (new HumanPlayer((Board.Difficulty)s["Difficulty"])).Play();
                            else
                                Console.WriteLine("Please Select Difficulty");
                        })}),
                    new Menu("AI Tools",null,new List<Menu>{
                        new Menu("View Trial",null,new List<Menu>{
                            new Menu("Display Stats",s=>
                            {
                                Trial<RuleSetSpecimen> trial = s["Trial"] as Trial<RuleSetSpecimen>;

                                Console.WriteLine("Generation: " + trial.Generation);
                            }),
                            new Menu("Evolve",s=>(s["Trial"] as Trial<RuleSetSpecimen>).Evolve()),
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
                        new Menu("New Trial",(s)=>
                        {
                            s["Trial"] = new Trial<RuleSetSpecimen>(
                                Menu.PromptFor<string>("Enter Name"),
                                    new TrialConfiguration<RuleSetSpecimen>
                                    {
                                        CarryoverRate = Menu.PromptFor<Double>("Enter Carryover Rate"),
                                        MutationRate = Menu.PromptFor<Double>("Enter Mutation Rate"),
                                        PopulationSize = Menu.PromptFor<Int32>("Enter Population Size"),

                                        Stringer = new RuleSetSpecimenStringer()
                                    }
                                );
                        }),
                        new Menu("Load Trial",s=>
                        {
                            s["Trial"] = new Trial<RuleSetSpecimen>(
                                Menu.PromptFor<string>("Enter Name"),
                                new RuleSetSpecimenStringer());
                        })
                    })
                });

            var state = new Dictionary<string, object>();

            menu.Display(state);
        }
    }
}
