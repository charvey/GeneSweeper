using GeneSweeper.AI.Evolution;
using GeneSweeper.AI.Models;
using GeneSweeper.Game;
using GeneSweeper.Game.Boards;
using GeneSweeper.Game.Players;
using GeneSweeper.Util;
using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneSweeper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ns = new NeighborhoodState(
                CellState.Edge.Value, CellState.Edge.Value, CellState.Edge.Value,
                CellState.Edge.Value, CellState.Initial.Value, CellState.Initial.Value,
                CellState.Edge.Value, CellState.Initial.Value, CellState.Initial.Value);

            Console.WriteLine(ns.Value);

            for (int i = 0; i < int.MaxValue; i++)
            {
                if (NeighborhoodState.GetRandom().Value == ns.Value)
                    Console.WriteLine(i);
            }
            Console.ReadLine();
            //MainMenu();
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
                            Board.Difficulty difficulty;
                            if(!Menu.Load(s,"Difficulty", out difficulty))
                                return;

                            new HumanPlayer(difficulty).Play();
                        })}),
                    new Menu("AI Tools",null,new List<Menu>{
                        new Menu("View Trial",null,new List<Menu>{
                            new Menu("Display Stats",s=>
                            {
                                Trial<RuleSetSpecimen> trial;
                                    if(!Menu.Load(s,"Trial",out trial))
                                        return;

                                Console.WriteLine("Generation: " + trial.Generation);
                                Console.WriteLine("Population Size: " + trial.TrialConfig.PopulationSize);
                                Console.WriteLine("Mutation Rate: " + trial.TrialConfig.PopulationSize);
                                Console.WriteLine("Carryover Rate: " + trial.TrialConfig.CarryoverRate);
                            }),
                            new Menu("Evolve",s=>
                            {
                                Trial<RuleSetSpecimen> trial;
                                    if(!Menu.Load(s,"Trial",out trial))
                                        return;

                                trial.Evolve(Menu.PromptFor<Int32>("How many generations?"));
                            }),
                            new Menu("Load Best",null,new List<Menu>{
                                new Menu ("Best Living",s=>{
                                    Trial<RuleSetSpecimen> trial;
                                    if(!Menu.Load(s,"Trial",out trial))
                                        return;

                                    s["Best"] = trial.GetBestLiving();
                                }),
                                new Menu("Best Ever",s=>{
                                    Trial<RuleSetSpecimen> trial;
                                    if(!Menu.Load(s,"Trial",out trial))
                                        return;

                                    s["Best"] = trial.GetBestLiving();
                                })
                            }),
                            new Menu("Run Best",null,new List<Menu>{
                                new Menu("Fast",s=>{
                                    RuleSetSpecimen best;
                                    if(!Menu.Load(s,"Best",out best))
                                        return;

                                    SmartPlayer player = new SmartPlayer(best.RuleSet, new AutoBoard(Board.Difficulty.Beginner));

                                    player.Play();

                                    Console.Out.WriteLine(player);
                                    Console.Out.WriteLine(player.GetCurrentState());
                                }),
                                new Menu("Slow",s=>{
                                    RuleSetSpecimen best;
                                    if(!Menu.Load(s,"Best",out best))
                                        return;

                                    SmartPlayer player = new SmartPlayer(best.RuleSet, new AutoBoard(Board.Difficulty.Beginner));
                                    bool halt;
                                    do
                                    {
                                        halt = player.Step();
                                        Console.Out.WriteLine(player);
                                        Console.Out.WriteLine("Current State: "+player.GetCurrentState());
                                        Console.Out.WriteLine("Steps Since Reveal: "+player.StepsSinceReveal);

                                        Console.ReadLine();
                                    } while (!halt && player.GetCurrentState() == Board.State.Playing && player.StepsSinceReveal < 10);

                                }),
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

        static void TestNumberOfStates()
        {

            Func<int, Tuple<int, int>> test = new Func<int, Tuple<int, int>>(m =>
            {
                int[] powers = Enumerable.Range(0, 10).Select(p => (int)Math.Pow(m, p)).ToArray();
                int states = powers[9];
                ISet<ulong> distinct = new HashSet<ulong>();
                for (int i = 0; i < states; i++)
                {
                    int state = i;

                    var ns = new NeighborhoodState(
                            (byte)((state % powers[0 + 1]) / powers[0]),
                            (byte)((state % powers[1 + 1]) / powers[1]),
                            (byte)((state % powers[2 + 1]) / powers[2]),
                            (byte)((state % powers[3 + 1]) / powers[3]),
                            (byte)((state % powers[4 + 1]) / powers[4]),
                            (byte)((state % powers[5 + 1]) / powers[5]),
                            (byte)((state % powers[6 + 1]) / powers[6]),
                            (byte)((state % powers[7 + 1]) / powers[7]),
                            (byte)((state % powers[8 + 1]) / powers[8])
                        );

                    distinct.Add(ns.Value);
                }
                return new Tuple<int, int>(states, distinct.Count);
            });

            Enumerable.Range(1, 7)
                .Select(test)
                .ToList()
                .ForEach(t => Console.Out.WriteLine("Expected: " + t.Item1 + "\tActual: " + t.Item2));
        }
    }
}
