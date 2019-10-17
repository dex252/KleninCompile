﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace Lexer.Model
{

    public enum State
    {
        Indefinitely = 0,
        Begin = 1,
        Identifier = 2,
        ReserveWord = 3,
        Word = 4,
        Int = 5,
        Double = 6,
        Operator = 7,
        Delimiters = 8,
        Logical = 9,
        ErrorException = 10,
        Plus = 11,
        PlusPlus = 12,
        Minus = 13,
        MinusMinus = 14,
        Equal = 15,
        EqualEqual = 16,
        LogicInequality = 17, /* ! */
        Inequality = 18, /* != */
        More = 19,
        MoreEqual = 20,
        Less = 21,
        LessEqual = 22,
        Ampersand = 23, /* & */
        DoubleAmpersand = 24,
        Or = 25, /* | */
        DoubleOr = 26,
        Devision = 27, /* / */
        Comment = 28, /*  */
        Acute = 29, /* ' */
        HalfChar = 30,
        Char = 31,
        String = 32,
        EndOfFile = 33,
    }
    public class StateTable
    {
        public Dictionary<(char, State), State> Dictionary { get; set; }
        public Dictionary<string, bool> ReserveWords { get; set; } = new Dictionary<string, bool>()
        {
            {"if", true} ,
            {"else", true} ,
            {"while", true},
            {"int", true},
            {"double",true},
            {"char",true},
            {"bool",true},
            {"true",true},
            {"false",true},
            {"new",true},
            {"void",true},
            {"return",true},
            {"class",true},
            {"cin",true},
            {"cout",true},
            {"namespace",true},
            {"using",true},
        };


        public StateTable(string excelPath)
        {
            if ((File.Exists(excelPath)))
            {
                Dictionary = new Dictionary<(char, State), State>();

                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(excelPath)))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.First();
                    var cells = worksheet.Cells;

                    var numberOfStatus = Convert.ToInt32(cells[1, 1].Value);
                    var numberOfCharacters = Convert.ToInt32(cells[1, 2].Value);


                    for (int i = 2; i < numberOfStatus + 1; i++)
                    {
                        var status = (State)Convert.ToInt32(cells[i, 2].Value);
                        for (int j = 3; j < numberOfCharacters + 2; j++)
                        {
                            var character = cells[1, j].Value.ToString()[0];
                            var nextStatus = (State)Convert.ToInt32(cells[i, j].Value);

                            Dictionary.Add((character, status), nextStatus);
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("Файл excel с таблицей переходов не найден.");
            }
        }
    }
}
