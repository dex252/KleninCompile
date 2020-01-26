using System;

namespace Lexer.Model.Nodes
{
    enum NodeType
    {
        Node = 0,
        BinaryNode,
        BlockNode,
        IteratorNode,
        NumberNode,
        SymbolsNode,
        BoolNode,
        EndLineNode,
        IdentifyNode
    }
    class Node
    {
        public int SpaceCount { get; }
        public Node(int SpaceCount = 1)
        {
            this.SpaceCount = SpaceCount;
        }

        public virtual void Print(int space)
        {

        }

        public virtual NodeType GetTypeNode()
        {
            return NodeType.Node;
        }
    }

  

    class BinaryNode : Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Token Operation { get; set; }

        public override void Print(int space)
        {
            Left?.Print(space + SpaceCount);

            for (int i = 0; i < space; i++)
            {
                Console.Write(" ");
            }

            Console.Write(Operation?.LiteralValue);
            Console.WriteLine();

            Right?.Print(space+ SpaceCount);
        }

    }
    class BlockNode : Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Block { get; set; }

        public override NodeType GetTypeNode()
        {
            return NodeType.BlockNode;
        }

        public override void Print(int space)
        {
            for (int i = 0; i < SpaceCount; i++)
            {
                Console.Write(" ");
            }
            Left?.Print(0);
            Console.WriteLine();

            Block?.Print(5 + Left.SpaceCount);
            Right?.Print(0);
          
        }
    }

    class IteratorNode : Node
    {
        public Token Iterator { get; set; }
        public Node Right { get; set; }

        public override void Print(int space)
        {
            Console.Write(Iterator.LiteralValue);
            Right?.Print(0);
        }

        public override NodeType GetTypeNode()
        {
            return NodeType.IteratorNode;
        }
    }

    class NumberNode : Node
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

            if (Right?.GetTypeNode() == NodeType.BlockNode)
            {
                Console.WriteLine();
            }
         
            Right?.Print(0);

            Console.WriteLine();
        }
    }

    class SymbolsNode : Node
    {
        public Token Constant { get; set; }

        public SymbolsNode(int space = 0) : base(space)
        {

        }

        public override void Print(int space)
        {
            for (int i = 0; i < SpaceCount; i++)
            {
                Console.Write(" ");
            }

            Console.Write(Constant.LiteralValue);

            Console.WriteLine();
        }
    }

    class BoolNode : Node
    {
        public Token Constant { get; set; }

        public override void Print(int space)
        {
            for (int i = 0; i < space; i++)
            {
                Console.Write(" ");
            }

            Console.Write(Constant.LiteralValue);

            Console.WriteLine();
        }
    }

    class EndLineNode: BinaryNode
    {
    }

    class IdentifyNode: NumberNode
    {

    }

}
