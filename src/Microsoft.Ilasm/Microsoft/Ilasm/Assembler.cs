//-----------------------------------------------------------------------
// <copyright file="Assembler.cs" company="PlaceholderCompany">
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
    /// The Assembler.
    /// </summary>
    internal class Assembler
    {
        /// <summary>
        /// Assembles this instance.
        /// </summary>
        public void Assemble()
        {
            // TODO: Complete this so that it actually generate the hello world sample
            // TODO: Use the data generated from the parser instead of hard coding here
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
