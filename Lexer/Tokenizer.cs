using System.IO;
using Lexer.Model;

namespace Lexer
{
    class Tokenizer
    {
        private StreamReader stream;
        private int cur;
        private int ColumnPos { get; set; }
        private  int RowPos { get; set; }
        private string Temp { get; set; }

        public Tokenizer(StreamReader stream)
        {
            this.stream = stream;
            RowPos = 0;
            ColumnPos = 0;
            Temp = "";
            cur = 0;
        }

        public Token GetToken()
        {
            if (Eof())
            {
                char literal = (char)stream.Read();
                Temp = literal.ToString();
                return new Token()
                {
                    RowPos = RowPos,
                    ColumnPos = ColumnPos,
                    LiteralValue = Temp
                };
            }

            return null;
        }

        private bool Eof()
        {
            if (stream.EndOfStream) return false;
            return true;
        }
    }
}
