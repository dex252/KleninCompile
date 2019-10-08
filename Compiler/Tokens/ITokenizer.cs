namespace Compiler.Tokens
{
    interface ITokenizer
    {
        void GetLine(string line, int count);
        
        void StartAnalyzer();
    }
}
