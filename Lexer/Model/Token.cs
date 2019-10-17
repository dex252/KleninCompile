namespace Lexer.Model
{
    public enum TypeLeksem
    {
        ErrorException,
        Char,
        Identifier,
        ReserveWord,
        Operator,
        Integer,
        Double,
        Logical,
        Delimiter,
        String,
    }

    public class Token
    {
        public int RowPos { get; set; }
        public int ColumnPos { get; set; }
        public string LiteralValue { get; set; }
        public TypeLeksem TypeLeksem { get; set; }
        public string SourceValue { get; set; }
    }
}
