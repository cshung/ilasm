namespace Microsoft.Ilasm
{
    using System;

    /// <summary>
    /// The parser.
    /// </summary>
    internal class Parser
    {
        private Scanner scanner;

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
