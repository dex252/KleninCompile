namespace Lexer.Model.Nodes
{
    class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Token Value { get; set; }

        //public Node() {}
        //public Node(Token token)
        //{
        //    Value = token;
        //}
    }

}
