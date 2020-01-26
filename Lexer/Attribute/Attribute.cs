using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lexer
{
    enum Atr
    {
        Default,
        Lexer,
        Syntax
    }
    internal class Attribute
    {
        private readonly List<string> collection;

        public Attribute()
        {
            collection = GetAtrList(new List<string>());
        }

        /// <summary>
        /// Метод проверки введенного атрибута на валидность. Возвращает true, если атрибут существует и false, если нет.
        /// </summary>
        public bool Validation(string attribute)
        {
            return collection.Contains(attribute);
        }

        private List<string> GetAtrList(List<string> collection)
        {
            Type myType = typeof(Atr);
            var fields = myType.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var item in fields)
            {
                collection.Add(item.Name);
            }

            return collection;
        }
    }
}
