namespace Lexer.Model.Nodes
{
    class Node
    {
       
    }

    class BinaryNode : Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Token Operation { get; set; }
    }

    class LiteralNode : Node
    {
        public Token Constant { get; set; }
    }
}
