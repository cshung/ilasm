//-----------------------------------------------------------------------
// <copyright file="Parser.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>This is the Parser class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Ilasm
{
    using System;

    /// <summary>
    /// The parser.
    /// </summary>
    internal class Parser
    {
        /// <summary>
        /// The scanner.
        /// </summary>
        private Scanner scanner;

        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class.
        /// </summary>
        /// <param name="scanner">The scanner.</param>
        /// <exception cref="ArgumentNullException">If the argument is null.</exception>
        internal Parser(Scanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }

            this.scanner = scanner;
        }

        /// <summary>
        /// Parses the decl.
        /// </summary>
        internal void ParseDecl()
        {
            if (this.scanner.Token.TokenType == TokenType.Assembly)
            {
                this.scanner.Scan();
                if (this.scanner.Token.TokenType == TokenType.Id)
                {
                    this.ParseDottedName();
                    if (this.scanner.Token.TokenType == TokenType.Lbrace)
                    {
                        this.scanner.Scan();
                        this.ParseAsmDecl();
                        if (this.scanner.Token.TokenType == TokenType.Rbrace)
                        {
                            this.scanner.Scan();
                            // Done - but how do I make this useful?
                        }
                        else
                        {
                            // TODO, error reporting/recovery?
                            throw new Exception("1");
                        }
                    }
                    else
                    {
                        // TODO, error reporting/recovery?
                        throw new Exception("2");
                    }
                }
                else if (this.scanner.Token.TokenType == TokenType.Extern)
                {
                    this.scanner.Scan();
                    if (this.scanner.Token.TokenType == TokenType.Id)
                    {
                        this.ParseDottedName();
                        if (this.scanner.Token.TokenType == TokenType.Lbrace)
                        {
                            this.scanner.Scan();
                            this.ParseAsmRefDecl();
                            if (this.scanner.Token.TokenType == TokenType.Rbrace)
                            {
                                this.scanner.Scan();
                                // Done - but how do I make this useful?
                            }
                            else
                            {
                                // TODO, error reporting/recovery?
                                throw new Exception("3");
                            }
                        }
                        else
                        {
                            // TODO, error reporting/recovery?
                            throw new Exception("4");
                        }
                    }
                }
                else
                {
                    // TODO, error reporting/recovery?
                    throw new Exception("5");
                }
            }
            else
            {
                throw new Exception("6");
            }
        }

        internal void ParseAsmRefDecl()
        {
            // TODO
        }

        internal void ParseAsmDecl()
        {
            // TODO
        }

        internal void ParseDottedName()
        {
            if (this.scanner.Token.TokenType == TokenType.Id)
            {
                this.scanner.Scan();
                if (this.scanner.Token.TokenType == TokenType.Dot)
                {
                    this.scanner.Scan();
                    this.ParseDottedName();
                }
            }
        }
    }
}