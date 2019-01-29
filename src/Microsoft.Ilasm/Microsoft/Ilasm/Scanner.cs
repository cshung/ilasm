namespace Microsoft.Ilasm
{
    using System;

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
            string dotAssembly = ".assembly";
            if ((this.m_position + dotAssembly.Length) <= this.m_text.Length)
            {
                if (this.m_text.Substring(this.m_position, dotAssembly.Length).Equals(dotAssembly))
                {
                    this.m_position += dotAssembly.Length;
                    this.m_token = Token.Assembly;
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
