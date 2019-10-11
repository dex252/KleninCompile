using System;
using System.IO;
using Compiler.Model;
using Compiler.Tokens;
using NLog;

namespace Compiler
{
    enum Atr
    {
        Default,
        One,
        All,
        Any
    }

    class Application
    {
        public static readonly Logger log = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            #region Setting
            string path;

            Console.WriteLine();

            if (args.Length >= 0)
            {
                path = Environment.CurrentDirectory + @"\" + "Test.csc";//= Environment.CurrentDirectory + @"\" + args[0];

                Atr atr = Atr.Default;

                try
                {
                    var attribute = args[1];
                    var attributeValidation = new Attribute().Validation(attribute);

                    if (attributeValidation)
                    {
                        log.Info("Введен атрибут: " + attribute);
                        atr = (Atr) Enum.Parse(typeof(Atr), attribute);
                    }
                    else
                    {
                        log.Error("Введенный атрибут не существует: " + attribute + " ; Установленно значение по умолчанию: Default");
                    }

                }
                catch
                {
                    log.Error("Установленно значение атрибута по умолчанию: Default");
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

                    foreach (var item in tokenizer.GetTable())
                    {
                        Console.WriteLine(item.LinePosition + "   " + item.SymbolPosition + "   " + item.TypeLiteral + "   `" + item.LiteralValue + "`");
                    }

                }
                else
                {
                    log.Error("Файл по указанному пути не существует: " + path);
                }

                Console.WriteLine("--Press AnyKey");
                Console.ReadKey();
            }
        }
        public static void Close(Item error)
        {
            Console.WriteLine($"Ошибка в позиции {error.SymbolPosition}, в строке {error.LinePosition}");
            Console.ReadKey();
            Environment.Exit(1);
        }
    }
}
