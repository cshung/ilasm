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
        /*
CHECK .assembly extern mscorlib {}
CHECK .assembly HelloWorld {}
CHECK .module HelloWorld.exe
.namespace Hello
{
  .class public auto ansi Program extends [mscorlib]System.Object
  {
    .method public static void Main() cil managed
    {
     .entrypoint
     ldstr ""Hello World""
     call void [mscorlib]System.Console::WriteLine(string)
     ret
    }
  }
}
         */
        /// <summary>
        /// Assembles this instance.
        /// </summary>
        public void Assemble()
        {
            // TODO: Complete this so that it actually generate the hello world sample
            // TODO: Use the data generated from the parser instead of hard coding here
            PEHeaderBuilder header = new PEHeaderBuilder();
            MetadataBuilder metadata = new MetadataBuilder();
            AssemblyReferenceHandle mscorlib = metadata.AddAssemblyReference(metadata.GetOrAddString("mscorlib"), new Version(), default(StringHandle), default(BlobHandle), (System.Reflection.AssemblyFlags)0, default(BlobHandle));
            AssemblyDefinitionHandle helloworldAssembly = metadata.AddAssembly(metadata.GetOrAddString("HelloWorld"), new Version(), default(StringHandle), default(BlobHandle), (System.Reflection.AssemblyFlags)0, AssemblyHashAlgorithm.None);
            ModuleDefinitionHandle helloworldModule = metadata.AddModule(0, metadata.GetOrAddString("HelloWorld.exe"), metadata.GetOrAddGuid(Guid.NewGuid()), default(GuidHandle), default(GuidHandle));
            MetadataRootBuilder metadataRootBuilder = new MetadataRootBuilder(metadata);
            BlobBuilder stream = new BlobBuilder();
            ManagedPEBuilder managedPEBuilder = new ManagedPEBuilder(header, metadataRootBuilder, stream);
            BlobBuilder output = new BlobBuilder();
            managedPEBuilder.Serialize(output);
            File.WriteAllBytes(@"C:\Temp\HelloWorld.dll", output.ToArray());
        }
    }
}
