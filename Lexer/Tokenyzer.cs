using System;
using System.Collections.Generic;
using System.IO;
using Lexer.Model;

namespace Lexer
{
    class Tokenyzer
    {
        private StreamReader Stream { get; }
        public Dictionary<(char, State), State> Dictionary { get; set; }
        public HashSet<string> ReserveWords { get; set; }
        private int ColumnPos { get; set; }
        private int RowPos { get; set; }
        private int Cur { get; set; }
        private string Temp { get; set; }
        private State State { get; set; }

        public Tokenyzer(StreamReader stream, StateTable stateTable)
        {
            Stream = stream;
            RowPos = 1;
            ColumnPos = 0;
            Cur = 0;
            Dictionary = stateTable.Dictionary;
            ReserveWords = stateTable.ReserveWords;
        }
        /**** ////////******/
        public Token GetToken()
        {
            State = State.Begin;
            Temp = "";
            while (State != State.EndOfFile)
            {
                if (Stream.Peek() != -1)
                {
                    var oldState = State;

                    var current = Stream.Peek();
                    var curr = (char)Stream.Peek();

                    State = SetNewState(State, current);

                    if (State == State.Float || State == State.DoubleToFloat || State == State.DoubleToFloat || State == State.Decimal)
                    {
                        Temp = Temp.Replace("_", "");
                    }

                    if (State == State.Word || State == State.String)
                    {
                        if (Temp.Contains(@"\n"))
                        {
                            int IndexFirst = Temp.IndexOf(@"\n");
                            Temp = Temp.Remove(IndexFirst, @"\n".Length).Insert(IndexFirst, "\n");

                        }
                    }

                    if (State == State.CharLineFoward)
                    {
                        int IndexFirst = Temp.IndexOf(@"\n");
                        Temp = Temp.Remove(IndexFirst, @"\n".Length).Insert(IndexFirst, "\n");
                    }

                    if (oldState == State.DevisionMultiplication || State == State.MultiComment || oldState == State.MultiComment)
                    {
                        switch (current)
                        {
                            case 32:
                            case 9:
                                {
                                    ColumnPos++;
                                    State = State.DevisionMultiplication;
                                    Stream.Read();
                                    continue;
                                }

                            case 10:
                                {


                                    RowPos++;
                                    State = State.DevisionMultiplication;
                                    Stream.Read();
                                    ColumnPos = 0;
                                    continue;

                                }

                            case 13:
                                {
                                    State = State.DevisionMultiplication;
                                    Stream.Read();
                                    continue;
                                }
                            case '*':
                                {
                                    if (State == State.MultiComment)
                                    {
                                        Temp += (char)current;
                                        ColumnPos++;
                                        Stream.Read();
                                    }
                                    continue;
                                }

                            case '\\':
                                {
                                    if (State == State.MultiComment)
                                    {
                                        Temp += (char)current;
                                        ColumnPos++;
                                        Stream.Read();
                                    }
                                    continue;
                                }
                        }
                    }

                    if (State == State.FinishMultipleComment)
                    {
                        Temp += (char)current;
                        ColumnPos++;
                        Stream.Read();
                        RefreshState();
                        continue;

                    }

                    if (State == State.IntDoubleOperator)
                    {
                        Stream.DiscardBufferedData();
                        Stream.BaseStream.Seek(Cur - 1, System.IO.SeekOrigin.Begin);
                        Cur--;
                        string value = Temp.Remove(Temp.Length - 1, 1);

                        return new Token()
                        {
                            LiteralValue = value,
                            RowPos = RowPos,
                            ColumnPos = ColumnPos - Temp.Length,
                            TypeLeksem = TypeLeksem.Integer
                        };
                    }

                    //экранируем от пробелов, табуляций, переходов строк, переноса корретки и символов, не входящих в алфавит
                    if ((oldState == State.Begin) && State == State.Indefinitely)
                    {
                        Stream.Read();
                        Cur++;
                        switch (current)
                        {
                            case 32:
                            case 9:
                                {
                                    RefreshState();
                                    ColumnPos++;
                                    continue;
                                }

                            case 10:
                                {
                                    if (oldState == State.String)
                                    {
                                        RowPos++;
                                        State = State.String;
                                        Stream.Read();
                                        ColumnPos = 0;
                                        continue;
                                    }

                                    RefreshState();
                                    RowPos++;
                                    ColumnPos = 0;
                                    continue;
                                }

                            case 13:
                                {
                                    RefreshState();
                                    continue;
                                }

                            default:
                                {
                                    Temp += (char)current;
                                    ColumnPos++;
                                    return new Token()
                                    {
                                        LiteralValue = Temp,
                                        RowPos = RowPos,
                                        ColumnPos = ColumnPos - Temp.Length + 1,
                                        TypeLeksem = TypeLeksem.ErrorException
                                    };
                                }
                        }
                    }

                    switch (State)
                    {
                        case State.Indefinitely:
                            {
                                if (oldState == State.Identifier)
                                {
                                    if (ReserveWords.Contains(Temp)) oldState = State.ReserveWord;
                                }

                                return new Token()
                                {
                                    ColumnPos = ColumnPos - Temp.Length + 1,
                                    RowPos = RowPos,
                                    SourceValue = Temp,
                                    LiteralValue = SourceValueToLiteralValue(Temp, oldState),
                                    TypeLeksem = GetLeksemType(oldState)
                                };
                            }

                        case State.Comment:
                            {
                                while (Stream.Peek() != 10 && Stream.Peek() != -1)
                                {
                                    Stream.Read();
                                    Cur++;
                                }

                                RefreshState();
                                ColumnPos = 0;
                                continue;
                            }

                        case State.MultiComment:
                            {
                                Console.WriteLine();
                                break;
                            }

                        case State.FinishMultipleComment:
                            {
                                RefreshState();
                                continue;
                            }

                        default:
                            {
                                Temp += (char)current;
                                ColumnPos++;
                                Stream.Read();
                                Cur++;
                                break;
                            }
                    }
                }
                else
                {
                    if (State != State.Begin)
                    {
                        if (State == State.Identifier)
                        {
                            if (ReserveWords.Contains(Temp)) State = State.ReserveWord;
                        }

                        //------------------------CHECK MAX LENGTH----------------------------------
                        if (State == State.Int)
                        {
                            try
                            {
                                Temp = Temp.Replace("_", "");
                                int Int = Convert.ToInt32(Temp);
                            }
                            catch (Exception e)
                            {
                                return new Token()
                                {
                                    ColumnPos = ColumnPos - Temp.Length + 1,
                                    RowPos = RowPos,
                                    SourceValue = Temp,
                                    LiteralValue = SourceValueToLiteralValue(Temp, State),
                                    TypeLeksem = TypeLeksem.ErrorException
                                };
                            }
                        }

                        if (State == State.Double)
                        {
                            try
                            {
                                Temp = Temp.Replace("_", "");
                                var convert = Temp.Replace('.', ',');
                                double Double = Convert.ToDouble(convert);

                            }
                            catch (Exception e)
                            {
                                return new Token()
                                {
                                    ColumnPos = ColumnPos - Temp.Length + 1,
                                    RowPos = RowPos,
                                    SourceValue = Temp,
                                    LiteralValue = SourceValueToLiteralValue(Temp, State),
                                    TypeLeksem = TypeLeksem.ErrorException
                                };
                            }
                        }
                        //--------------------------------------------------------------

                        return new Token()
                        {
                            ColumnPos = ColumnPos - Temp.Length + 1,
                            RowPos = RowPos,
                            SourceValue = Temp,
                            LiteralValue = SourceValueToLiteralValue(Temp, State),
                            TypeLeksem = GetLeksemType(State)
                        };
                    }
                    State = State.EndOfFile;
                }
            }

            return null;

        }
        private void RefreshState()
        {
            State = State.Begin;
            Temp = "";
        }

