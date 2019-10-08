using System.ComponentModel;

namespace Compiler.Model
{
    enum TypeLeksem
    {
        Constants,
        Identifiers,
        KeyWords,
        Limiters,
        NULL
    }
    struct Item
    {
        [DisplayName("Номер строки")]
        public int LinePosition { get; set; }
        [DisplayName("Номер символа в строке")]
        public int SymbolPosition { get; set; }
        [DisplayName("Значение лексемы")]
        public string LiteralValue { get; set; }
        [DisplayName("Тип лексемы")]
        public TypeLeksem TypeLiteral { get; set; }
    }
}
