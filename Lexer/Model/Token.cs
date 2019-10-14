namespace Lexer.Model
{
    public enum TypeLeksem
    {
        Constant,
        Identifier,
        KeyWord,
        Operator,
        Number,
    }

    public class Token
    {
        public int RowPos { get; set; }
        public int ColumnPos { get; set; }
        public string LiteralValue { get; set; }
        public TypeLeksem TypeLeksem { get; set; }
    }
}
