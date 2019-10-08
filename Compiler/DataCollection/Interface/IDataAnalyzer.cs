using System.Collections.Generic;
using Compiler.Model;

namespace Compiler.DataCollection
{
    interface IDataAnalyzer
    {
        bool ConstantsAnalyzer(Item item);
        bool IdentifiersAnalyzer(Item item);
        bool KeyWordsAnalyzer(Item item, List<string> keyWords);
        bool LimitersAnalyzer(Item item, List<char> limiters);
    }
}
