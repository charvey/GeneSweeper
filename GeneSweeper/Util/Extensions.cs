﻿using System.Collections.Generic;
using GeneSweeper.AI;
using GeneticAlgorithm;

namespace GeneSweeper.Util
{
    public static class Extensions
    {
        public static string BinaryString(this ulong n)
        {
            char[] b = new char[64];
            int pos = 63;
            int i = 0;

            while (i < 64)
            {
                if ((n & (((ulong)1) << i)) != 0)
                {
                    b[pos] = '1';
                }
                else
                {
                    b[pos] = '0';
                }
                pos--;
                i++;
            }
            return new string(b);
        }

        public static T RandomElement<T>(this List<T> list, bool remove=false)
        {
            int index = (int) Random.NextDouble()*list.Count;
            T element = list[index];

            if(remove)
                list.RemoveAt(index);

            return element;
        }
    }
}
