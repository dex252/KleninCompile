using System.Collections.Generic;
using System.Linq;
using Compiler.DataCollection;
using Compiler.Model;
using Compiler.Tokens.Tables;

namespace Compiler.Tokens
{
    class Tokenizer : ITokenizer
    {
        private readonly Data data;
        private TokenTables tables;
        private List<Item> items;

        private string Line { get; set; }
        private int Count { get; set; }

        public Tokenizer()
        {
            data = new Data(new DataAnalyzer());
            tables = new TokenTables();
            items = new List<Item>();
        }

        public void GetLine(string line, int count)
        {
            Line = line;
            Count = count;
        }

        public void StartAnalyzer()
        {
            Item item;

            for (int i = 0; i < Line.Length; i++)
            {

                #region SpaceDelete
                //удаляем пробелы
                if (Line[i] == ' ')
                {
                    while (Eof(i) && Line[i + 1] == ' ')
                    {
                        i++;
                    }
                }
                #endregion

                #region Apostrof
                //считываем текст, указываемый в out(вывод на экран), или в char
                if (Line[i] == '\'')
                {
                    item = new Item()
                    {
                        SymbolPosition = i,
                        LinePosition = Count,
                        TypeLiteral = TypeLeksem.Constants
                    };
                    item.LiteralValue += '\'';

                    if (!Eof(i)) Application.Close(item);//жесткая привязка к строкам
                    i++;

                    while (Line[i] != '\'')
                    {
                        item.LiteralValue += Line[i];

                        if (!Eof(i))
                        {
                            item.SymbolPosition = i;
                            Application.Close(item);
                        }
                        i++;
                    }

                    item.LiteralValue += '\'';

                    items.Add(item);

                    continue;
                }
                #endregion

                #region Constant
                //Проверка на числовые значения
                if (char.IsDigit(Line[i]))
                {
                    item = new Item()
                    {
                        SymbolPosition = i,
                        LinePosition = Count,
                        TypeLiteral = TypeLeksem.Constants
                    };

                    item.LiteralValue += Line[i];

                    if (Eof(i))
                    {
                        while (Eof(i+1) && (char.IsDigit(Line[i+1]) || Line[i+1] == '.'))
                        {
                            item.LiteralValue += Line[i+1];
                            i++;
                        }

                        if (char.IsDigit(Line[i+1]) || Line[i+1] == '.')
                        {
                            item.LiteralValue += Line[i+1];
                        }

                        var value = item.LiteralValue;

                        if (value[value.Length - 1] == '.' || value.Count(x => x == '.') > 1)
                        {
                            Application.Close(item);
                        }

                        items.Add(item);
                    }
                    else
                    {
                        items.Add(item);
                    }

                    continue;
                }
                #endregion

                #region Limiters
                //проверка на разделители
                if (data.Limiters.Contains(Line[i]))
                {
                    item = new Item
                    {
                        LinePosition = Count,
                        SymbolPosition = i,
                        TypeLiteral = TypeLeksem.Limiters
                    };

                    item.LiteralValue += Line[i];

                    if (Eof(i) && (Line[i] == '=' || Line[i] == '!') && Line[i+1] == '=')
                    {
                        i++;
                        item.LiteralValue += Line[i];
                        items.Add(item);
                    }
                    else
                    {
                        items.Add(item);
                    }
                    continue;
                }
                #endregion

                /*
                * a
                * ab
                * a;
                * ab;
                * 1;
                * 123;
                * 1s;
                * 1s
                * ;
                * =
                * >=;
                * > 4
                * >f
                */

                #region Identifiers&KeyWords
                //if (Line[i])
                #endregion

            }

        }

        /// <summary>
        /// Проверка существования следущего символа в строке вслед за index. Возвращает true, если символ существует, false - если нет.
        /// </summary>
        private bool Eof(int index)
        {
            return Line.Length > index + 1;
        }

        public List<Item> GetTable()
        {
            return items;
        }
    }
}
