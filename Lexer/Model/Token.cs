namespace Lexer.Model
{
    public enum TypeLeksem
    {
        ErrorException,
        Char,//
        String,//
        Identifier,
        ReserveWord,
        Operator,
        Integer,//
        Double,//
        Logical,
        Delimiter,
        Equals,
    }

    public class Token
    {
        public int RowPos { get; set; }
        public int ColumnPos { get; set; }
        public string LiteralValue { get; set; }
        public TypeLeksem TypeLeksem { get; set; }
        public string SourceValue { get; set; }

        public override string ToString()
        {
            return LiteralValue;
        }
    }
}
