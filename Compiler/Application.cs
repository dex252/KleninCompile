using System;
using System.IO;
using Compiler.Model;
using Compiler.Tokens;
using NLog;

namespace Compiler
{
    class Application
    {
        public static readonly Logger log = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            #region Setting
            string path;

            Console.WriteLine();

            if (args.Length > 0)
            {
                //path = Environment.CurrentDirectory + @"\" + "Test.csc";
                // path = Environment.CurrentDirectory + @"\" + args[0];
                path = args[0];
                Atr atr = Atr.Default;

                try
                {
                    var attribute = args[1];
                    var attributeValidation = new Attribute().Validation(attribute);

                    if (attributeValidation)
                    {
                        log.Info("Attribute entered: " + attribute);
                        Console.WriteLine("Attribute entered: " + attribute);
                        Console.WriteLine();
                        atr = (Atr) Enum.Parse(typeof(Atr), attribute);
                    }
                    else
                    {
                        log.Error("The attribute you entered does not exist: " + attribute + " ; The default attribute value is set: Default");
                        Console.WriteLine("The attribute you entered does not exist: " + attribute + " ; The default attribute value is set: Default");
                    }

                }
                catch
                {
                    Console.WriteLine("The default attribute value is set: Default");
                    Console.WriteLine();
                    log.Error("The default attribute value is set: Default");
                }
                #endregion

                if (File.Exists(path))
                {
                    Tokenizer tokenizer = new Tokenizer();

                    using StreamReader stream = File.OpenText(path);
                    string line;
                    int count = 0;
                    while ((line = stream.ReadLine()) != null)
                    {
                        count++;
                        tokenizer.GetLine(line, count);
                        tokenizer.StartAnalyzer();
                    }

                    tokenizer.PrintTables();

                    //foreach (var item in tokenizer.GetTable())
                    //{
                    //    Console.WriteLine("{0, 4} | {1, 4} | {2, 12} | {3, 20}",
                    //        item.LinePosition,
                    //        item.SymbolPosition,
                    //        item.TypeLiteral,
                    //        item.LiteralValue);
                    //}

                }
                else
                {
                    log.Error("The file at the specified path does not exist: " + path);
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
        public static void Close(Item error)
        {
            Console.WriteLine($"Error in {error.SymbolPosition} position, in line {error.LinePosition}");
            //Console.ReadKey();
            Environment.Exit(1);
        }
    }
}
