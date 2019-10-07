using System;

namespace Compiler.Tokens.Types
{
    class Int: Token
    {
        public override bool CheckToken(string item)
        {
            try
            {
                int token = Convert.ToInt32(item);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
