namespace Compiler.Tokens.Types
{
    public abstract class Token<T>
    {
        public T value;
        public string type;

        protected Token()
        {
            type = typeof(T).Name;
        }

        public virtual bool CheckToken(string item)
        {
            return false;
        }
    }
}
