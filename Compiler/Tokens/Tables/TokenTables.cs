using System.Collections.Generic;
using Compiler.Model;

namespace Compiler.Tokens.Tables
{
    class TokenTables
    {
        public List<Item> Constants { get; set; }
        public List<Item> Identifiers { get; set; }
        public List<Item> KeyWords { get; set; }
        public List<Item> Limiters { get; set; }

        public TokenTables()
        {
            Constants = new List<Item>();
            Identifiers = new List<Item>();
            KeyWords = new List<Item>();
            Limiters = new List<Item>();
        }
    }
}
