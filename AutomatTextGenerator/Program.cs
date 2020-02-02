using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutomatTextGenerator
{
    /// <summary>
    /// Генератор коллекции словарей для моего лексера из ексельки
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string excelPath = "table.xlsx";
            string path = "AutomatText.txt";

            if ((File.Exists(excelPath)))
            {
                Dictionary<(char, State), State> Dictionary = new Dictionary<(char, State), State>();

                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(excelPath)))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.First();
                    var cells = worksheet.Cells;

                    var numberOfStatus = Convert.ToInt32(cells[1, 1].Value);
                    var numberOfCharacters = Convert.ToInt32(cells[1, 2].Value);
                    using (FileStream fs = File.Create(path))
                    {
                        int pos = 0;
                        for (int i = 2; i < numberOfStatus + 1; i++)
                        {
                            var status = (State)Convert.ToInt32(cells[i, 2].Value);
                            for (int j = 3; j < numberOfCharacters + 2; j++)
                            {
                                pos++;
                                var character = cells[1, j].Value.ToString()[0];
                                var nextStatus = (State)Convert.ToInt32(cells[i, j].Value);

                                if (character == '\'')
                                {
                                    AddText(fs, $"/* {pos} */  Dictionary.Add((\'\\'\', State.{status}), State.{nextStatus});" + '\n');
                                }
                                else if (character == '\\')
                                {
                                    AddText(fs, $"/* {pos} */  Dictionary.Add(('\\\\', State.{status}), State.{nextStatus});" + '\n');
                                } else
                                {
                                    AddText(fs, $"/* {pos} */  Dictionary.Add((\'{character}\', State.{status}), State.{nextStatus});" + '\n');
                                }
                               
                                Dictionary.Add((character, status), nextStatus);
                            }
                        }
                       
                    }

                }
            }

            void AddText(FileStream fs, string value)
            {
                byte[] info = new UTF8Encoding(true).GetBytes(value);
                fs.Write(info, 0, info.Length);
            }
        }

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
            ShiftRight = 33,
            ShiftLeft = 34,
            IntPoint = 35,
            IntDoubleOperator = 36,
            DevisionMultiplication = 37, /* /* */
            MultiComment = 38, /* /* */
            Dog = 39,
            BackSlashException = 40,
            InvalidCharacter,
            ExponentaInt,
            DoubleD,
            Decimal,
            Float,
            Int_,
            ErrorInIdentifier,
            NumberException,
            Double_,
            ExponentaDouble,
            DoubleToDouble,
            DoubleToDecimal,
            DoubleToFloat,
            DoublePoint,
            ErrorInOperator,
            ErrorInEterator,
            ErrorInLogicalExpression,
            FinishMultipleComment,
            CharException,
            CharWithBackSlash,
            CharLineFowardHalf,
            CharLineFoward,
            ShiftException,
            SpeekDog,
            SleepDog,
            ExponentaIntNumberToDouble,
            ExponentaIntPlus,
            ExponentaIntMinus,
            EndOfFile = 99,
        }
    }
}
