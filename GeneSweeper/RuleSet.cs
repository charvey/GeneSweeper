using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeneSweeper
{
    public class RuleSet
    {
        private const int LENGTH = 100000000;
        private BitArray _ruleData;

        public RuleSet()
        {
            _ruleData = new BitArray(LENGTH);
        }

        private RuleSet(byte[] bytes)
        {
            if(bytes.Length!=LENGTH/8)
                throw new ArgumentException("Incorrect ruleset size.");

            _ruleData = new BitArray(bytes);
        }

        public void SaveToFile(string filename="file")
        {
            byte[] bytes = new byte[LENGTH/8];
            _ruleData.CopyTo(bytes, 0);
            File.WriteAllBytes(filename + ".gsr", bytes);
        }

        public static RuleSet ReadFromFile(string filename = "file")
        {
            byte[] bytes = File.ReadAllBytes(filename + ".gsr");
            return new RuleSet(bytes);
        }

        public bool GetRuling()
        {
            throw new NotImplementedException();
        }
    }
}
