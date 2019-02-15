//-----------------------------------------------------------------------
// <copyright file="Scanner.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
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
        /// The index (to the string) to start scanning for the next token.
        /// </summary>
        private int position;

        /// <summary>
        /// The token.
        /// </summary>
        private Token token;

        /// <summary>
        /// The line number of the next token.
        /// </summary>
        private int line;

        /// <summary>
        /// The column number of the next token.
        /// </summary>
        private int column;

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
            this.line = 1;
            this.column = 1;
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
        /// Gets the line number of the next token.
        /// </summary>
        /// <value>
        /// The line number of the next token.
        /// </value>
        internal int Line
        {
            get
            {
                return this.line;
            }
        }

        /// <summary>
        /// Gets the column number of the next token.
        /// </summary>
        /// <value>
        /// The column number of the next token.
        /// </value>
        internal int Column
        {
            get
            {
                return this.column;
            }
        }


        /// <summary>
        /// Scans this instance.
        /// </summary>
        internal void Scan()
        {
            while (this.position < this.text.Length && char.IsWhiteSpace(this.text[this.position]))
            {
                if (this.text[this.position] == '\n')
                {
                    this.line++;
                    this.column = 1;
                }
                else
                {
                    this.column++;
                }
                this.position++;
            }

            if (this.position == this.text.Length)
            {
                this.token = new Token(this.text, TokenType.Eof, this.position, this.position);
                return;
            }

            // It is important that these simple tokens are not prefix of each other
            var simpleTokens = new KeyValuePair<TokenType, string>[]
            {
                new KeyValuePair<TokenType, string>(TokenType.Assembly, ".assembly"),
                new KeyValuePair<TokenType, string>(TokenType.Extern, "extern"),
                new KeyValuePair<TokenType, string>(TokenType.Lbrace, "{"),
                new KeyValuePair<TokenType, string>(TokenType.Rbrace, "}"),
                new KeyValuePair<TokenType, string>(TokenType.Module, ".module"),
                new KeyValuePair<TokenType, string>(TokenType.NameSpace, ".namespace"),
                new KeyValuePair<TokenType, string>(TokenType.Class, ".class"),
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
                        this.column += keyword.Length;
                        return;
                    }
                }
            }

            if (this.text[this.position] == '.')
            {
                this.token = new Token(this.text, TokenType.Dot, this.position, this.position + 1);
                this.position += 1;
                this.column += 1;
                return;
            }

            if (this.IsIdBeginCharacter(this.text[this.position]))
            {
                int beginPosition = this.position;
                do
                {
                    this.position++;
                    this.column++;
                }
                while (this.position < this.text.Length && this.IsIdCharacter(this.text[this.position]));
                this.token = new Token(this.text, TokenType.Id, beginPosition, this.position);
                return;
            }
        }

        /// <summary>Determines whether [is identifier character] [the specified c].</summary>
        /// <param name="c">The character to be tested.</param>
        /// <returns>
        ///   <c>true</c> if [is identifier character] [the specified c]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsIdCharacter(char c)
        {
            return (c >= '0' && c <= '9') || this.IsIdBeginCharacter(c);
        }

        /// <summary>Determines whether [is identifier begin character] [the specified c].</summary>
        /// <param name="c">The character to be tested.</param>
        /// <returns>
        ///   <c>true</c> if [is identifier begin character] [the specified c]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsIdBeginCharacter(char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_' || c == '$' || c == '@' || c == '`' || c == '?';
        }
    }
}
