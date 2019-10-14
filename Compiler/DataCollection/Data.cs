using System.Collections.Generic;
using System.Linq;

namespace Compiler
{

    class Data
    {
        private readonly char[] limiters = {'[', ']', '{', '}','(' ,')' ,'>' ,'<' , '=', '!', ';', '-', '+', '*', '/', '.' };
        private readonly string[] keyWords =
        {
            "if",
            "else",
            "while",
            "int",
            "double",
            "char",
            "bool",
            "true",
            "false",
            "new",
            "void",
            "return",
            "class",
            "cin",
            "cout",
            "namespace",
            "using",
        };

        /// <summary>
        /// Саписок всех доступных разделителей в языке
        /// </summary>
        public List<char> Limiters { get; set; }
        /// <summary>
        /// Саписок всех доступных зарезервированных слов в языке
        /// </summary>
        public List<string> KeyWords { get; set; }

        public Data()
        {
            Limiters = limiters.ToList();
            KeyWords = keyWords.ToList();
        }
    }
}
