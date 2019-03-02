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
CHECK .namespace Hello
CHECK {
CHECK   .class public auto ansi Program extends [mscorlib]System.Object
CHECK   {
CHECK    .method public static void Main() cil managed
CHECK    {
     .entrypoint
     ldstr ""Hello World""
     call void [mscorlib]System.Console::WriteLine(string)
     ret
CHECK    }
CHECK   }
CHECK }
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
            AssemblyReferenceHandle mscorlib = metadata.AddAssemblyReference(metadata.GetOrAddString("mscorlib"), new Version(), default(StringHandle), default(BlobHandle), (AssemblyFlags)0, default(BlobHandle));
            AssemblyDefinitionHandle helloworldAssembly = metadata.AddAssembly(metadata.GetOrAddString("HelloWorld"), new Version(), default(StringHandle), default(BlobHandle), (AssemblyFlags)0, AssemblyHashAlgorithm.None);
            ModuleDefinitionHandle helloworldModule = metadata.AddModule(0, metadata.GetOrAddString("HelloWorld.exe"), metadata.GetOrAddGuid(Guid.NewGuid()), default(GuidHandle), default(GuidHandle));
            EntityHandle objectClass = metadata.AddTypeReference(mscorlib, metadata.GetOrAddString("System"), metadata.GetOrAddString("Object"));

            // Something worth notice with the FieldDefinitionHandle and MethodDefinitionHandle here, it is supposed to point to the first field/method
            // the metadata is read in a way such that it will read in sequence until it reach either the end or the next start
            // So even if it is empty, we need to point to a valid row
            // and row starts with 1
            TypeDefinitionHandle programTypeDefinition = metadata.AddTypeDefinition(
                TypeAttributes.Public | TypeAttributes.AutoClass | TypeAttributes.AnsiClass,
                metadata.GetOrAddString("Hello"),
                metadata.GetOrAddString("Program"),
                objectClass,
                MetadataTokens.FieldDefinitionHandle(1),
                MetadataTokens.MethodDefinitionHandle(1));

            MethodSignatureEncoder methodSignatureEncoder = new BlobEncoder(new BlobBuilder()).MethodSignature(SignatureCallingConvention.Default, 0, false);
            ReturnTypeEncoder returnTypeEncoder;
            ParametersEncoder parametersEncoder;
            methodSignatureEncoder.Parameters(0, out returnTypeEncoder, out parametersEncoder);
            returnTypeEncoder.Void();

            MethodDefinitionHandle methodDefinitionHandle = metadata.AddMethodDefinition(
                MethodAttributes.Public | MethodAttributes.Static,
                MethodImplAttributes.IL | MethodImplAttributes.Managed,
                metadata.GetOrAddString("Main"),
                metadata.GetOrAddBlob(methodSignatureEncoder.Builder),
                0, // TODO - one last thing (or not?) - we need to encode the method body
                MetadataTokens.ParameterHandle(1));

            MetadataRootBuilder metadataRootBuilder = new MetadataRootBuilder(metadata);
            BlobBuilder stream = new BlobBuilder();
            ManagedPEBuilder managedPEBuilder = new ManagedPEBuilder(header, metadataRootBuilder, stream);
            BlobBuilder output = new BlobBuilder();
            managedPEBuilder.Serialize(output);
            File.WriteAllBytes(@"C:\Temp\HelloWorld.dll", output.ToArray());
        }
    }
}
