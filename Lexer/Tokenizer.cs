﻿using System.Collections.Generic;
using System.IO;
using Lexer.Model;

namespace Lexer
{
    class Tokenizer
    {
        private StreamReader Stream { get; }
        public Dictionary<(char, State), State> Dictionary { get; set; }
        public Dictionary<string, bool> ReserveWords { get; set; }
        private int ColumnPos { get; set; }
        private int RowPos { get; set; }
        private string Temp { get; set; }
        private State State { get; set; }

        public Tokenizer(StreamReader stream, StateTable stateTable)
        {
            Stream = stream;
            RowPos = 1;
            ColumnPos = 0;
            Dictionary = stateTable.Dictionary;
            ReserveWords = stateTable.ReserveWords;
        }

        public Token GetToken()
        {
            State = State.Begin;
            Temp = "";
            while (State != State.EndOfFile)
            {
                if (Stream.Peek() != -1)
                {
                    var current = Stream.Peek();

                    var oldState = State;
                    State = SetNewState(State, current);

                    //экранируем от пробелов, переходов строк, переноса корретки и символов, не входящих в алфавит
                    if (oldState == State.Begin && State == State.Indefinitely)
                    {
                        Stream.Read();
                        switch (current)
                        {
                            case 32:
                            {
                                RefreshState();
                                ColumnPos++;
                                continue;
                            }

                            case 10:
                            {
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
                                if (ReserveWords.ContainsKey(Temp)) oldState = State.ReserveWord;
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
                            }

                            RefreshState();
                            ColumnPos = 0;
                            continue;
                        }

                        default:
                        {
                            Temp += (char) current;
                            ColumnPos++;
                            Stream.Read();
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
                            if (ReserveWords.ContainsKey(Temp)) State = State.ReserveWord;
                        }

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
            if (state == State.String)
            {
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
            //TO DO
            switch (oldState)
            {
                case State.Identifier:
                    return TypeLeksem.Identifier;
                case State.Word:
                    return TypeLeksem.ErrorException;
                case State.Int:
                    return TypeLeksem.Integer;
                case State.Double:
                    return TypeLeksem.Double;
                case State.ReserveWord:
                    return TypeLeksem.ReserveWord;
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
                    return TypeLeksem.Operator;
                case State.LogicInequality:
                    return TypeLeksem.Logical;
                case State.Inequality:
                    return TypeLeksem.Operator;
                case State.More:
                    return TypeLeksem.Operator;
                case State.MoreEqual:
                    return TypeLeksem.Operator;
                case State.Less:
                    return TypeLeksem.Operator;
                case State.LessEqual:
                    return TypeLeksem.Operator;
                case State.Ampersand:
                    return TypeLeksem.ErrorException;
                case State.DoubleAmpersand:
                    return TypeLeksem.Logical;
                case State.Or:
                    return TypeLeksem.ErrorException;
                case State.DoubleOr:
                    return TypeLeksem.Logical;
                case State.Devision:
                    return TypeLeksem.Operator;
                case State.Comment:
                    return TypeLeksem.ErrorException;
                case State.Acute:
                    return TypeLeksem.ErrorException;
                case State.HalfChar:
                    return TypeLeksem.ErrorException;
                case State.Char:
                    return TypeLeksem.Char;
                case State.String:
                    return TypeLeksem.String;
                default: return TypeLeksem.ErrorException;
            }
        }

        private State SetNewState(State state, int asc)
        {
            if (asc >= 65 && asc <= 90 || asc >= 97 && asc <= 122 || asc == 95)
            {
                Dictionary.TryGetValue(('s', state), out state);
            }
            else if (asc >= 48 && asc <= 57)
            {
                Dictionary.TryGetValue(('0', state), out state);
            }
            else
                Dictionary.TryGetValue(((char) asc, state), out state);

            return state;
        }
    }
}