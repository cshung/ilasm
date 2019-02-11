//-----------------------------------------------------------------------
// <copyright file="TokenType.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>This is the Widget class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Ilasm
{
    /// <summary>
    /// The TokenType.
    /// </summary>
    internal enum TokenType
    {
        /// <summary>
        /// The assembly.
        /// </summary>
        Assembly,

        /// <summary>
        /// The extern.
        /// </summary>
        Extern,

        /// <summary>
        /// The identifier.
        /// </summary>
        Id,

        /// <summary>
        /// The end of file.
        /// </summary>
        Eof,

        /// <summary>
        /// The right brace.
        /// </summary>
        Rbrace,

        /// <summary>
        /// The left brace.
        /// </summary>
        Lbrace,

        /// <summary>
        /// The module.
        /// </summary>
        Module,

        /// <summary>
        /// The dot.
        /// </summary>
        Dot,

        /// <summary>
        /// The namespace.
        /// </summary>
        NameSpace,

        /// <summary>
        /// The class.
        /// </summary>
        Class
    }
}
