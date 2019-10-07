using System.Collections.Generic;
using System.Linq;
using Compiler.Model;

namespace Compiler.Tokens
{
    class Tokenizer: ITokenizer
    {
        private List<Item> items;
        private List<string> parsLine;

        public void GetLine(string line)
        {
            parsLine = line.Split(' ').ToList();
        }

        public void ParsLine(List<string> parsLine)
        {
            foreach (var item in parsLine)
            {
                //TO DO
                //?Validation after Three
            }

        }

        public void EOF()
        {
            throw new System.NotImplementedException();
        }
    }
}