        private string SourceValueToLiteralValue(string value, State state)
        {
            if (state == State.String || state ==State.SleepDog)
            {
                if (state == State.SleepDog)
                {
                    value = value.Remove(0, 1);
                }
                return value.Replace("\"", "");
            } 
            
            if (state == State.Char)
            {
                return value.Replace("\'", "");
            }

            return value;
        }

        private TypeLeksem GetLeksemType(State oldState)
        {
            switch (oldState)
            {
                case State.Identifier:
                    return TypeLeksem.Identifier;
                case State.ReserveWord:
                    return TypeLeksem.ReserveWord;
                case State.Word:
                    return TypeLeksem.ErrorException;
                case State.Int:
                    return TypeLeksem.Integer;
                case State.Double:
                    return TypeLeksem.Double;
                case State.Operator:
                    return TypeLeksem.Operator;
                case State.Delimiters:
                    return TypeLeksem.Delimiter;
                case State.Logical:
                    return TypeLeksem.Logical;
                case State.ErrorException:
                    return TypeLeksem.ErrorException;
                case State.Plus:
                    return TypeLeksem.Operator;
                case State.PlusPlus:
                    return TypeLeksem.Operator;
                case State.Minus:
                    return TypeLeksem.Operator;
                case State.MinusMinus:
                    return TypeLeksem.Operator;
                case State.Equal:
                    return TypeLeksem.Operator;
                case State.EqualEqual:
                    return TypeLeksem.Equals;
                case State.LogicInequality:
                    return TypeLeksem.Logical;
                case State.Inequality:
                    return TypeLeksem.Equals;
                case State.More:
                    return TypeLeksem.Equals;
                case State.MoreEqual:
                    return TypeLeksem.Equals;
                case State.Less:
                    return TypeLeksem.Equals;
                case State.LessEqual:
                    return TypeLeksem.Equals;
                case State.Ampersand:
                    return TypeLeksem.Operator;
                case State.DoubleAmpersand:
                    return TypeLeksem.Logical;
                case State.Or:
                    return TypeLeksem.Operator;
                case State.DoubleOr:
                    return TypeLeksem.Logical;
                case State.Devision:
                    return TypeLeksem.Operator;
                case State.Comment:
                    return TypeLeksem.ErrorException;
                case State.Acute:
                    return TypeLeksem.ErrorException;
                case State.HalfChar:
                    return TypeLeksem.CharException;
                case State.Char:
                    return TypeLeksem.Char;
                case State.String:
                    return TypeLeksem.String;
                case State.ShiftRight:
                    return TypeLeksem.Operator;
                case State.ShiftLeft:
                    return TypeLeksem.Operator;
                case State.IntPoint:
                    return TypeLeksem.NumberException;
                case State.IntDoubleOperator:
                    return TypeLeksem.ErrorException;
                case State.DevisionMultiplication:
                    return TypeLeksem.ErrorException;
                case State.MultiComment:
                    return TypeLeksem.ErrorException;
                case State.Dog:
                    return TypeLeksem.IdentifierException;
                case State.BackSlashException:
                    return TypeLeksem.BackSlashException;
                case State.InvalidCharacter:
                    return TypeLeksem.InvalidCharacterException;
                case State.ExponentaInt:
                    return TypeLeksem.NumberException;
                case State.DoubleD:
                    return TypeLeksem.Double;
                case State.Decimal:
                    return TypeLeksem.Decimal;
                case State.Float:
                    return TypeLeksem.Float;
                case State.Int_:
                    return TypeLeksem.NumberException;
                case State.ErrorInIdentifier:
                    return TypeLeksem.IdentifierException;
                case State.NumberException:
                    return TypeLeksem.NumberException;
                case State.Double_:
                    return TypeLeksem.NumberException;
                case State.ExponentaDouble:
                    return TypeLeksem.NumberException;
                case State.DoubleToDouble:
                    return TypeLeksem.Double;
                case State.DoubleToDecimal:
                    return TypeLeksem.Decimal;
                case State.DoubleToFloat:
                    return TypeLeksem.Float;
                case State.DoublePoint:
                    return TypeLeksem.NumberException;
                case State.ErrorInOperator:
                    return TypeLeksem.OperatorException;
                case State.ErrorInEterator:
                    return TypeLeksem.IteratorException;
                case State.ErrorInLogicalExpression:
                    return TypeLeksem.LogicalException;
                case State.FinishMultipleComment:
                    return TypeLeksem.ErrorException;
                case State.CharException:
                    return TypeLeksem.CharException;
                case State.CharWithBackSlash:
                    return TypeLeksem.CharException;
                case State.CharLineFowardHalf:
                    return TypeLeksem.CharException;
                case State.CharLineFoward:
                    return TypeLeksem.LineFowardSymbol;
                case State.ShiftException:
                    return TypeLeksem.OperatorException;
                case State.SpeekDog:
                    return TypeLeksem.ErrorException;
                case State.SleepDog:
                    return TypeLeksem.String;
                case State.ExponentaIntNumberToDouble:
                    return TypeLeksem.Double;
                case State.ExponentaIntPlus:
                    return TypeLeksem.NumberException;
                case State.ExponentaIntMinus:
                    return TypeLeksem.NumberException;
                default:
                    return TypeLeksem.ErrorException;
            }
        }

        private State SetNewState(State state, int asc)
        {
            if ((char)asc == 'e' || (char)asc == 'E')
            {
                Dictionary.TryGetValue(('e', state), out state);
            }
            else if ((char)asc == 'n')
            {
                Dictionary.TryGetValue(('n', state), out state);
            }
            else if ((char)asc == 'd' || (char)asc == 'D')
            {
                Dictionary.TryGetValue(('d', state), out state);
            }
            else if ((char)asc == 'm' || (char)asc == 'M')
            {
                Dictionary.TryGetValue(('m', state), out state);
            }
            else if ((char)asc == 'f' || (char)asc == 'F')
            {
                Dictionary.TryGetValue(('f', state), out state);
            }
            else if ((char)asc == '_')
            {
                Dictionary.TryGetValue(('_', state), out state);
            }
            else if (asc >= 65 && asc <= 90 || asc >= 97 && asc <= 122 || asc == 95 || (char)asc=='?')
            {
                Dictionary.TryGetValue(('s', state), out state);
            }
            else if (asc >= 48 && asc <= 57)
            {
                Dictionary.TryGetValue(('0', state), out state);
            }
            else
                Dictionary.TryGetValue(((char)asc, state), out state);

            return state;
        }
    }
}