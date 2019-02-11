namespace Microsoft.Ilasm
{
    internal class Token
    {
        private readonly TokenType m_tokenType;
        private readonly string m_text;
        private readonly int m_begin;
        private readonly int m_end;

        public TokenType TokenType
        {
            get
            {
                return this.m_tokenType;
            }
        }

        public Token(string text, TokenType tokenType, int begin, int end)
        {
            this.m_text = text;
            this.m_tokenType = tokenType;
            this.m_begin = begin;
            this.m_end = end;
        }

        public string TokenText
        {
            get
            {
                return this.m_text.Substring(this.m_begin, this.m_end - this.m_begin);
            }
        }
    }
}
