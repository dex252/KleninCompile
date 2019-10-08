using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Compiler.Model;

namespace Compiler.DataCollection
{
    class DataAnalyzer : IDataAnalyzer
    {
        public bool ConstantsAnalyzer(Item item)
        {
            throw new System.NotImplementedException();
        }

        public bool IdentifiersAnalyzer(Item item)
        {
            throw new System.NotImplementedException();
        }

        public bool KeyWordsAnalyzer(Item item, List<string> keyWords)
        {
            throw new System.NotImplementedException();
        }

        public bool LimitersAnalyzer(Item item, List<char> limiters)
        {
            char limiter = item.LiteralValue[0];

            if (limiters.Contains(limiter)) return true;
            return false;
        }
    }
}
