//-----------------------------------------------------------------------
// <copyright file="Parser.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Ilasm
{
    using System;
    using System.Collections.Generic;
    using System.Text;

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
        /// The list of errors collected during parsing.
        /// </summary>
        private List<Error> errors;

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
            this.Scan(isWhitespaceAccepted: true);
            this.errors = new List<Error>();
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<Error> Errors
        {
            get
            {
                return this.errors;
            }
        }

        /// <summary>
        /// Parses the non-terminal 'Decl'.
        /// </summary>
        internal void ParseDecl()
        {
            if (this.scanner.Token.TokenType == TokenType.Assembly)
            {
                this.Scan(isWhitespaceAccepted: true);
                if (this.scanner.Token.TokenType == TokenType.Id)
                {
                    string assemblyName = this.ParseDottedName();
                    if (this.scanner.Token.TokenType == TokenType.Lbrace)
                    {
                        this.Scan(isWhitespaceAccepted: true);
                        this.ParseAsmDecl();
                        if (this.scanner.Token.TokenType == TokenType.Rbrace)
                        {
                            this.Scan(isWhitespaceAccepted: true);
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
                    this.Scan(isWhitespaceAccepted: true);
                    if (this.scanner.Token.TokenType == TokenType.Id)
                    {
                        string assemblyReferenceName = this.ParseDottedName();
                        if (this.scanner.Token.TokenType == TokenType.Lbrace)
                        {
                            this.Scan(isWhitespaceAccepted: true);
                            this.ParseAsmRefDecl();
                            if (this.scanner.Token.TokenType == TokenType.Rbrace)
                            {
                                this.Scan(isWhitespaceAccepted: true);
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
        /// <returns>The string representation of the dotted name.</returns>
        internal string ParseDottedName()
        {
            StringBuilder dottedNameBuilder = new StringBuilder();
            this.ParseDottedName(dottedNameBuilder);
            return dottedNameBuilder.ToString();
        }

        /// <summary>
        /// Parses the non-terminal 'DottedName'.
        /// </summary>
        /// <param name="dottedNameBuilder">The dotted name builder.</param>
        private void ParseDottedName(StringBuilder dottedNameBuilder)
        {
            if (this.scanner.Token.TokenType == TokenType.Id)
            {
                dottedNameBuilder.Append(this.scanner.Token.TokenText);
                this.Scan(isWhitespaceAccepted: true);
                if (this.scanner.Token.TokenType == TokenType.Dot)
                { 
                    dottedNameBuilder.Append(".");
                    this.Scan(isWhitespaceAccepted: false);
                    this.ParseDottedName(dottedNameBuilder);
                }
            }
            else
            {
                // TODO, error reporting/recovery?
                throw new Exception("6");
            }
        }

        /// <summary>
        /// Call Scan on the underlying scanner and skip all Error tokens.
        /// </summary>
        /// <param name="isWhitespaceAccepted">if set to <c>true</c> [is whitespace accepted].</param>
        private void Scan(bool isWhitespaceAccepted)
        {
            while (true)
            {
                this.scanner.Scan(isWhitespaceAccepted);
                if (this.scanner.Token.TokenType == TokenType.Error)
                {
                    this.Errors.Add(new Error(this.scanner.Line, this.scanner.Column, "TODO: Localization"));
                }
                else
                {
                    break;
                }
            }
        }
    }
}