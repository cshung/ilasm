namespace Microsoft.Ilasm
{
    using System;
    using System.Collections.Generic;

    // It is intended to be an implementation of ECMA-335 (page 108+ is the specification of the ilasm grammar)
    // The implementation sequence depends on the sample program I am trying to assemble
    internal class Scanner
    {
        private string m_text;
        private int m_position;
        private Token m_token;

        internal Scanner(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            this.m_text = text;
            this.m_position = 0;
            this.m_token = null;
            this.Scan();
        }

        internal void Scan()
        {
            while (this.m_position < this.m_text.Length && char.IsWhiteSpace(this.m_text[this.m_position]))
            {
                this.m_position++;
            }
            if (this.m_position == this.m_text.Length)
            {
                this.m_token = new Token(this.m_text, TokenType.Eof, this.m_position, this.m_position);
            }
            var keywords = new KeyValuePair<TokenType, string>[]
            {
                new KeyValuePair<TokenType, string>(TokenType.Assembly, ".assembly"),
                new KeyValuePair<TokenType, string>(TokenType.Extern, "extern")
            };
            foreach (var pair in keywords)
            {
                string keyword = pair.Value;
                if ((this.m_position + keyword.Length) <= this.m_text.Length)
                {
                    if (this.m_text.Substring(this.m_position, keyword.Length).Equals(keyword))
                    {
                        this.m_token = new Token(this.m_text, pair.Key, this.m_position, this.m_position + keyword.Length);
                        this.m_position += keyword.Length;
                        return;
                    }
                }
            }
            if (IsIdBeginCharacter(this.m_text[this.m_position]))
            {
                int beginPosition = this.m_position;
                do
                {
                    this.m_position++;
                } while (IsIdCharacter(this.m_text[this.m_position]));
                this.m_token = new Token(this.m_text, TokenType.Id, beginPosition, this.m_position);
            }
        }

        private bool IsIdCharacter(char v)
        {
            return ('0' <= v && v <= '9') || IsIdBeginCharacter(v);
        }

        private bool IsIdBeginCharacter(char v)
        {
            return ('A' <= v && v <= 'Z') || ('a' <= v && v <= 'z') || v == '_' || v == '$' || v == '@' || v == '`' || v == '?';
        }

        internal Token Token
        {
            get
            {
                return this.m_token;
            }
        }
    }
}
