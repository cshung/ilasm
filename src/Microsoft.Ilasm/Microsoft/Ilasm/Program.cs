//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Ilasm
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Reflection.Metadata;
    using System.Reflection.Metadata.Ecma335;
    using System.Reflection.PortableExecutable;

    /// <summary>
    /// The program.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        private static void Main()
        {
            PEHeaderBuilder header = new PEHeaderBuilder();
            MetadataBuilder metadata = new MetadataBuilder();
            metadata.AddAssembly(metadata.GetOrAddString("HelloWorld"), new Version(), default(StringHandle), default(BlobHandle), (System.Reflection.AssemblyFlags)0, AssemblyHashAlgorithm.None);
            MetadataRootBuilder metadataRootBuilder = new MetadataRootBuilder(metadata);
            BlobBuilder stream = new BlobBuilder();
            ManagedPEBuilder managedPEBuilder = new ManagedPEBuilder(header, metadataRootBuilder, stream);
            BlobBuilder output = new BlobBuilder();
            managedPEBuilder.Serialize(output);
            File.WriteAllBytes(@"C:\Temp\HelloWorld.dll", output.ToArray());
        }
    }
}
