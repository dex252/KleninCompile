using System;
using System.IO;
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
            string path;

            Console.WriteLine();

            if (args.Length > 0)
            {
                path = Environment.CurrentDirectory + @"\" + args[0];

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

                if (!File.Exists(path))
                {
                    Tokenizer tokenizer = new Tokenizer();

                    using (StreamReader stream = File.OpenText(path))
                    {
                        string line;

                        while ((line = stream.ReadLine()) != null)
                        {
                            tokenizer.GetLine(line);

                        }
                    }
                }
                else
                {
                    log.Error("Файл по указанному пути не существует");
                }

            }
        }
    }
}
