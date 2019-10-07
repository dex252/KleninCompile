namespace Compiler.Tokens.Types
{
    public abstract class Token
    {
        public object typeToken;

        public virtual bool CheckToken(string item)
        {
            return false;
        }
    }
}
