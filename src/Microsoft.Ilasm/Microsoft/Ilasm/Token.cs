//-----------------------------------------------------------------------
// <copyright file="Token.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>This is the Widget class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Ilasm
{
    /// <summary>
    /// The Token.
    /// </summary>
    internal class Token
    {
        /// <summary>
        /// The token type.
        /// </summary>
        private readonly TokenType tokenType;

        /// <summary>
        /// The source text.
        /// </summary>
        private readonly string text;

        /// <summary>
        /// The begin of the span.
        /// </summary>
        private readonly int begin;

        /// <summary>
        /// The end of the span.
        /// </summary>
        private readonly int end;

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="text">The source text.</param>
        /// <param name="tokenType">Type of the token.</param>
        /// <param name="begin">The begin of the token.</param>
        /// <param name="end">The end of the token.</param>
        public Token(string text, TokenType tokenType, int begin, int end)
        {
            this.text = text;
            this.tokenType = tokenType;
            this.begin = begin;
            this.end = end;
        }

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        /// <value>
        /// The type of the token.
        /// </value>
        public TokenType TokenType
        {
            get
            {
                return this.tokenType;
            }
        }

        /// <summary>
        /// Gets the token text.
        /// </summary>
        /// <value>
        /// The token text.
        /// </value>
        public string TokenText
        {
            get
            {
                return this.text.Substring(this.begin, this.end - this.begin);
            }
        }
    }
}
