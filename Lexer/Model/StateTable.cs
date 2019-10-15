using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;

namespace Lexer.Model
{
    public enum State
    {
        Begin = 0,
        Char = 1,
        Number = 2,
        Operator = 3,
        Comment = 4,
        DoubleQuote = 5,
        Delimiter = 6,
        EuqualOrExclamationMoreOrLess = 7,
        Plus = 9,
        Minus = 10,
        Floating = 11,
        DoubleEuqualOrExclamationMoreOrLess = 12,
        DoublePlus = 13,
        DoubleMinus = 14,
        Null = 15,
    }

    public enum TypeLiteral 
    {
        Char,
        Number,
        Operator,
        Comment,
        DoubleQuote,
        Delimiter,
        Equal,
        ExclamationMoreOrLess,
        Plus,
        Minus,
        Null,
        Point,
    }

    public class StateTable
    {
        public Dictionary<(TypeLiteral, State), State> dictionary;
        public Dictionary<(char, string), string> test;

        public StateTable(string excelPath)
        {
            if ((File.Exists(excelPath)))
            {
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(excelPath)))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.First();
                    var cells = worksheet.Cells;

                    var numberOfStatus = Convert.ToInt32(cells[1, 1].Value);
                    var numberOfCharacters = Convert.ToInt32(cells[1, 2].Value);

                    for (int i = 2; i < numberOfStatus + 1; i++)
                    {
                        for (int j = 3; j < numberOfCharacters + 2; j++)
                        {
                            var character = cells[1, j].Value;
                            var status = cells[i, 1].Value.ToString();
                            var nextStatus = cells[Convert.ToInt32(cells[i, j].Value), 1].Value.ToString();
                            Console.WriteLine(nextStatus);
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("Файл excel с таблицей переходов не найден.");
            }
        }
        //public StateTable()
        //{
        //    dictionary = new Dictionary<(TypeLiteral, State), State>();

        //    dictionary = new Dictionary<(TypeLiteral, State), State>
        //    {
        //        /*(<входящий символ>, <текущее состояние>) => <новое состояние>*/
        //        { (TypeLiteral.Char, State.Begin), State.Char },
        //        { (TypeLiteral.Number, State.Begin), State.Number },
        //        { (TypeLiteral.Operator, State.Begin), State.Operator},
        //        { (TypeLiteral.Comment, State.Begin), State.Comment},
        //        { (TypeLiteral.DoubleQuote, State.Begin), State.DoubleQuote},
        //        { (TypeLiteral.Delimiter, State.Begin), State.Delimiter},
        //        { (TypeLiteral.ExclamationMoreOrLess, State.Begin), State.EuqualOrExclamationMoreOrLess},
        //        { (TypeLiteral.Plus, State.Begin), State.Plus},
        //        { (TypeLiteral.Minus, State.Begin), State.Minus},
        //        { (TypeLiteral.Point, State.Begin), State.Delimiter},
        //        { (TypeLiteral.Equal, State.Begin), State.EuqualOrExclamationMoreOrLess},
        //    };

        //    dictionary.Add((TypeLiteral.Char, State.Char), State.Char);
        //    dictionary.Add((TypeLiteral.Number, State.Char), State.Char);

        //    dictionary.Add((TypeLiteral.Number, State.Number), State.Number);
        //    dictionary.Add((TypeLiteral.Point, State.Number), State.Floating);

        //    dictionary.Add((TypeLiteral.Number, State.Floating), State.Floating);

        //    //С комментариями особый случай

        //    dictionary.Add((TypeLiteral.Equal, State.EuqualOrExclamationMoreOrLess), State.DoubleEuqualOrExclamationMoreOrLess);

        //    dictionary.Add((TypeLiteral.Plus, State.Plus), State.DoublePlus);
        //    dictionary.Add((TypeLiteral.Minus, State.Minus), State.DoubleMinus);
        //}

    }
}
