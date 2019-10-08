using System;
using System.Collections.Generic;
using Compiler.DataCollection;
using Compiler.Model;
using Compiler.Tokens.Tables;

namespace Compiler.Tokens
{
    class Tokenizer: ITokenizer
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
            Item item = new Item();

            for (int i = 0; i < Line.Length; i++)
            {
               
                //удаляем пробелы
                if (Line[i] == ' ')
                {
                    while (Eof(i) && Line[i + 1] == ' ')
                    {
                        i++;
                    }
                }

                #region Apostrof
                //считываем текст, указываемый в out(вывод на экран), или в char
                if (Line[i] == '\'')
                {
                    item.LiteralValue += '\'';
                    item.SymbolPosition = i;
                    item.LinePosition = Count;
                    item.TypeLiteral = TypeLeksem.Constants;

                    if (!Eof(i)) Application.Close(item);
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
                    item = new Item();
                }
                #endregion

                if (!Eof(i)) return;
                i++;



                /*
                 * a
                 * ab
                 * a;
                 * ab;
                 * 1;
                 * 123;
                 * 1s;
                 * 1s
                 * 1/n/n/n/n/n;
                 * =
                 * >=;
                 * > 4
                 * >f
                 * ; /n /n;
                 */

                //проверка на разделители
                //if (item.LiteralValue.Length == 1)
                //{
                //    if (data.LimitersAnalyzer(item))
                //    {
                //        item.TypeLiteral = TypeLeksem.Limiters;
                //        i++;

                //        if (item.LiteralValue == "=" || item.LiteralValue == "!")
                //        {
                //            try
                //            {
                //                if (Line[i] == '=')
                //                {
                //                    item.LiteralValue += Line[i];
                //                    i++;
                //                }

                //                //to do проверка на недопустимые, либо оставить это дело на синтаксический анализ
                //            }
                //            catch (Exception e)
                //            {
                //                Application.log.Error(e);
                //            }
                //        }

                //        items.Add(item);
                //        item = new Item();
                //    }
                //}
            }

        }

        /// <summary>
        /// Проверка существования следущего символа в строке вслед за index. Возвращает true, если символ существует, false - если нет.
        /// </summary>
        private bool Eof(int index)
        {
            if (Line.Length == index + 1)
            {
                return false;
            }

            return true;
        }
        //посмотреть на символ следующий за указанным 
        //private bool EosEquals(int i)
        //{
        //    int next = i;
        //    try
        //    {
        //        if (AllText[next] == '=';
        //        else
        //        {
        //            if (AllText[next] == '\'' || AllText)
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Application.log.Error(e);
        //    }
           
           
        //}
    }
}
