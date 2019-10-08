namespace Compiler.Tokens
{
    interface ITokenizer
    {
        void GetLine(string line);
        
        void StartAnalyzer();
    }
}
