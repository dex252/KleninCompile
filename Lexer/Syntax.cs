using Lexer.Model;
using Lexer.Model.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using Lexer.Service;

namespace Lexer
{
    enum SymbolType
    {
        Null = 0,
        StartBlock,
        EndBlock,
    }
    class Syntax
    {
        private string path;
        private List<Token> tokensList = new List<Token>();
        private Token token = new Token();
        private List<Token> term;
        private Node tree;
        private Tokenyzer tokenyzer;
        private Token buff;
        private int NumBlocks = 0;

        public Syntax(string path)
        {
            this.path = path;

        }

        public void SyntaxAnalyzer()
        {
            using StreamReader stream = File.OpenText(path);
            tokenyzer = new Tokenyzer(stream, new StateTable());

            tree = ParseBraces();
            new TreePrinter(tree);

            Console.WriteLine();
        }

        private Node ParseBraces()
        {
            Node left = ParseLine();

            var token = Next();

            if (token?.LiteralValue == "{")
            {
                NumBlocks+=2;
                return new BlockNode()
                {
                    Left = new SymbolsNode(NumBlocks)
                    {
                        Constant = token
                    },
                    Block = left,
                    Right = ParseBraces()
                };
            }

            SetBuff(token);
            return left;
        }

        private Node ParseLine()
        {
            Node left = ParseExpression();

            var token = Next();

            if (token?.LiteralValue == ";")
            {
                return new EndLineNode()
                {
                    Left = left,
                    Operation = token,
                    Right = ParseLine()
                };
            }

            SetBuff(token);
            return left;

        }

        private Node ParseExpression()
        {
            Node left = ParseLowOperation();

            var token = Next();

            if (token?.LiteralValue == "=")
            {
                return new BinaryNode()
                {
                    Left = left,
                    Operation = token,
                    Right = ParseExpression()
                };
            }

            SetBuff(token);
            return left;
        }

        private Node ParseLowOperation()
        {
            Node left = ParseHightOperation();

            var token = Next();

            if (token?.LiteralValue == "+" || token?.LiteralValue == "-")
            {
                return new BinaryNode()
                {
                    Left = left,
                    Operation = token,
                    Right = ParseLowOperation()
                };
            }

            SetBuff(token);
            return left;
        }

        private Node ParseHightOperation()
        {
            Node left = ParseLyteral();

            var token = Next();

            if (token?.LiteralValue == "*" || token?.LiteralValue == "/")
            {
                return new BinaryNode()
                {
                    Left = left,
                    Operation = token,
                    Right = ParseHightOperation()
                };
            }

            SetBuff(token);
            return left;
        }


        private Node ParseLyteral()
        {
            var token = Next();

            if (token?.TypeLeksem == TypeLeksem.Integer || token?.TypeLeksem == TypeLeksem.Double)
            {
                return new NumberNode()
                {
                    Constant = token
                };
            }

            if (token?.TypeLeksem == TypeLeksem.Char || token?.TypeLeksem == TypeLeksem.String)
            {
                return new SymbolsNode()
                {
                    Constant = token
                };
            }

            if (token?.LiteralValue == "true" || token?.LiteralValue == "false")
            {
                return new BoolNode()
                {
                    Constant = token
                };
            }

            if (token?.LiteralValue == "-")
            {
                return new NumberNode()
                {
                    Constant = token,
                    Right = ParseNumber()
                };
            }

            if (token?.LiteralValue == "}")
            {
                NumBlocks-=2;
                return new BlockNode()
                {
                   Left = new SymbolsNode(NumBlocks+2)
                   {
                       Constant = token,
                   },
                   Block = ParseBraces(),

                };
            }

            if (token?.LiteralValue == "{")
            {
                NumBlocks+=2;
                return new BlockNode()
                {
                    Left = new SymbolsNode(NumBlocks)
                    {
                        Constant = token
                    },
                    Block = ParseBraces(),
                    Right = ParseBraces()
                };
            }

            if (token?.TypeLeksem == TypeLeksem.Identifier)
            {
                return new IdentifyNode()
                {
                    Constant = token,
                    Right = ParseStatement()
                };
            }

            SetBuff(token);
            return null;
        }

        private Node ParseNumber()
        {
            var token = Next();

            if (token?.TypeLeksem == TypeLeksem.Integer || token?.TypeLeksem == TypeLeksem.Double)
            {
                return new NumberNode()
                {
                    Constant = token
                };
            }

            SetBuff(token);
            return null;
        }

        //TO DO: только для переменных
        private Node ParseStatement()
        {
            var token = Next();

            if (token?.LiteralValue == "++" || token?.LiteralValue == "--")
            {
                return new IteratorNode()
                {
                    Iterator = token
                };
            }

            if (token?.LiteralValue == "{")
            {
                NumBlocks += 2;
                return new BlockNode()
                {
                    Left = new SymbolsNode(NumBlocks)
                    {
                        Constant = token
                    },
                    Block = ParseBraces(),
                    Right = ParseBraces()
                };
            }

            SetBuff(token);
            return null;

        }

        /// <summary>
        /// Получить следующий токен, либо токен из буфера, если таковой содержится в нем
        /// </summary>
        /// <returns></returns>
        private Token Next()
        {
            var token = GetBuff();

            if (token == null)
            {
                token = tokenyzer.GetToken();
                if (!CheckErrorExceptionTypeToken(token))
                {
                    Console.WriteLine("Error");
                    throw new NotImplementedException();
                }

            }
            SetBuff(null);

            return token;
        }

        private bool CheckErrorExceptionTypeToken(Token token)
        {
            return token?.TypeLeksem != TypeLeksem.ErrorException;
        }

        /// <summary>
        /// Записать токен в буфер
        /// </summary>
        /// <param name="token">Записываемый токен</param>
        private void SetBuff(Token token)
        {
            buff = token;
        }

        private Token GetBuff()
        {
            return buff;
        }

    }
}