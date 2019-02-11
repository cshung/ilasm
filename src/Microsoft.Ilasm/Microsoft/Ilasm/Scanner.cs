//-----------------------------------------------------------------------
// <copyright file="Scanner.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>This is the Widget class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Ilasm
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Scanner.
    /// </summary>
    internal class Scanner
    {
        /// <summary>
        /// The text to be scanned.
        /// </summary>
        private string text;

        /// <summary>
        /// The position.
        /// </summary>
        private int position;

        /// <summary>
        /// The token.
        /// </summary>
        private Token token;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scanner"/> class.
        /// </summary>
        /// <param name="text">The text to be scanned.</param>
        /// <exception cref="ArgumentNullException">If the argument is null.</exception>
        internal Scanner(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            this.text = text;
            this.position = 0;
            this.token = null;
            this.Scan();
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        internal Token Token
        {
            get
            {
                return this.token;
            }
        }

        /// <summary>
        /// Scans this instance.
        /// </summary>
        internal void Scan()
        {
            while (this.position < this.text.Length && char.IsWhiteSpace(this.text[this.position]))
            {
                this.position++;
            }

            if (this.position == this.text.Length)
            {
                this.token = new Token(this.text, TokenType.Eof, this.position, this.position);
            }

            // It is important that these simple tokens are not prefix of each other
            var simpleTokens = new KeyValuePair<TokenType, string>[]
            {
                new KeyValuePair<TokenType, string>(TokenType.Assembly, ".assembly"),
                new KeyValuePair<TokenType, string>(TokenType.Extern, "extern"),
                new KeyValuePair<TokenType, string>(TokenType.Lbrace, "{"),
                new KeyValuePair<TokenType, string>(TokenType.Rbrace, "}"),
                new KeyValuePair<TokenType, string>(TokenType.Module, ".module"),
            };
            foreach (var pair in simpleTokens)
            {
                string keyword = pair.Value;
                if ((this.position + keyword.Length) <= this.text.Length)
                {
                    if (this.text.Substring(this.position, keyword.Length).Equals(keyword))
                    {
                        this.token = new Token(this.text, pair.Key, this.position, this.position + keyword.Length);
                        this.position += keyword.Length;
                        return;
                    }
                }
            }
            if (this.text[this.position] == '.')
            {
                this.token = new Token(this.text, TokenType.Dot, this.position, this.position + 1);
                this.position += 1;
                return;
            }

            if (this.IsIdBeginCharacter(this.text[this.position]))
            {
                int beginPosition = this.position;
                do
                {
                    this.position++;
                }
                while (this.IsIdCharacter(this.text[this.position]));
                this.token = new Token(this.text, TokenType.Id, beginPosition, this.position);
            }
        }

        /// <summary>Determines whether [is identifier character] [the specified c].</summary>
        /// <param name="c">The character to be tested.</param>
        /// <returns>
        ///   <c>true</c> if [is identifier character] [the specified c]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsIdCharacter(char c)
        {
            return ('0' <= c && c <= '9') || this.IsIdBeginCharacter(c);
        }

        /// <summary>Determines whether [is identifier begin character] [the specified c].</summary>
        /// <param name="c">The character to be tested.</param>
        /// <returns>
        ///   <c>true</c> if [is identifier begin character] [the specified c]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsIdBeginCharacter(char c)
        {
            return ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z') || c == '_' || c == '$' || c == '@' || c == '`' || c == '?';
        }
    }
}
