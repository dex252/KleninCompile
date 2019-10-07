using System.Collections.Generic;

namespace Compiler.Tokens
{
    interface ITokenizer
    {
        void GetLine(string literal);
        
        void ParsLine(List<string> parsLine);

        void EOF();
    }
}
