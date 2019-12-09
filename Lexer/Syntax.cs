using Lexer.Model;
using Lexer.Model.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using Lexer.Service;

namespace Lexer
{
    class Syntax
    {
        private string path;
        private List<Token> tokensList = new List<Token>();
        private Token token = new Token();
        private List<Token> term;
        private Node tree;
        private Tokenyzer tokenyzer;
        private Token buff;

        public Syntax(string path)
        {
            this.path = path;
        }

        public void SyntaxAnalyzer()
        {
            using StreamReader stream = File.OpenText(path);
            tokenyzer = new Tokenyzer(stream, new StateTable());

            tree = ParseExpression();
            new TreePrinter(tree);

            Console.WriteLine();
        }

        private Node ParseExpression()
        {
            Node left = ParseTerm();

            var token = Next();

            if (token?.LiteralValue == "+" || token?.LiteralValue == "-")
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

        private Node ParseTerm()
        {
            Node left = ParseFactor();

            var token = Next();

            if (token?.LiteralValue == "*" || token?.LiteralValue == "/")
            {
                return new BinaryNode()
                {
                    Left = left,
                    Operation = token,
                    Right = ParseTerm()
                };
            }

            SetBuff(token);
            return left;
        }

        private Node ParseFactor()
        {
            var token = Next();

            if (token?.TypeLeksem == TypeLeksem.Integer || token?.TypeLeksem == TypeLeksem.Double)
            {
                return new LiteralNode()
                {
                    Constant = token,
                    Right = ParseIterator()
                };
            }

            throw new NotImplementedException();
        }

        private Node ParseIterator()
        {
            var token = Next();

            if (token?.LiteralValue == "++" || token?.LiteralValue == "--")
            {
                return new IteratorNode()
                {
                    Iterator = token,
                };
            }
            else
            {
                SetBuff(token);
                return null;
            }
        }

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
