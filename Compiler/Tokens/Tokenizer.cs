using System;
using System.Collections.Generic;
using System.IO;
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

        private string AllText { get; set; }

        public Tokenizer()
        {
            data = new Data(new DataAnalyzer());
            tables = new TokenTables();
            items = new List<Item>();
        }

        public void GetLine(string line)
        {
            AllText += line + "\n";
        }

        public void StartAnalyzer()
        {
            Item item = new Item();
            int lineNum = 1;

            for (int i = 0; i < AllText.Length; i++)
            {
                if (AllText[i] == '\n')
                {
                    lineNum++;
                    i++;
                }
                #region Apostrof

                if (i >= AllText.Length)
                {
                    Application.Close(item);
                }

                if (AllText[i] == ' ')
                {
                    try
                    {
                        while (AllText[i + 1] == ' ')
                        {
                            i++;
                        }
                    }
                    catch (Exception e)
                    {
                        Application.log.Error(e);
                    }
                };
                
                //считываем текст, указываемый в out(вывод на экран), или в char
                if (AllText[i] == '\'')
                {
                    item.LiteralValue += '\'';
                    item.SymbolPosition = i;
                    item.LinePosition = lineNum;
                    i++;

                    while (AllText[i] != '\'')
                    {
                        item.LiteralValue += AllText[i];
                        i++;
                        if (i == AllText.Length)
                        {
                            item.TypeLiteral = TypeLeksem.NULL;
                            Application.Close(item);
                        }
                    }

                    item.LiteralValue += '\'';
                    i++;
                    item.TypeLiteral = TypeLeksem.Constants;

                    items.Add(item);
                    item = new Item();
                }
                #endregion
                
                if (item.SymbolPosition == 0)
                {
                    item.SymbolPosition = i;
                    item.LinePosition = lineNum;
                }

                item.LiteralValue += AllText[i]; 
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
                if (item.LiteralValue.Length == 1)
                {
                    if (data.LimitersAnalyzer(item))
                    {
                        item.TypeLiteral = TypeLeksem.Limiters;
                        i++;

                        if (item.LiteralValue == "=" || item.LiteralValue == "!")
                        {
                            try
                            {
                                if (AllText[i] == '=')
                                {
                                    item.LiteralValue += AllText[i];
                                    i++;
                                }

                                //to do проверка на недопустимые, либо оставить это дело на синтаксический анализ
                            }
                            catch (Exception e)
                            {
                                Application.log.Error(e);
                            }
                        }

                        items.Add(item);
                        item = new Item();
                    }
                }
            }

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
