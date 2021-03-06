﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneSweeper.Util
{
    public class Menu
    {
        public readonly string Text;
        public readonly Action<Dictionary<string, object>> Oper;
        public readonly List<Menu> SubMenus;

        public Menu(string text, Action<Dictionary<string, object>> oper, List<Menu> subMenus = null)
        {
            Text = text;
            Oper = oper;
            SubMenus = subMenus;
        }

        public void Display(Dictionary<string, object> state)
        {
            Menu choice;
            int input;

            while (true)
            {
                //Display Menu
                Console.Out.WriteLine(Text);
                for (int i = 1; i <= SubMenus.Count; i++)
                {
                    Console.Out.WriteLine(i + ". " + SubMenus[i - 1].Text);
                }
                Console.Out.WriteLine((SubMenus.Count + 1) + ". Exit");

                //Read Choice
                if (int.TryParse(Console.In.ReadLine(), out input))
                {
                    if (input == SubMenus.Count + 1)
                    {
                        return;
                    }
                    else if (1 <= input && input <= SubMenus.Count)
                    {
                        choice = SubMenus[input - 1];
                    }
                    else
                    {
                        choice = null;
                    }
                }
                else
                {
                    choice = null;
                }

                //Do Choice
                Console.WriteLine();
                if (choice != null)
                {
                    if (choice.SubMenus != null)
                    {
                        choice.Display(state);
                    }
                    if (choice.Oper != null)
                    {
                        choice.Oper(state);
                    }
                }
            }
        }

        public static T PromptFor<T>(string prompt) where T : IConvertible
        {
            while (true)
            {
                Console.WriteLine(prompt);

                try
                {
                    return (T) Convert.ChangeType(Console.ReadLine(), typeof (T));
                }
                catch (Exception)
                {
                    Console.WriteLine("Input must be of type: " + typeof (T).Name);
                }
            }
        }

        public static bool Load<T>(Dictionary<string, object> state, String name, out T variable) where T:class
        {
            if (!state.ContainsKey(name))
            {
                Console.WriteLine(name + " must be loaded before using");
                variable = null;
                return false;
            }

            variable = state[name] as T;

            if (variable == null)
            {
                Console.WriteLine(name+" is not of type "+typeof(T));
                return false;
            }

            return true;
        }
    }
}