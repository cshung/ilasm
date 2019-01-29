namespace Microsoft.Ilasm.Tests
{
    using Xunit;
    using Microsoft.Ilasm;
    using System;

    public class ScannerTests
    {
        private static string sampleFile = @"
.assembly extern mscorlib {}
.assembly HelloWorld {}
.module HelloWorld.exe
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
";
        [Fact]
        public void TestConstructingScannerWithNull()
        {
            bool argNullThrown = false;
            try
            {
                Scanner scanner = new Scanner(null);
            }
            catch (ArgumentNullException argNull)
            {
                Assert.Equal("text", argNull.ParamName);
                argNullThrown = true;
            }
            Assert.True(argNullThrown);
        }

        [Fact]
        public void TestLeadingSpace()
        {
            Scanner scanner = new Scanner("    .assembly");
            Assert.Equal(Token.Assembly, scanner.Token);
        }

        [Fact]
        public void TestScanningSample()
        {
            Scanner scanner = new Scanner(sampleFile);
            Assert.Equal(Token.Assembly, scanner.Token);
        }
    }
}
