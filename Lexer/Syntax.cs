using Lexer.Model;
using Lexer.Model.Nodes;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lexer
{
    class Syntax
    {
        private string path;
        private List<Token> tokensList = new List<Token>();
        private Token token = new Token();
        private List<Token> term;

        public Syntax(string path)
        {
            this.path = path;
        }

        public void SyntaxAnalyzer()
        {
            using StreamReader stream = File.OpenText(path);
            Tokenyzer tokenizer = new Tokenyzer(stream, new StateTable());

            ParseExpression(tokenizer);
        }

        private void ParseExpression(Tokenyzer tokenizer)
        {
            Node left = new Node();

            while (left != null)
            {
                left = ParseTerm(tokenizer);
                //либо здесь вытащить value  и снова применить ParseTerm при необходимости, записав новую Node, куда я её запишу?
                //если left = 2, то по логике следуем к оператору + (пример), после того как вытащим +, заменим в этой left Node левую часть на неё саму, а Value на +
                //этого мало, идем дальше: читаем 5 (прим), далее, получив выражение, запишем 5 в правую часть ноды, выйдет готовое выражение
                //если ест ьвложенные выражения? то они полетят в правую часть и рекусривным смуском действия повторятся
            }
        }

        private Node ParseTerm(Tokenyzer tokenizer)
        {
            
                token = tokenizer.GetToken();
                if (token != null)
                {
                    Console.WriteLine($"{token.RowPos}  {token.ColumnPos}   {token.TypeLeksem}  {token.LiteralValue}");
                    tokensList.Add(token); //для отладки
                    
                    if (token?.TypeLeksem == TypeLeksem.ErrorException) return null;

                    //где то здесь нужно вставить проверку на ноды
                    //если встретили ( то => читаем до ) и создаем ноду с left = ( и right = ), value = между ними выражение
                    //также выражение 5 + 2 => 5 - левая нода, 2 - правая нода + - value и т.д.

                    return new Node() 
                    { 
                        Value = token//заглушка!!!!!!!!!!
                    };
                }
                else
                {
                    return null;
                }

        }
    }
}
