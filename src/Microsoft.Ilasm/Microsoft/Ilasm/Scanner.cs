namespace Microsoft.Ilasm
{
    using System;
    using System.Collections.Generic;

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
            this.m_token = Token.Eof;
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
                this.m_token = Token.Eof;
            }
            var keywords = new KeyValuePair<Token, string>[]
            {
                new KeyValuePair<Token, string>(Token.Assembly, ".assembly"),
                new KeyValuePair<Token, string>(Token.Extern, "extern")
            };
            foreach (var pair in keywords)
            {
                string keyword = pair.Value;
                if ((this.m_position + keyword.Length) <= this.m_text.Length)
                {
                    if (this.m_text.Substring(this.m_position, keyword.Length).Equals(keyword))
                    {
                        this.m_position += keyword.Length;
                        this.m_token = pair.Key;
                    }
                }
            }
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
