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
    }
}
