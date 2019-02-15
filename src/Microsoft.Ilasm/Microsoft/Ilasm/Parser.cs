//-----------------------------------------------------------------------
// <copyright file="Parser.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Ilasm
{
    using System;
    using System.Diagnostics.CodeAnalysis;

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
            this.scanner.Scan(isWhitespaceAccepted: true);
        }

        /// <summary>
        /// Parses the non-terminal 'Decl'.
        /// </summary>
        internal void ParseDecl()
        {
            if (this.scanner.Token.TokenType == TokenType.Assembly)
            {
                this.scanner.Scan(isWhitespaceAccepted: true);
                if (this.scanner.Token.TokenType == TokenType.Id)
                {
                    this.ParseDottedName();
                    if (this.scanner.Token.TokenType == TokenType.Lbrace)
                    {
                        this.scanner.Scan(isWhitespaceAccepted: true);
                        this.ParseAsmDecl();
                        if (this.scanner.Token.TokenType == TokenType.Rbrace)
                        {
                            this.scanner.Scan(isWhitespaceAccepted: true);
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
                    this.scanner.Scan(isWhitespaceAccepted: true);
                    if (this.scanner.Token.TokenType == TokenType.Id)
                    {
                        this.ParseDottedName();
                        if (this.scanner.Token.TokenType == TokenType.Lbrace)
                        {
                            this.scanner.Scan(isWhitespaceAccepted: true);
                            this.ParseAsmRefDecl();
                            if (this.scanner.Token.TokenType == TokenType.Rbrace)
                            {
                                this.scanner.Scan(isWhitespaceAccepted: true);
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

        /// <summary>
        /// Parses the non-terminal 'AsmRefDecl'.
        /// </summary>
        internal void ParseAsmRefDecl()
        {
            // TODO
        }

        /// <summary>
        /// Parses the non-terminal 'AsmDecl'.
        /// </summary>
        internal void ParseAsmDecl()
        {
            // TODO
        }

        /// <summary>
        /// Parses the non-terminal 'DottedName'.
        /// </summary>
        internal void ParseDottedName()
        {
            if (this.scanner.Token.TokenType == TokenType.Id)
            {
                this.scanner.Scan(isWhitespaceAccepted: true);
                if (this.scanner.Token.TokenType == TokenType.Dot)
                {
                    this.scanner.Scan(isWhitespaceAccepted: false);
                    this.ParseDottedName();
                }
            }
            else
            {
                // TODO, error reporting/recovery?
                throw new Exception("6");
            }
        }
    }
}