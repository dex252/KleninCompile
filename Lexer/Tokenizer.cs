using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using Lexer.Model;

namespace Lexer
{
    class Tokenizer
    {
        private StreamReader Stream { get; set; }
        private int Cur { get; set; }
        private StateTable StateTable { get; set; }
        private int ColumnPos { get; set; }
        private  int RowPos { get; set; }
        private string Temp { get; set; }
        private State State { get; set; }

        public Tokenizer(StreamReader stream, StateTable stateTable)
        {
            Stream = stream;
            RowPos = 0;
            ColumnPos = 0;
            Temp = "";
            Cur = 0;
            State = State.Null;
            StateTable = stateTable;
        }

        public Token GetToken()
        {
            while (State != State.Begin)
            {
                State = State.Begin;
                if (!Eof())
                {
                    var asc = Stream.Read();
                    char literal = (char)asc;
                    //TO DO
                    State = SetNewState(State, asc);
                    if (State != State.Begin) Temp += literal.ToString();
                    else return new Token()
                    {
                        RowPos = RowPos,
                        ColumnPos = ColumnPos,
                        LiteralValue = Temp
                    };

                    Console.WriteLine($"{literal} = {asc}       {State}     {RowPos}    {ColumnPos}");

                    
                }
            }
            
            return null;
        }

        private State SetNewState(State state, int asc)
        {
            TypeLiteral typeLiteral = SetTypeLiteral(asc);
            // state = StateTable.dictionary[(typeLiteral, State)];

            StateTable.dictionary.TryGetValue((typeLiteral, State), out state);

            return state;
        }

        private TypeLiteral SetTypeLiteral(int asc)
        {
            TypeLiteral typeLiteral = TypeLiteral.Null;

            if (asc == 10)
            {
                RowPos++;
            }

            if (asc == 46)
            {
                typeLiteral = TypeLiteral.Point;
            }
            else if (asc >= 65 && asc <= 90 || asc == 95 || asc >= 97 && asc <= 122)//попроавить
            {
                typeLiteral = TypeLiteral.Char;
            }
            else if (asc >= 48 && asc <= 57)
            {
                typeLiteral = TypeLiteral.Number;
            }
            else if (asc == 59 || asc == 44 || asc == 91 || asc == 93 || asc == 40 || asc == 41 || asc == 123 || asc == 125)
            {
                typeLiteral = TypeLiteral.Delimiter;
            }
            else if (asc == 61)
            {
                typeLiteral = TypeLiteral.Equal;
            }
            else if (asc == 33 || asc == 60 || asc == 62)
            {
                typeLiteral = TypeLiteral.ExclamationMoreOrLess;
            }
            else if (asc == 42 || asc == 47)
            {
                typeLiteral = TypeLiteral.Operator;
            }
            else if (asc == 34)
            {
                typeLiteral = TypeLiteral.DoubleQuote;
            }
            else if (asc == 92)
            {
                typeLiteral = TypeLiteral.Comment;
            }
            else if (asc == 45)
            {
                typeLiteral = TypeLiteral.Minus;
            }
            else if (asc == 43)
            {
                typeLiteral = TypeLiteral.Plus;
            }

            return typeLiteral;
        }

        private bool Eof()
        {
            if (Stream.EndOfStream) return true;
            return false;
        }
    }
}
