using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Lexer.Model;

namespace Lexer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Token> tokensList = new List<Token>();
            Token token = new Token();
            string path;
            string excelPath;
            #region Setting

            //Console.WriteLine();

            if (args.Length > 0)
            //if (args.Length == 0)
            {
                //path = Environment.CurrentDirectory + @"\" + "Test.csc";
              //  excelPath = Environment.CurrentDirectory + @"\" + "table.xlsx";
               // excelPath = "table.xlsx";
               excelPath = ConfigurationManager
                   .ConnectionStrings["Lexer.Properties.Settings.ExcelPathToTable"]
                   .ConnectionString;
                path = args[0];
                Atr atr = Atr.Default;

                try
                {
                    var attribute = args[1];
                    var attributeValidation = new Attribute().Validation(attribute);

                    if (attributeValidation)
                    {
                       // Console.WriteLine("Attribute entered: " + attribute);
                      //  Console.WriteLine();
                        atr = (Atr)Enum.Parse(typeof(Atr), attribute);
                    }
                    else
                    {
                        Console.WriteLine("The attribute you entered does not exist: " + attribute +
                                          " ; The default attribute value is set: Default");
                    }

                }
                catch
                {
                   // Console.WriteLine("The default attribute value is set: Default");
                   // Console.WriteLine();
                }

                #endregion

                if (File.Exists(path))
                {
                    using StreamReader stream = File.OpenText(path);
                    Tokenizer tokenizer = new Tokenizer(stream, new StateTable(excelPath));

                    while (token != null)
                    {
                        token = tokenizer.GetToken();
                        if (token != null)
                        {
                            Console.WriteLine($"{token.RowPos}  {token.ColumnPos}   {token.TypeLeksem}  {token.LiteralValue}");
                            tokensList.Add(token);
                        }

                        if (token?.TypeLeksem == TypeLeksem.ErrorException) break;
                    }
                }
                else
                {
                    Console.WriteLine("The file at the specified path does not exist: " + path);
                    Console.WriteLine("--Press AnyKey");
                }
            }
            else
            {
                Console.WriteLine("No arguments");
            }

            //Console.ReadKey();
        }
    }
}
