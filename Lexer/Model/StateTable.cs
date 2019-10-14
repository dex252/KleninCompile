namespace Lexer.Model
{
    enum State
    {
        Begin = 0,
        Char = 1,
        Number = 2,
        Operator = 3,
        Comment = 4,
        DoubleQuote = 5,
        Delimiter = 6,
        EqualOrExclamation = 7,
        MoreOrLess = 8,
        Plus = 9,
        Minus = 10,
        Floating = 11,
        DoubleEqual = 12,
        DoublePlus = 13,
        DoubleMinus = 14,
    }

    public class StateTable
    {
        public int[,] table =  new int[15,128];



    }
}
