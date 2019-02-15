//-----------------------------------------------------------------------
// <copyright file="Error.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Ilasm
{
    /// <summary>
    /// The Error.
    /// </summary>
    internal class Error
    {
        /// <summary>
        /// The line number of the error.
        /// </summary>
        private int line;

        /// <summary>
        /// The column number of the error.
        /// </summary>
        private int column;

        /// <summary>
        /// The message of the error.
        /// </summary>
        private string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="line">The line number of the error.</param>
        /// <param name="column">The column number of the error.</param>
        /// <param name="message">The message of the error.</param>
        public Error(int line, int column, string message)
        {
            this.line = line;
            this.column = column;
            this.message = message;
        }

        /// <summary>
        /// Gets the line number of the error.
        /// </summary>
        /// <value>
        /// The line number of the error.
        /// </value>
        public int Line
        {
            get
            {
                return this.line;
            }
        }

        /// <summary>
        /// Gets the column number of the error.
        /// </summary>
        /// <value>
        /// The column number of the error.
        /// </value>
        public int Column
        {
            get
            {
                return this.column;
            }
        }

        /// <summary>
        /// Gets the message of the error.
        /// </summary>
        /// <value>
        /// The message of the error.
        /// </value>
        public string Message
        {
            get
            {
                return this.message;
            }
        }
    }
}