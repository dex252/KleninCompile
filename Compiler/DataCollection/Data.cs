using System.Collections.Generic;
using System.Linq;
using Compiler.DataCollection;
using Compiler.Model;

namespace Compiler
{

    class Data
    {
        private readonly char[] limiters = {'[', ']', '{', '}','(' ,')' ,'>' ,'<' , '=', '!', ';', '-', '+', '*', '/', ',', '.' };
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
            "out"
        };

        /// <summary>
        /// Саписок всех доступных разделителей в языке
        /// </summary>
        public List<char> Limiters { get; set; }
        /// <summary>
        /// Саписок всех доступных зарезервированных слов в языке
        /// </summary>
        public List<string> KeyWords { get; set; }

        /// <summary>
        /// Логика для проверки строк и символов на их принадлежность к разделителям/ключевым словам/константам/идентификаторам;
        /// </summary>
        public IDataAnalyzer DataAnalyzer;

        public Data(IDataAnalyzer dataAnalyzer)
        {
            Limiters = limiters.ToList();
            KeyWords = keyWords.ToList();
            this.DataAnalyzer = dataAnalyzer;
        }

        public bool LimitersAnalyzer(Item item)
        {
            if (DataAnalyzer.LimitersAnalyzer(item, Limiters))
            {
                return true;
            };

            return false;
        }

        public TypeLeksem Analyzer(Item item)
        {
            if (DataAnalyzer.ConstantsAnalyzer(item))
            {
                return TypeLeksem.Constants;
            };

            if (DataAnalyzer.IdentifiersAnalyzer(item))
            {
                return TypeLeksem.Identifiers;
            };

            if (DataAnalyzer.KeyWordsAnalyzer(item, KeyWords))
            {
                return TypeLeksem.KeyWords;
            };

            

            return TypeLeksem.NULL;
        }
    }
}
