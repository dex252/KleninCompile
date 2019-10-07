using System.ComponentModel;
using Compiler.Tokens.Types;

namespace Compiler.Model
{
    struct Item
    {
        [DisplayName("Номер строки")]
        public int LinePosition { get; set; }
        [DisplayName("Номер символа в строке")]
        public int SymbolPosition { get; set; }
        [DisplayName("Значение литерала")]
        public string LiteralValue { get; set; }
        [DisplayName("Тип литерала")]
        public Token TypeOfLiteral { get; set; }
    }
}
