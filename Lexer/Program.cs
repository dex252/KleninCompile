using System;
using System.Collections.Generic;
using System.IO;
using Lexer.Model;

namespace Lexer
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            #region Setting

            //if (args.Length > 0)
            if (args.Length == 0)
            {
                path = Environment.CurrentDirectory + @"\" + "Test.csc";
   
                //path = args[0];
                Atr atr = Atr.Default;

                try
                {
                    var attribute = args[1];
                    var attributeValidation = new Attribute().Validation(attribute);

                    if (attributeValidation)
                    {
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
                    Syntax Syntax = new Syntax(path);
                    Syntax.SyntaxAnalyzer();
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

            Console.ReadKey();
        }
    }
}
