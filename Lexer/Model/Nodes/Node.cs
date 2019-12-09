using System;

namespace Lexer.Model.Nodes
{
    class Node
    {
        public int spaceCount = 3;
        public virtual void Print(int space)
        {

        }
    }

    class BinaryNode : Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Token Operation { get; set; }

        public override void Print(int space)
        {
            Left.Print(space+ spaceCount);

            for (int i = 0; i < space; i++)
            {
                Console.Write(" ");
            }

            Console.Write(Operation.LiteralValue);
            Console.WriteLine();

            Right.Print(space+ spaceCount);
        }
    }

    class IteratorNode : Node
    {
        public Token Iterator { get; set; }

        public override void Print(int space)
        {
            Console.Write(Iterator.LiteralValue);
        }
    }

    class LiteralNode : Node
    {
        public Token Constant { get; set; }
        public Node Right { get; set; }

        public override void Print(int space)
        {
            for (int i = 0; i < space; i++)
            {
                Console.Write(" ");
            }

            Console.Write(Constant.LiteralValue);
            Right?.Print(0);

            Console.WriteLine();
        }
    }
}
