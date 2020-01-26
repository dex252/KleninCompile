using Lexer.Model.Nodes;

namespace Lexer.Service
{
    class TreePrinter
    {
        public TreePrinter(Node tree)
        {
            tree.Print(1);
        }
    }
}
